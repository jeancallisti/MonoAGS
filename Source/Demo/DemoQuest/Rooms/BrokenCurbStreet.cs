﻿using System;
using System.Threading.Tasks;
using AGS.API;
using AGS.Engine;

namespace DemoGame
{
	public class BrokenCurbStreet
	{
		private IRoom _room;
		private IPlayer _player;
		private IGame _game;

		private const string _baseFolder = "../../Assets/Rooms/BrokenCurbStreet/";

		public async Task<IRoom> LoadAsync(IGame game)
		{
			_game = game;
			_game.Events.OnSavedGameLoad.Subscribe((sender, e) => onSavedGameLoaded());
			_player = game.State.Player;
			IGameFactory factory = game.Factory;
			_room = factory.Room.GetRoom ("Broken Curb Street", 20f, 310f, 190f, 10f);
			_room.MusicOnLoad = await factory.Sound.LoadAudioClipAsync("../../Assets/Sounds/01_Ghosts_I.flac");

			IObject bg = factory.Object.GetObject("Broken Curb BG");
			bg.Image = await factory.Graphics.LoadImageAsync(_baseFolder + "bg.png");
			_room.Background = bg;

			AGSMaskLoader maskLoader = new AGSMaskLoader (factory, new ResourceLoader());
			_room.WalkableAreas.Add(new AGSArea { Mask = await maskLoader.LoadAsync(_baseFolder + "walkable1.png") });
			_room.WalkableAreas.Add(new AGSArea { Mask = await maskLoader.LoadAsync(_baseFolder + "walkable2.png") });
			_room.WalkableAreas.Add(new AGSArea { Mask = await maskLoader.LoadAsync(_baseFolder + "walkable3.png"), Enabled = false });
			_room.WalkableAreas.Add(new AGSArea { Mask = await maskLoader.LoadAsync(_baseFolder + "walkable4.png") });
			_room.ScalingAreas.Add(AGSScalingArea.Create(_room.WalkableAreas[0], 0.50f, 0.90f));
			_room.ScalingAreas.Add(AGSScalingArea.Create(_room.WalkableAreas[2], 0.40f, 0.50f));
			_room.ScalingAreas.Add(AGSScalingArea.Create(_room.WalkableAreas[3], 1.20f, 1.20f));

			_room.WalkBehindAreas.Add(new AGSWalkBehindArea (new AGSArea { Mask = maskLoader.Load(_baseFolder + "walkbehind1.png") }));
			_room.WalkBehindAreas.Add(new AGSWalkBehindArea (new AGSArea { Mask = maskLoader.Load(_baseFolder + "walkbehind2.png") }));

			IObject wallHotspot = await factory.Object.GetHotspotAsync(_baseFolder + "wallHotspot.png", "Wall");
			IObject graffitiHotspot = await factory.Object.GetHotspotAsync (_baseFolder + "graffitiHotspot.png", "Graffiti");
			IObject doorHotspot = await factory.Object.GetHotspotAsync (_baseFolder + "doorHotspot.png", "Door");
			doorHotspot.Z = wallHotspot.Z - 1;
			graffitiHotspot.Z = wallHotspot.Z - 1;

			IObject manholeHotspot = await factory.Object.GetHotspotAsync (_baseFolder + "manholeHotspot.png", "Manhole");
			IObject roadHotspot = await factory.Object.GetHotspotAsync (_baseFolder + "roadHotspot.png", "Road");
			manholeHotspot.Z = roadHotspot.Z - 1;

			_room.Objects.Add(await factory.Object.GetHotspotAsync(_baseFolder + "barrierHotspot.png", "Barrier"));
			_room.Objects.Add(await factory.Object.GetHotspotAsync(_baseFolder + "bloodySidewalkHotspot.png", "Bloody Sidewalk"));
			_room.Objects.Add(await factory.Object.GetHotspotAsync(_baseFolder + "brokenCurbHotspot.png", "Broken Curb"));
			_room.Objects.Add(await factory.Object.GetHotspotAsync(_baseFolder + "curbHotspot.png", "Curb"));
			_room.Objects.Add(doorHotspot);
			_room.Objects.Add(graffitiHotspot);
			_room.Objects.Add(manholeHotspot);
			_room.Objects.Add(roadHotspot);
			_room.Objects.Add(await factory.Object.GetHotspotAsync(_baseFolder + "sidewalkHotspot.png", "Sidewalk"));
			_room.Objects.Add(await factory.Object.GetHotspotAsync(_baseFolder + "slimeHotspot.png", "Slime"));
			_room.Objects.Add(wallHotspot);

			IObject panel = factory.Object.GetObject("Panel");
			panel.Hotspot = "Panel";
			IAnimation panelAnimation = await factory.Graphics.LoadAnimationFromFolderAsync(_baseFolder + "Panel");
			Characters.RandomAnimationDelay(panelAnimation);
			panel.StartAnimation(panelAnimation);
			panel.X = 195;
			panel.Y = 145;
			panel.Z = 110;
			panel.Interactions.OnInteract.Subscribe((sender, args) => panel.Animation.State.IsPaused = !panel.Animation.State.IsPaused);
			_room.Objects.Add(panel);

			subscribeEvents();

			return _room;
		}

		private void onSavedGameLoaded()
		{
			_player = _game.State.Player;
			_room = Rooms.Find(_game.State, _room);
			subscribeEvents();
		}

		private void subscribeEvents()
		{
			_room.Edges.Left.OnEdgeCrossed.Subscribe(onLeftEdgeCrossed);
			_room.Events.OnBeforeFadeIn.Subscribe(onBeforeFadeIn);
		}

		private void onLeftEdgeCrossed(object sender, AGSEventArgs args)
		{
			_player.Character.ChangeRoom(Rooms.EmptyStreet.Result, 310);
		}

		private void onBeforeFadeIn(object sender, AGSEventArgs args)
		{
			_player.Character.PlaceOnWalkableArea();
		}
	}
}
