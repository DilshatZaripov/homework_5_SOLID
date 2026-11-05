# Архитектура проекта homework_5_SOLID

## Описание

Консольная игра «Угадай число» на .NET 10 (C# 14).  
Программа генерирует число, пользователь пытается его угадать. После каждой попытки выводится подсказка «больше» или «меньше». Количество попыток и диапазон чисел задаются через настройки, сохраняемые в JSON-файл.

Архитектура построена с применением принципов **SOLID** и разделением на слои по образцу **Clean Architecture / Ports & Adapters**.

---

## Обзор слоёв

Проект разделён на три слоя с чёткими границами ответственности:

| Слой | Роль | Зависит от |
|------|------|------------|
| **Domain** | Ядро: модели, контракты, чистая бизнес-логика. Не зависит ни от чего. | — |
| **Application** | Оркестрация use cases (сессия игры, редактор настроек, главное меню). | Domain (только интерфейсы) |
| **Infrastructure** | Технические детали: консоль, JSON, конкретные реализации. | Domain (реализует интерфейсы) |

**Правило зависимостей:** стрелки зависимостей направлены внутрь — от Infrastructure и Application к Domain. Domain ничего не знает о внешних слоях.

---

## Структура папок и файлов

```text
homework_5_SOLID/
 │
 ├── Domain/
 │   ├── Models/
 │   │   ├── GameSettings.cs
 │   │   ├── MenuModel.cs
 │   │   └── EGuessResult.cs
 │   │
 │   ├── Contracts/
 │   │   ├── Generation/
 │   │   │   └── INumberGenerator.cs
 │   │   ├── Input/
 │   │   │   └── IInputReader.cs
 │   │   ├── Settings/
 │   │   │   ├── IGameSettingsReader.cs
 │   │   │   └── IGameSettingsWriter.cs
 │   │   └── UI/
 │   │       ├── IGameUI.cs
 │   │       └── IMenuUI.cs
 │   │
 │   └── Services/
 │       └── GuessChecker.cs
 │
 ├── Application/
 │   ├── Application.cs
 │   ├── GameSession.cs
 │   └── SettingsEditor.cs
 │
 ├── Infrastructure/
 │   ├── Generators/
 │   │   ├── RandomNumberGenerator.cs
 │   │   └── SequentialNumberGenerator.cs
 │   │
 │   ├── Persistence/
 │   │   └── JsonSettingsRepository.cs
 │   │
 │   ├── Presentation/
 │   │   ├── ConsoleGameUI.cs
 │   │   ├── ConsoleMenuUI.cs
 │   │   └── ConsoleInputReader.cs
 │   │
 │   └── Parsing/
 │       └── InputParser.cs
 │
 ├── Program.cs
 └── homework_5_SOLID.csproj
```

---

## Описание файлов

### Domain/Models

| Файл | Ответственность |
|------|-----------------|
| `GameSettings.cs` | `record` с настройками игры: `MinValue`, `MaxValue`, `MaxAttempts`. Неизменяемый, с валидацией в конструкторе. |
| `MenuModel.cs` | `record` для описания меню: `Title` и `IReadOnlyList<string> Items`. Неизменяемый, с валидацией на непустой список. |
| `EGuessResult.cs` | `enum`: `Less`, `More`, `Guessed`. Результат одной попытки. |

### Domain/Contracts

| Файл | Ответственность |
|------|-----------------|
| `INumberGenerator.cs` | Контракт генерации числа: `int Generate(int min, int max)`. |
| `IInputReader.cs` | Контракт чтения сырого ввода: `string Read()`. |
| `IGameSettingsReader.cs` | Контракт чтения настроек: `GameSettings GetGameSettings()`, `GameSettings GetDefaultGameSettings()`. |
| `IGameSettingsWriter.cs` | Контракт записи настроек: `void Save(GameSettings settings)`. |
| `IGameUI.cs` | Контракт отображения игры: `ShowPrompt`, `ShowResult`, `ShowWin`, `ShowLose`. |
| `IMenuUI.cs` | Универсальный контракт отображения меню: `int ShowMenu(MenuModel model)`. Возвращает 1-based индекс выбранного пункта. Гарантирует обработку невалидного ввода без исключений. |

### Domain/Services

| Файл | Ответственность |
|------|-----------------|
| `GuessChecker.cs` | Чистая функция сравнения. Принимает загаданное число и попытку, возвращает `EGuessResult`. Без состояния, без зависимостей. |

### Application

| Файл | Ответственность |
|------|-----------------|
| `Application.cs` | Главный оркестратор. Крутит цикл приложения: показывает главное меню через `IMenuUI`, запускает `GameSession` или `SettingsEditor` по выбору пользователя. |
| `GameSession.cs` | Оркестратор одной партии. Владеет состоянием: загаданное число, счётчик попыток. Управляет циклом ввода-проверки. |
| `SettingsEditor.cs` | Оркестратор редактирования настроек. Показывает меню настроек, принимает изменения, использует `with` для создания новых `GameSettings` с валидацией, сохраняет через `IGameSettingsWriter`. |

### Infrastructure/Generators

| Файл | Ответственность |
|------|-----------------|
| `RandomNumberGenerator.cs` | Реализация `INumberGenerator` через `System.Random`. |
| `SequentialNumberGenerator.cs` | Реализация `INumberGenerator` для тестов. Всегда возвращает `min`. Полностью stateless, гарантирует предсказуемость и демонстрирует LSP. |

### Infrastructure/Persistence

| Файл | Ответственность |
|------|-----------------|
| `JsonSettingsRepository.cs` | Реализует `IGameSettingsReader` и `IGameSettingsWriter`. Читает и пишет настройки в JSON-файл через `System.Text.Json`. Создает директорию при первом запуске. |

### Infrastructure/Presentation

| Файл | Ответственность |
|------|-----------------|
| `ConsoleGameUI.cs` | Реализация `IGameUI` для консоли. Форматирует и выводит игровые сообщения. |
| `ConsoleMenuUI.cs` | Реализация `IMenuUI` для консоли. Отрисовывает любое меню из `MenuModel` и обеспечивает циклический запрос до получения валидного 1-based индекса. |
| `ConsoleInputReader.cs` | Реализация `IInputReader` через `Console.ReadLine() ?? string.Empty`. |

### Infrastructure/Parsing

| Файл | Ответственность |
|------|-----------------|
| `InputParser.cs` | Статический класс с методом `ParseInt`. Парсит строку в число. Не знает про консоль. Выбрасывает `ArgumentException` при невалидном вводе. |

### Корневые файлы

| Файл | Ответственность |
|------|-----------------|
| `Program.cs` | Composition Root. Создаёт DI-контейнер (`Microsoft.Extensions.DependencyInjection`), регистрирует зависимости с управлением временем жизни, запускает `Application`. |
| `homework_5_SOLID.csproj` | Файл проекта. Целевой фреймворк: `net10.0`. |

---

## Диаграмма зависимостей

```text
Program.cs (Composition Root)
    │
    └──► Application
            │
            ├── GameSession
            │       ├── INumberGenerator        (Domain)
            │       ├── IGameSettingsReader     (Domain)
            │       ├── IGameUI                 (Domain)
            │       └── IInputReader            (Domain)
            │
            ├── SettingsEditor
            │       ├── IGameSettingsReader     (Domain)
            │       ├── IGameSettingsWriter     (Domain)
            │       ├── IMenuUI                 (Domain)
            │       └── IInputReader            (Domain)
            │
            └── Application
                    ├── IMenuUI                 (Domain)
                    ├── GameSession             (Application)
                    └── SettingsEditor          (Application)

Infrastructure реализует интерфейсы из Domain:
    - RandomNumberGenerator     : INumberGenerator
    - SequentialNumberGenerator : INumberGenerator
    - JsonSettingsRepository    : IGameSettingsReader, IGameSettingsWriter
    - ConsoleGameUI             : IGameUI
    - ConsoleMenuUI             : IMenuUI
    - ConsoleInputReader        : IInputReader
```

## Маппинг SOLID

| Принцип | Где и как демонстрируется |
|---------|---------------------------|
| **SRP** (единственная ответственность) | Каждый класс имеет одну причину для изменения: `GameSession` — логика партии, `ConsoleGameUI` — отрисовка, `JsonSettingsRepository` — работа с JSON, `InputParser` — парсинг строки, `ConsoleInputReader` — чтение из консоли. |
| **OCP** (открытость/закрытость) | `INumberGenerator` — добавление `SequentialNumberGenerator` не требует правки `GameSession`. Новые режимы игры добавляются через новые реализации интерфейсов, а не через `switch` в существующих классах. |
| **LSP** (подстановка Лисков) | `RandomNumberGenerator` и `SequentialNumberGenerator` полностью взаимозаменяемы. `GameSession` не замечает подмены. Предусловия (`min < max`) и постусловия (число в диапазоне `[min, max]`) контракта соблюдаются в обеих реализациях. |
| **ISP** (разделение интерфейса) | Интерфейс настроек разделён на `IGameSettingsReader` (используется `GameSession`) и `IGameSettingsWriter` (используется `SettingsEditor`). Ни один клиент не зависит от методов, которые не использует. `IMenuUI` универсален и не дробится искусственно на `IMainMenuUI`/`ISettingsUI`. |
| **DIP** (инверсия зависимостей) | Высокоуровневые модули (`GameSession`, `SettingsEditor`, `Application`) зависят только от абстракций из `Domain/Contracts`. Конкретные реализации живут в `Infrastructure` и внедряются через DI-контейнер в `Program.cs`. |

---

## Ключевые архитектурные решения

1. **Владелец состояния партии — `GameSession`.** Загаданное число и счётчик попыток живут в сессии. Попытка тратится только после успешного парсинга ввода, что защищает от штрафов за опечатки.
2. **Универсальность `IMenuUI`.** Вместо плождения интерфейсов (`IMainMenuUI`, `ISettingsUI`), используется единый контракт, принимающий `MenuModel`. Это устраняет дублирование и соблюдает ISP.
3. **`GuessChecker` — чистая функция без интерфейса.** Логика сравнения тривиальна и не имеет альтернативных реализаций. Интерфейс здесь был бы паразитной абстракцией.
4. **Разделение `IInputReader` и `InputParser`.** Чтение из консоли и парсинг строки — разные причины для изменения (SRP). `InputParser` можно тестировать без моков консоли.
5. **Разделение `IGameSettingsReader` и `IGameSettingsWriter`.** Классический пример ISP: `GameSession` не должна знать о существовании методов записи.
6. **Composition Root в `Program.cs`.** Все `new` конкретных реализаций собраны в одной точке. Нигде в коде нет жёстких зависимостей от `Console`, `Random` или `System.Text.Json`.
7. **`record` для моделей.** `GameSettings` и `MenuModel` неизменяемы. Настройки фиксируются на момент старта или редактирования, а валидация в конструкторе гарантирует отсутствие некорректного состояния.
8. **Stateless `SequentialNumberGenerator`.** Генератор для тестов не хранит внутреннее состояние (`_order`), а всегда возвращает `min`. Это делает его поведение абсолютно предсказуемым и независимым от времени жизни (Transient/Singleton) в DI-контейнере.
 слоистая структура, DI, неизменяемые модели, современный `System.Text.Json` |