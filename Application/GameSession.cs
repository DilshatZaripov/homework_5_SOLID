using homework_5_SOLID.Domain.Contracts;
using homework_5_SOLID.Domain.Contracts.Settings;
using homework_5_SOLID.Domain.Contracts.UI;
using homework_5_SOLID.Domain.Models;

namespace homework_5_SOLID.Application
{
    public class GameSession(
        INumberGenerator numberGenerator,
        IGameUI gameUI, 
        IGameSettingsReader settingsReader, 
        IInputReader inputReader)
    {
        private readonly INumberGenerator _numberGenerator = numberGenerator;
        private readonly IGameUI _gameUI = gameUI;
        private readonly IGameSettingsReader _settingsReader = settingsReader;
        private readonly IInputReader _inputReader = inputReader;

        public void StartGame()
        {
            var settings = _settingsReader.GetGameSettings();
            var targetNumber = _numberGenerator.Generate(settings.MinValue, settings.MaxValue);
            var attemptsCounter = 0;
            while (true)
            {
                _gameUI.ShowPrompt(++attemptsCounter, settings.MaxAttempts);
                var guess = _inputReader.Read();

                EGuessResult guessResult;
                if (guess == targetNumber)
                    guessResult = EGuessResult.Guessed;
                else if (guess < targetNumber)
                    guessResult = EGuessResult.Less;
                else
                    guessResult = EGuessResult.More;

                switch (guessResult)
                {
                    case EGuessResult.Guessed:
                        _gameUI.ShowWin(targetNumber, attemptsCounter);
                        return;
                    default:
                        if (attemptsCounter == settings.MaxAttempts)
                        {
                            _gameUI.ShowLose(targetNumber, settings.MaxAttempts);
                            return;
                        }
                        _gameUI.ShowResult(guessResult, attemptsCounter, settings.MaxAttempts);
                        break;
                }
            }
        }
    }
}
