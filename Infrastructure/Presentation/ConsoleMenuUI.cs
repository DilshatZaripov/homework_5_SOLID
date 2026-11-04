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

        private readonly int _width = 30;

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
            builder.AppendLine();
            builder.Append(new string(' ', (_width - model.Title.Length) / 2));
            builder.AppendLine(model.Title);
            builder.AppendLine();
            builder.AppendLine(new string('*', _width));
            builder.AppendLine();
            for (int i = 1; i <= model.Items.Count; i++)
            {
                builder.AppendLine($"{i}. {model.Items[i - 1]}");
            }
            builder.AppendLine("Введите пункт меню:");

            return builder.ToString();
        }
    }
}
