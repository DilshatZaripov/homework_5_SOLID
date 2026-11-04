namespace homework_5_SOLID.Domain.Models
{
    public sealed record GameSettings
    {
        public GameSettings(int minValue, int maxValue, int maxAttempts)
        {
            if (maxAttempts < 1)
                throw new ArgumentException("Max attemts must be more then 0", nameof(maxAttempts));
            if (minValue >= maxValue)
                throw new ArgumentException("Min value must be less then max value", nameof(minValue));

            MinValue = minValue;
            MaxValue = maxValue;
            MaxAttempts = maxAttempts;
        }

        public int MinValue { get; init; }
        public int MaxValue { get; init; }
        public int MaxAttempts { get; init; }
    }
}
