using homework_5_SOLID.Domain.Models;

namespace homework_5_SOLID.Domain.Contracts.UI
{
    public interface IGameUI
    {
        void ShowPrompt(int currentAttempt, int maxAttempts);
        void ShowResult(EGuessResult result, int currentAttempt, int maxAttempts);
        void ShowWin(int targetNumber, int attemptsUsed);
        void ShowLose(int targetNumber, int maxAttempts);
    }
}
