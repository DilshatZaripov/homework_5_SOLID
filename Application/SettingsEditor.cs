using homework_5_SOLID.Domain.Contracts;
using homework_5_SOLID.Domain.Contracts.Settings;
using homework_5_SOLID.Domain.Contracts.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace homework_5_SOLID.Application
{
    public class SettingsEditor(IGameSettingsReader reader, IGameSettingsWriter writer, ISettingsUI settingsUI, IInputReader inputReader)
    {
        private readonly IGameSettingsReader _reader = reader;
        private readonly IGameSettingsWriter _writer = writer;
        private readonly ISettingsUI _settingsUI = settingsUI;
        private readonly IInputReader _inputReader = inputReader;

        public void OpenSettings()
        {
            var settings = _reader.GetGameSettings();

            while (true)
            {
                _settingsUI.ShowSettings(settings.MaxAttempts, settings.MinValue, settings.MaxValue);

            }
        }
    }
}
