using homework_5_SOLID.Application;
using homework_5_SOLID.Domain.Contracts.Generation;
using homework_5_SOLID.Domain.Contracts.Input;
using homework_5_SOLID.Domain.Contracts.Settings;
using homework_5_SOLID.Domain.Contracts.UI;
using homework_5_SOLID.Infrastructure.Generators;
using homework_5_SOLID.Infrastructure.Persistence;
using homework_5_SOLID.Infrastructure.Presentation;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<IGameSettingsReader, JsonSettingsRepository>();
services.AddSingleton<IGameSettingsWriter, JsonSettingsRepository>();

services.AddTransient<INumberGenerator, RandomNumberGenerator>();
//services.AddTransient<INumberGenerator, SequentialNumberGenerator>();

services.AddTransient<IInputReader, ConsoleInputReader>();
services.AddTransient<IMenuUI, ConsoleMenuUI>();
services.AddTransient<IGameUI, ConsoleGameUI>();

services.AddTransient<GameSession>();
services.AddTransient<SettingsEditor>();
services.AddTransient<Application>();

using var serviceProvider = services.BuildServiceProvider();

var app = serviceProvider.GetRequiredService<Application>();

app.Run();
