using homework_5_SOLID.Domain.Contracts.Generation;

namespace homework_5_SOLID.Infrastructure.Generators
{
    public sealed class RandomNumberGenerator : INumberGenerator
    {
        public int Generate(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}
