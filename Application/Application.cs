using homework_5_SOLID.Domain.Contracts.UI;
using homework_5_SOLID.Domain.Models;

namespace homework_5_SOLID.Application
{
    public class Application(IMenuUI mainMenuUI, GameSession gameSession, SettingsEditor settingsEditor)
    {
        private readonly IMenuUI _menuUI = mainMenuUI;
        private readonly GameSession _gameSession = gameSession;
        private readonly SettingsEditor _settingsEditor = settingsEditor;

        public void Run()
        {
            var menuModel = new MenuModel(
                "Угадай число",
                [
                    "Начать игру",
                    "Настройки",
                    "Выход"
                ]
            );

            while (true)
            {
                var input = _menuUI.ShowMenu(menuModel);

                switch (input)
                {
                    case 1:
                        _gameSession.StartGame();
                        break;
                    case 2:
                        _settingsEditor.OpenSettings();
                        break;
                    case 3:
                        return;
                }
            }
        }
    }
}
