using homework_5_SOLID.Domain.Models;

namespace homework_5_SOLID.Domain.Contracts.Settings
{
    public interface IGameSettingsReader
    {
        GameSettings GetGameSettigs();
    }
}
