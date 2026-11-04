using homework_5_SOLID.Domain.Contracts.UI;
using homework_5_SOLID.Domain.Models;
using System.Text;

namespace homework_5_SOLID.Infrastructure.Presentation
{
    public class ConsoleGameUI : IGameUI
    {
        private static readonly int _width = 30;


        public void ShowPrompt(int currentAttempt, int maxAttempts)
        {
            Console.Clear();
            Console.WriteLine(BuildHeader("Угадай загаданное число"));
            Console.WriteLine(BuildFooter([$"Попытка {currentAttempt}/{maxAttempts}"]));
        }

        public void ShowResult(EGuessResult result, int currentAttempt, int maxAttempts)
        {
            Console.Clear();
            Console.WriteLine(BuildHeader($"Загаданное число {EGuessResultToString(result)}"));
            Console.WriteLine(BuildFooter([$"Попытка {currentAttempt}/{maxAttempts}"]));
        }

        public void ShowWin(int targetNumber, int attemptsUsed)
        {
            Console.Clear();
            Console.WriteLine(BuildHeader("! ПОБЕДА !"));
            Console.WriteLine(BuildFooter([
                $"Загаданное число: {targetNumber}",
                $"Попыток использовано: {attemptsUsed}"
                ]));
        }

        public void ShowLose(int targetNumber, int maxAttempts)
        {
            Console.Clear();
            Console.WriteLine(BuildHeader("Проигрыш"));
            Console.WriteLine(BuildFooter([
                $"Загаданное число: {targetNumber}",
                $"Попыток использовано: {maxAttempts}"
                ]));
        }

        private static string BuildHeader(string header)
        {
            StringBuilder builder = new();
            builder.AppendLine(new string('*', _width));
            builder.AppendLine();
            builder.Append(new string(' ', (_width - header.Length) / 2));
            builder.AppendLine(header);
            builder.AppendLine();
            builder.AppendLine(new string('*', _width));
            builder.AppendLine();

            return builder.ToString();
        }

        private static string BuildFooter(string[] parameters)
        {
            StringBuilder builder = new();
            builder.AppendLine();
            foreach (string parameter in parameters)
            {
                builder.AppendLine(parameter);
            }
            builder.AppendLine(new string('*', _width));
            builder.AppendLine();

            return builder.ToString();
        }

        private static string EGuessResultToString(EGuessResult guessResult)
        {
            return guessResult switch
            {
                EGuessResult.Guessed => "победа",
                EGuessResult.Less => "меньше",
                EGuessResult.More => "больше",
                _ => "ошибка обработки",
            };
        }
    }
}
