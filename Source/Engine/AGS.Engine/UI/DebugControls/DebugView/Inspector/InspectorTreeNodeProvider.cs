﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using AGS.API;

namespace AGS.Engine
{
    public class InspectorTreeNodeProvider : ITreeNodeViewProvider
    {
        private ITreeNodeViewProvider _provider;
        private IGameFactory _factory;
        private readonly Dictionary<string, ITreeTableLayout> _layouts;
        private readonly IGameEvents _gameEvents;
        private readonly IBlockingEvent _onResize;
        private readonly IObject _inspectorPanel;
        private float _rowWidth;

        private static int _nextNodeId;

        public InspectorTreeNodeProvider(ITreeNodeViewProvider provider, IGameFactory factory, 
                                         IGameEvents gameEvents, IObject inspectorPanel)
        {
            _inspectorPanel = inspectorPanel;
            _onResize = new AGSEvent();
            _provider = provider;
            _factory = factory;
            _gameEvents = gameEvents;
            _layouts = new Dictionary<string, ITreeTableLayout>();
        }

        public void BeforeDisplayingNode(ITreeStringNode item, ITreeNodeView nodeView, bool isCollapsed, bool isHovered, bool isSelected)
        {
            _provider.BeforeDisplayingNode(item, nodeView, isCollapsed, isHovered, isSelected);
            var parent = item.TreeNode.Parent;
            if (parent != null && parent.TreeNode.Parent == null)
            {
                displayCategoryRow(nodeView, isSelected);
            }
        }

        public void Resize(float width)
        {
            _rowWidth = width;
            _onResize.Invoke();
        }

        public ITreeNodeView CreateNode(ITreeStringNode item, IRenderLayer layer)
        {
            var view = _provider.CreateNode(item, layer);
            var parent = item.TreeNode.Parent;
            if (parent != null && parent.TreeNode.Parent == null)
            {
                setupCategoryRow(view);
            }
            else if (parent != null && parent.TreeNode.Parent != null)
            {
                var layoutId = parent.Properties.Strings.GetValue("LayoutID", Guid.NewGuid().ToString());
                var tableLayout = _layouts.GetOrAdd(layoutId, () => new TreeTableLayout(_gameEvents) { ColumnPadding = 20f });
                view.HorizontalPanel.RemoveComponent<IStackLayoutComponent>();
                var rowLayout = view.HorizontalPanel.AddComponent<ITreeTableRowLayoutComponent>();
                rowLayout.Table = tableLayout;
            }
            var node = item as IInspectorTreeNode;
            if (node == null) return view;

			int nodeId = Interlocked.Increment(ref _nextNodeId);
			var itemTextId = (item.Text ?? "") + "_" + nodeId;
            node.Editor.AddEditorUI("InspectorEditor_" + itemTextId, view, node.Property);

            var propertyChanged = node.Property.Object as INotifyPropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged.PropertyChanged += (sender, e) => 
                {
                    if (e.PropertyName != node.Property.Name) return;
                    node.Property.Refresh();
                    node.Editor.RefreshUI();
                };
            }

            return view;
        }

        private void setupCategoryRow(ITreeNodeView view)
        {
            var inspectorJump = _inspectorPanel.AddComponent<IJumpOffsetComponent>();
            var rowJump = view.HorizontalPanel.AddComponent<IJumpOffsetComponent>();
            inspectorJump.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(IJumpOffsetComponent.JumpOffset)) return;
                rowJump.JumpOffset = new PointF(-inspectorJump.JumpOffset.X, 0f);
            };
        }

        private void displayCategoryRow(ITreeNodeView nodeView, bool isSelected)
        {
            nodeView.TreeItem.Tint = Colors.Transparent;
            nodeView.HorizontalPanel.Tint = isSelected ? Colors.DarkSlateBlue : Colors.Gray.WithAlpha(50);
            _onResize.Unsubscribe(onResize);
            _onResize.Subscribe(onResize);
            onResize();

            void onResize()
            {
                nodeView.HorizontalPanel.BaseSize = new SizeF(_rowWidth, 30f);
            }
        }
    }
}
