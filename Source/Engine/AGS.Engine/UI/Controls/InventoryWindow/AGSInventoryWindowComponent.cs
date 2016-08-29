﻿using System;
using AGS.API;

using System.Collections.Generic;

namespace AGS.Engine
{
	public class AGSInventoryWindowComponent : AGSComponent, IInventoryWindowComponent
	{
		private IList<IObject> _inventoryItems;
		private volatile bool _refreshNeeded;
		private AGS.API.SizeF _itemSize;
		private ICharacter _character;
		private int _topItem;
		private IGameState _state;
		private IGameEvents _gameEvents;
		private IScale _scale;
		private IInObjectTree _tree;

		public AGSInventoryWindowComponent(IGameState state, IGameEvents gameEvents)
		{
			_state = state;
			_inventoryItems = new List<IObject> (20);
			_gameEvents = gameEvents;
			_gameEvents.OnRepeatedlyExecute.Subscribe(onRepeatedlyExecute);
		}

		public override void Init(IEntity entity)
		{
			base.Init(entity);
			_scale = entity.GetComponent<IScaleComponent>();
			_tree = entity.GetComponent<IInObjectTree>();
		}

		public override void Dispose()
		{
			base.Dispose();
			_gameEvents.OnRepeatedlyExecute.Unsubscribe(onRepeatedlyExecute);
		}

		#region IInventoryWindow implementation

		public void ScrollUp()
		{
			TopItem = Math.Max(0, TopItem - ItemsPerRow);
		}

		public void ScrollDown()
		{
			TopItem = Math.Min(CharacterToUse.Inventory.Items.Count - 1, TopItem + ItemsPerRow); 
		}

		public AGS.API.SizeF ItemSize
		{
			get { return _itemSize; }
			set
			{
				_itemSize = value;
				_refreshNeeded = true;
			}
		}

		public ICharacter CharacterToUse 
		{
			get { return _character; }
			set
			{
				_character = value;
				_refreshNeeded = true;
			}
		}

		public int TopItem 
		{
			get { return _topItem; }
			set
			{
				_topItem = value;
				_refreshNeeded = true;
			}
		}

		public int ItemsPerRow { get { return (int)(_scale.Width / ItemSize.Width); } }

		public int RowCount { get { return (int)(_scale.Height / ItemSize.Height); } }

		#endregion

		private void onRepeatedlyExecute(object sender, AGSEventArgs args)
		{
			if (_character == null) return;
			if (!isRefreshNeeded()) return;
			_refreshNeeded = false;

			foreach (var obj in _inventoryItems)
			{
                obj.Visible = false;				
			}
			List<IObject> items = new List<IObject> (_character.Inventory.Items.Count);
			foreach (var item in _character.Inventory.Items)
			{
				items.Add(item.Graphics);
			}
			_inventoryItems = items;

			int topItem = TopItem;
			int count = Math.Min(topItem + RowCount * ItemsPerRow, items.Count);
			float stepX = ItemSize.Width;
			float stepY = ItemSize.Height;
			float x = stepX/2f;
			float y = _scale.Height - stepY/2;
			for (int item = topItem; item < count; item++)
			{
				IObject obj = items[item];
				obj.X = x;
				obj.Y = y;
                
                _tree.TreeNode.AddChild(obj);
                if (!_state.UI.Contains(obj))
                {
                    _state.UI.Add(obj);
                }
                obj.Visible = true;

				x += stepX;
				if (x >= _scale.Width)
				{
					x = stepX/2f;
					y -= stepY;
				}
			}
		}

		private bool isRefreshNeeded()
		{
			if (_refreshNeeded) return true;
			if (_inventoryItems.Count != _character.Inventory.Items.Count) return true;
			for (int i = 0; i < _inventoryItems.Count; i++)
			{
				var item = _inventoryItems[i];
				if (item != _character.Inventory.Items[i].Graphics) return true;
				if (item.TreeNode.Parent == null) return true;
			}
			return false;
		}
	}
}

