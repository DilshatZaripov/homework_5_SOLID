using homework_5_SOLID.Domain.Contracts.Settings;
using homework_5_SOLID.Domain.Models;
using System.Text.Json;

namespace homework_5_SOLID.Infrastructure.Persistence
{
    public class JsonSettingsRepository : IGameSettingsReader, IGameSettingsWriter
    {
        public JsonSettingsRepository()
        {
            if (!File.Exists(_settingsFile))
                Save(GetDefaultGameSettings());
        }

        private readonly string _settingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "GameSettings.json");

        public GameSettings GetDefaultGameSettings()
        {
            return new(0, 10, 5);
        }

        public GameSettings GetGameSettings()
        {
            var settingsJson = File.ReadAllText(_settingsFile);
            return JsonSerializer.Deserialize<GameSettings>(settingsJson) ?? GetDefaultGameSettings();
        }

        public void Save(GameSettings settings)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var settingsJson = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(_settingsFile, settingsJson);
        }
    }
}
