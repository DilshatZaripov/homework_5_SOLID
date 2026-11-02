using homework_5_SOLID.Domain.Contracts.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace homework_5_SOLID.Application
{
    public class Application(IMenuUI ui, GameSession gameSession, SettingsEditor settingsEditor)
    {
        private readonly IMenuUI        _ui = ui;
        private readonly GameSession    _gameSession = gameSession;
        private readonly SettingsEditor _settingsEditor = settingsEditor;

        public void Run()
        {

        }
    }
}
