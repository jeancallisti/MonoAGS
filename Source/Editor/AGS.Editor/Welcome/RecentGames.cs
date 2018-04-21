﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AGS.API;
using AGS.Engine;

namespace AGS.Editor
{
    public class RecentGames
    {
        const string PATH = "recent_games.txt";
        const int GAMES_LIMIT = 8;
        private readonly IFileSystem _fileSystem;
        private readonly IGameFactory _factory;
        private readonly List<(string, string)> _games = new List<(string, string)>();
        private readonly IRenderMessagePump _messagePump;

        public RecentGames(IFileSystem fileSystem, IGameFactory factory, IRenderMessagePump messagePump)
        {
            _fileSystem = fileSystem;
            _factory = factory;
            _messagePump = messagePump;
        }

        public void Load()
        {
            if (!_fileSystem.FileExists(PATH)) return;
            var stream = _fileSystem.Open(PATH);
            if (stream == null) return;
            using (stream)
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    string gameName = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        if (gameName == null)
                        {
                            gameName = line;
                            continue;
                        }
                        if (!_fileSystem.FileExists(line))
                        {
                            gameName = null;
                            continue;
                        }
                        _games.Add((gameName, line));
                    }
                }
            }
        }

        public IObject Show(IObject parent)
        {
            var panel = _factory.UI.GetPanel("RecentGamesPanel", 800f, 600f, 0f, 0f, parent);
            panel.Tint = GameViewColors.SubPanel;
            panel.Border = AGSBorders.SolidColor(GameViewColors.Border, 5f);
            var layout = panel.AddComponent<IStackLayoutComponent>();
            layout.StartLocation = panel.Height - 60f;
            layout.AbsoluteSpacing = -10f;
            _factory.UI.GetLabel("RecentGamesLabel", "Recent Games:", 200f, 50f, 0f, 0f, panel);

            var labelFont = _factory.Fonts.LoadFont(AGSGameSettings.DefaultTextFont.FontFamily, 8f, FontStyle.Italic);
            if (_games.Count == 0)
            {
                _factory.UI.GetLabel("NoRecentGamesLabel", "There's nothing here yet, time to start making games!", 200f, 20f, 0f, 0f, panel, new AGSTextConfig(_factory.Graphics.Brushes.LoadSolidBrush(Colors.Gray), labelFont));
                layout.StartLayout();
                return panel;
            }

            var buttonFont = _factory.Fonts.LoadFont(AGSGameSettings.DefaultTextFont.FontFamily, 14f, FontStyle.Bold);

            var idle = new ButtonAnimation(null, new AGSTextConfig(_factory.Graphics.Brushes.LoadSolidBrush(Colors.LightSkyBlue), buttonFont, autoFit: AutoFit.LabelShouldFitText), Colors.Transparent);
            var hovered = new ButtonAnimation(null, new AGSTextConfig(_factory.Graphics.Brushes.LoadSolidBrush(Colors.Yellow), buttonFont, autoFit: AutoFit.LabelShouldFitText), Colors.Transparent);
            var pushed = new ButtonAnimation(null, new AGSTextConfig(_factory.Graphics.Brushes.LoadSolidBrush(Colors.Black), buttonFont, autoFit: AutoFit.LabelShouldFitText), Colors.Transparent);

            foreach (var (game, path) in _games)
            {
                var gamePanel = _factory.UI.GetPanel($"RecentGamePanel_{path}", panel.Width, 40f, 0f, 0f, panel);
                gamePanel.Tint = Colors.Transparent;
                var button = _factory.UI.GetButton($"RecentGameButton_{path}", idle, hovered, pushed, 0f, 20f, gamePanel, game, width: 30f, height: 20f);
                button.MouseClicked.Subscribe(async args =>
                {
                    if (!_fileSystem.FileExists(path))
                    {
                        await AGSMessageBox.DisplayAsync($"Did not find a game in the path: {path}");
                        return;
                    }
                    parent.Visible = false;
                    AddGame(game, path);
                    await Task.Delay(100);
                    GameLoader.Load(_messagePump, AGSProject.Load(path), _factory);
                });
                _factory.UI.GetLabel($"RecentGameLabel_{path}", path, 200f, 20f, 0f, 0f, gamePanel, new AGSTextConfig(_factory.Graphics.Brushes.LoadSolidBrush(Colors.Gray), labelFont));
            }
            layout.StartLayout();
            return panel;
        }

        public void AddGame(string name, string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return;
            _games.Remove((name, path));
            _games.Insert(0, (name, path));
            if (_games.Count > GAMES_LIMIT) _games.RemoveAt(_games.Count - 1);
            save();
        }

        private void save()
        {
            var stream = _fileSystem.Create(PATH);
            if (stream == null) return;
            using (stream)
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    foreach ((string name, string path) in _games)
                    {
                        writer.WriteLine(name);
                        writer.WriteLine(path);
                    }
                }
            }
        }
    }
}
