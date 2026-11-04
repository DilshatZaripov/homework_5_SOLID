using homework_5_SOLID.Domain.Contracts.Input;
using homework_5_SOLID.Domain.Contracts.UI;
using homework_5_SOLID.Domain.Models;
using homework_5_SOLID.Infrastructure.Parsing;
using System.Text;

namespace homework_5_SOLID.Infrastructure.Presentation
{
    public sealed class ConsoleMenuUI(IInputReader inputReader) : IMenuUI
    {
        private readonly IInputReader _inputReader = inputReader;

        private readonly int _width = 40;

        public int ShowMenu(MenuModel model)
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine(BuildMenu(model));
                int itemNumber;
                try { itemNumber = InputParser.ParseInt(_inputReader.Read()); }
                catch (ArgumentException) { continue; }

                if (1 <= itemNumber && itemNumber <= model.Items.Count)
                    return itemNumber;
            }
        }

        private string BuildMenu(MenuModel model)
        {
            StringBuilder builder = new();
            builder.AppendLine(new string('*', _width));
            builder.AppendLine($"*{new string(' ', _width - 2)}*");
            int horizontalOffset = (_width - model.Title.Length) / 2 - 1;
            builder.AppendLine(
                $"*{new string(' ', horizontalOffset)}" +
                $"{model.Title}" +
                $"{new string(' ', _width - horizontalOffset - model.Title.Length - 2)}*");
            builder.AppendLine($"*{new string(' ', _width - 2)}*");
            builder.AppendLine(new string('*', _width));
            builder.AppendLine();
            for (int i = 1; i <= model.Items.Count; i++)
            {
                builder.AppendLine($" {i}. {model.Items[i - 1]}");
            }
            builder.AppendLine();
            builder.Append("Введите пункт меню и нажмите Enter:");

            return builder.ToString();
        }
    }
}
