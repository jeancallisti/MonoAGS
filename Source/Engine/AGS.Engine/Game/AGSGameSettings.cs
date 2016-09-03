﻿using AGS.API;

namespace AGS.Engine
{
	public class AGSGameSettings : IGameSettings
	{
		public static IFont DefaultSpeechFont = Hooks.FontLoader.LoadFont(null, 14f); 
		
		public static IFont DefaultTextFont = Hooks.FontLoader.LoadFont(null, 14f);

        public static ISkin CurrentSkin;

		public AGSGameSettings(string title, AGS.API.Size virtualResolution, WindowState windowState = WindowState.Maximized,
            AGS.API.Size? windowSize = null, VsyncMode vsync = VsyncMode.On, bool preserveAspectRatio = true)
		{
            Title = title;
            VirtualResolution = virtualResolution;
            WindowState = windowState;
            WindowSize = windowSize.HasValue ? windowSize.Value : virtualResolution;
            Vsync = vsync;
            PreserveAspectRatio = preserveAspectRatio;
		}

        public string Title { get; private set; }
        
        public AGS.API.Size VirtualResolution { get; private set; }

        public VsyncMode Vsync { get; private set; }

        public AGS.API.Size WindowSize { get; private set; }

        public bool PreserveAspectRatio { get; private set; }

        public WindowState WindowState { get; private set; }
    }
}

