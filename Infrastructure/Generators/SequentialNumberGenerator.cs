
using homework_5_SOLID.Domain.Contracts.Generation;

namespace homework_5_SOLID.Infrastructure.Generators
{
    public sealed class SequentialNumberGenerator : INumberGenerator
    {
        /// <summary>
        /// Метод генерации числа для игры
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>Всегда возвращает <paramref name="min"/></returns>
        public int Generate(int min, int max)
        {
            return min;
        }
    }
}
