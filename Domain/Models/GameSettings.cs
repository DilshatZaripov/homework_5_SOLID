namespace homework_5_SOLID.Domain.Models
{
    /// <summary>
    /// Настройка игры
    /// </summary>
    public sealed record GameSettings
    {
        /// <summary>
        /// Конструктор с валидацией данных
        /// </summary>
        /// <param name="minValue">Минимальное значение загадываемого числа</param>
        /// <param name="maxValue">Максимальное значение загадываемого числа</param>
        /// <param name="maxAttempts">Максимальное число попыток</param>
        /// <exception cref="ArgumentException">Выбрасывается, если <paramref name="maxAttempts"/> меньше 1
        /// или <paramref name="minValue"/> больше либо равно <paramref name="maxValue"/></exception>
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
