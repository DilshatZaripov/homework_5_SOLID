using homework_5_SOLID.Domain.Contracts;
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
                        _settingsUI.ShowMessage("Введите новое значение для максимальных попыток:");
                        int newMaxAttempts = _inputReader.Read();
                        if (newMaxAttempts < 1)
                        {
                            _settingsUI.ShowMessage("Максимальные попытки не могут быть меньше 1");
                            break;
                        }

                        settings = settings with { MaxAttempts = newMaxAttempts };
                        menuModel = BuildSettingsMenuModel(settings);
                        break;
                    case 2:
                        _settingsUI.ShowMessage("Введите новое значение для максимального значения:");
                        int newMaxValue = _inputReader.Read();
                        if (newMaxValue <= settings.MinValue)
                        {
                            _settingsUI.ShowMessage("Максимальное значение должно быть больше минимального");
                            break;
                        }

                        settings = settings with { MaxValue = newMaxValue };
                        menuModel = BuildSettingsMenuModel(settings);
                        break;
                    case 3:
                        _settingsUI.ShowMessage("Введите новое значение для минимального значения:");
                        int newMinValue = _inputReader.Read();
                        if (newMinValue >= settings.MaxValue)
                        {
                            _settingsUI.ShowMessage("Минимальное значение должно быть меньше максимального");
                            break;
                        }

                        settings = settings with { MinValue = newMinValue };
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
                    default:
                        _settingsUI.ShowMessage("Введите корректный пункт меню 1-6");
                        break;
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
