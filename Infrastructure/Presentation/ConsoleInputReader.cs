using homework_5_SOLID.Domain.Contracts.Input;

namespace homework_5_SOLID.Infrastructure.Presentation
{
    internal class ConsoleInputReader : IInputReader
    {
        public string Read()
        {
            return Console.ReadLine() ?? string.Empty;
        }
    }
}
