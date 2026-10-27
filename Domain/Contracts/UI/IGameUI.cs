namespace homework_5_SOLID.Domain.Contracts.UI
{
    public interface IGameUI
    {
        void ShowPrompt(int currentAttempt, int maxAttempts, int minValue, int maxValue);
        void ShowResult();
        void ShowWin();
        void ShowLose();
    }
}
