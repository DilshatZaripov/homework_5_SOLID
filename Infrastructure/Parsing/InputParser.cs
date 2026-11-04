namespace homework_5_SOLID.Infrastructure.Parsing
{
    public static class InputParser
    {
        /// <summary>
        /// Парсер для строки
        /// </summary>
        /// <param name="input">Строка для парса</param>
        /// <returns>Целое число из строки</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если входящяя строка не может преобразоваться в целое число</exception>
        public static int ParseInt(string input)
        {
            if (!int.TryParse(input, out int result))
                throw new ArgumentException("Can not parse input string to int", nameof(input));

            return result;
        }
    }
}
