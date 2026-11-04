using homework_5_SOLID.Domain.Contracts.UI;
using homework_5_SOLID.Domain.Models;
using System.Text;

namespace homework_5_SOLID.Infrastructure.Presentation
{
    public class ConsoleGameUI : IGameUI
    {
        private static readonly int _width = 40;


        public void ShowPrompt(int currentAttempt, int maxAttempts)
        {
            Console.Clear();
            Console.WriteLine(BuildHeader("Угадай загаданное число"));
            Console.WriteLine(BuildFooter([$"Попытка {currentAttempt}/{maxAttempts}"]));
        }

        public void ShowResult(EGuessResult result, int guessNumber)
        {
            Console.Clear();
            Console.WriteLine(BuildHeader($"Загаданное число {EGuessResultToString(result)} чем {guessNumber}"));
            Console.WriteLine("Для продолжения нажмите на Enter");
            Console.ReadLine();
        }

        public void ShowWin(int targetNumber, int attemptsUsed)
        {
            Console.Clear();
            Console.WriteLine(BuildHeader("! ПОБЕДА !"));
            Console.WriteLine(BuildFooter([
                $"Загаданное число: {targetNumber}",
                $"Попыток использовано: {attemptsUsed}"
                ]));
            Console.WriteLine("Для выхода нажмите на Enter");
            Console.ReadLine();
        }

        public void ShowLose(int targetNumber, int maxAttempts)
        {
            Console.Clear();
            Console.WriteLine(BuildHeader("Проигрыш"));
            Console.WriteLine(BuildFooter([
                $"Загаданное число: {targetNumber}",
                $"Попыток использовано: {maxAttempts}"
                ]));
            Console.WriteLine("Для выхода нажмите на Enter");
            Console.ReadLine();
        }

        private static string BuildHeader(string header)
        {
            StringBuilder builder = new();
            builder.AppendLine(new string('*', _width));
            builder.AppendLine($"*{new string(' ', _width - 2)}*");
            int horizontalOffset = (_width - header.Length) / 2 - 1;
            builder.AppendLine(
                $"*{new string(' ', horizontalOffset)}" +
                $"{header}" +
                $"{new string(' ', _width - horizontalOffset - header.Length - 2)}*");
            builder.AppendLine($"*{new string(' ', _width - 2)}*");
            builder.Append(new string('*', _width));

            return builder.ToString();
        }

        private static string BuildFooter(string[] rows)
        {
            StringBuilder builder = new();
            foreach (string row in rows)
            {
                builder.AppendLine(
                    $"*{new string(' ', 3)}" +
                    $"{row}" +
                    $"{new string(' ' ,_width - row.Length - 5)}*");
            }
            builder.Append(new string('*', _width));

            return builder.ToString();
        }

        private static string EGuessResultToString(EGuessResult guessResult)
        {
            return guessResult switch
            {
                EGuessResult.Less => "меньше",
                EGuessResult.More => "больше",
                _ => "ошибка обработки",
            };
        }
    }
}
