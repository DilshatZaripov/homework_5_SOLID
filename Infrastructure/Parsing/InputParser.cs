namespace homework_5_SOLID.Infrastructure.Parsing
{
    public static class InputParser
    {
        public static int ParseInt(string input)
        {
            if (!int.TryParse(input, out int result))
                throw new ArgumentException("Can not parse input string to int", nameof(input));

            return result;
        }
    }
}
