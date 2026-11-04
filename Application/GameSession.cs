using homework_5_SOLID.Domain.Contracts.Generation;
using homework_5_SOLID.Domain.Contracts.Input;
using homework_5_SOLID.Domain.Contracts.Settings;
using homework_5_SOLID.Domain.Contracts.UI;
using homework_5_SOLID.Domain.Models;
using homework_5_SOLID.Domain.Services;
using homework_5_SOLID.Infrastructure.Parsing;

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
                _gameUI.ShowPrompt(attemptsCounter + 1, settings.MaxAttempts);

                int guess;
                try { guess = InputParser.ParseInt(_inputReader.Read()); }
                catch (ArgumentException) { continue; }

                attemptsCounter++;
                var guessResult = GuessChecker.Check(targetNumber, guess);

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
