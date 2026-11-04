namespace homework_5_SOLID.Domain.Models
{
    /// <summary>
    /// Модель данных для отображения меню
    /// </summary>
    public sealed record MenuModel
    {
        /// <summary>
        /// Конструктор с валидацией данных
        /// </summary>
        /// <param name="title">Заголовок меню</param>
        /// <param name="items">Пункты меню</param>
        /// <exception cref="ArgumentException">Выкидывается исключение аргумента, если пунктов нет</exception>
        public MenuModel(string title, IReadOnlyList<string> items)
        {
            ArgumentNullException.ThrowIfNull(items);
            if (items.Count == 0)
                throw new ArgumentException("Menu must contain at least one item", nameof(items));

            Title = title;
            Items = items;
        }

        public string Title { get; init; }
        public IReadOnlyList<string> Items { get; init; }
    }
}
