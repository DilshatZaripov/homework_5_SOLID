using homework_5_SOLID.Domain.Models;

namespace homework_5_SOLID.Domain.Services
{
    public static class GuessChecker
    {
        public static EGuessResult Check(int targetValue, int inputValue)
        {
            if (targetValue < inputValue)
                return EGuessResult.Less;

            if (targetValue > inputValue)
                return EGuessResult.More;

            return EGuessResult.Guessed;
        }
    }
}
