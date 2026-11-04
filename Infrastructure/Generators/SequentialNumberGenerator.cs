
using homework_5_SOLID.Domain.Contracts.Generation;

namespace homework_5_SOLID.Infrastructure.Generators
{
    public sealed class SequentialNumberGenerator : INumberGenerator
    {
        private int _order = 0;

        public int Generate(int min, int max)
        {
            return min + _order <= max ? min + _order++ : max;
        }
    }
}
