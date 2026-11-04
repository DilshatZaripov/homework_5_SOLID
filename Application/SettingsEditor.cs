using homework_5_SOLID.Domain.Contracts.Input;
using homework_5_SOLID.Domain.Contracts.Settings;
using homework_5_SOLID.Domain.Contracts.UI;
using homework_5_SOLID.Domain.Models;

namespace homework_5_SOLID.Application
{
    public class SettingsEditor(IGameSettingsReader settingsReader, IGameSettingsWriter settingsWriter, IMenuUI menuUI, IInputReader inputReader)
    {
        private readonly IGameSettingsReader _reader = settingsReader;
        private readonly IGameSettingsWriter _writer = settingsWriter;
        private readonly IMenuUI _settingsUI = menuUI;
        private readonly IInputReader _inputReader = inputReader;

        public void OpenSettings()
        {
            var settings = _reader.GetGameSettings();

            var menuModel = BuildSettingsMenuModel(settings);

            while (true)
            {
                var input = _settingsUI.ShowMenu(menuModel);

                switch (input)
                {
                    case 1:
                        if (!int.TryParse(_inputReader.Read(), out int newMaxAttempts))
                            break;

                        try
                        {
                            settings = settings with { MaxAttempts = newMaxAttempts };
                        }
                        catch (ArgumentException) { break; }

                        menuModel = BuildSettingsMenuModel(settings);
                        break;
                    case 2:
                        if (!int.TryParse(_inputReader.Read(), out int newMaxValue))
                            break;
                        
                        try
                        {
                            settings = settings with { MaxValue = newMaxValue };
                        }
                        catch (ArgumentException) { break; }

                        menuModel = BuildSettingsMenuModel(settings);
                        break;
                    case 3:
                        if (!int.TryParse(_inputReader.Read(), out int newMinValue))
                            break;

                        try
                        {
                            settings = settings with { MinValue = newMinValue };
                        }
                        catch (ArgumentException) { break; }

                        menuModel = BuildSettingsMenuModel(settings);
                        break;
                    case 4:
                        settings = _reader.GetDefaultGameSettings();
                        menuModel = BuildSettingsMenuModel(settings);
                        break;
                    case 5:
                        _writer.Save(settings);
                        return;
                    case 6:
                        return;
                }
            }
        }

        private static MenuModel BuildSettingsMenuModel(GameSettings settings) => 
            new(
                "Настройки игры",
                [
                    $"Максимальное число попыток: {settings.MaxAttempts}",
                    $"Максимальое значение: {settings.MaxValue}",
                    $"Минимальное значение: {settings.MinValue}",
                    $"Настроить по-умолчанию",
                    $"Сохранить и выйти",
                    $"Назад"
                ]
            );
    }
}
