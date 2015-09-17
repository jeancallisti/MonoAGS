﻿using System;
using Moq;
using AGS.API;
using System.Collections.Generic;
using Autofac;
using Autofac.Features.ResolveAnything;
using System.Drawing;
using Autofac.Core;

namespace Tests
{
	public class Mocks : IDisposable
	{
		Mock<IAnimationState> _animationState;
		Mock<IAnimation> _animation;
		Mock<IGameState> _gameState;
		Mock<IPlayer> _player;
		Mock<ICharacter> _character;
		Mock<IRoom> _room;
		Mock<IObject> _obj;
		Mock<IViewport> _viewport;
		Mock<ISprite> _sprite;
		Mock<IImage> _image;
		Mock<IMaskLoader> _maskLoader;

		IContainer container;

		public static Mocks Init()
		{
			ContainerBuilder builder = new ContainerBuilder ();
			Mocks mocks = new Mocks ();
			builder.RegisterInstance(mocks.Animation().Object);
			builder.RegisterInstance(mocks.AnimationState().Object);
			builder.RegisterInstance(mocks.GameState().Object);
			builder.RegisterInstance(mocks.Player().Object);
			builder.RegisterInstance(mocks.Character().Object);
			builder.RegisterInstance(mocks.Room().Object);
			builder.RegisterInstance(mocks.Object().Object);
			builder.RegisterInstance(mocks.Viewport().Object);
			builder.RegisterInstance(mocks.Sprite().Object);
			builder.RegisterInstance(mocks.Image().Object);

			builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

			mocks.container = builder.Build();

			return mocks;
		}

		public TItem Create<TItem>(params Parameter[] parameters)
		{
			return container.Resolve<TItem>(parameters);
		}

		public void Dispose()
		{
			container.Dispose();
		}

		public Mock<IAnimationState> AnimationState()
		{
			if (_animationState == null)
			{
				_animationState = new Mock<IAnimationState> ();
			}
			return _animationState;
		}

		public Mock<IAnimation> Animation()
		{
			if (_animation == null)
			{
				_animation = new Mock<IAnimation> ();
				_animation.Setup(m => m.State).Returns(AnimationState().Object);
				_animation.Setup(m => m.Sprite).Returns(Sprite().Object);
			}
			return _animation;
		}

		public Mock<IGameState> GameState()
		{
			if (_gameState == null)
			{
				_gameState = new Mock<IGameState> ();
				_gameState.Setup(m => m.Player).Returns(Player().Object);
			}
			return _gameState;
		}

		public Mock<IPlayer> Player()
		{
			if (_player == null)
			{
				_player = new Mock<IPlayer> ();
				_player.Setup(m => m.Character).Returns(Character().Object);
			}
			return _player;
		}

		public Mock<ICharacter> Character()
		{
			if (_character == null)
			{
				_character = new Mock<ICharacter> ();
				_character.Setup(m => m.Room).Returns(Room().Object);
			}
			return _character;
		}

		public Mock<IRoom> Room()
		{
			if (_room == null)
			{
				_room = new Mock<IRoom> ();
				_room.Setup(m => m.Background).Returns(Object().Object);
				_room.Setup(m => m.Viewport).Returns(Viewport().Object);
				_room.Setup(m => m.Objects).Returns(new List<IObject> ());
				_room.Setup(m => m.ShowPlayer).Returns(true);
			}
			return _room;
		}

		public Mock<IObject> Object()
		{
			if (_obj == null)
			{
				_obj = new Mock<IObject> ();
				_obj.Setup(m => m.Animation).Returns(Animation().Object);
				_obj.Setup(m => m.Image).Returns(Image().Object);
				_obj.Setup(m => m.Enabled).Returns(true);
				_obj.Setup(m => m.Visible).Returns(true);
			}
			return _obj;
		}

		public Mock<IViewport> Viewport()
		{
			if (_viewport == null)
			{
				_viewport = new Mock<IViewport> ();
			}
			return _viewport;
		}

		public Mock<ISprite> Sprite()
		{
			if (_sprite == null)
			{
				_sprite = new Mock<ISprite> ();
				_sprite.Setup(m => m.Image).Returns(Image().Object);
			}
			return _sprite;
		}

		public Mock<IImage> Image()
		{
			if (_image == null)
			{
				_image = new Mock<IImage> ();
			}
			return _image;
		}

		public Mock<IMaskLoader> MaskLoader()
		{
			if (_maskLoader == null)
			{
				_maskLoader = new Mock<IMaskLoader> ();
			}
			return _maskLoader;
		}
	}
}

