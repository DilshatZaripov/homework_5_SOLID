# Архитектура проекта homework_5_SOLID

## Описание

Консольная игра «Угадай число» на .NET 10 (C# 14).  
Программа генерирует случайное число, пользователь пытается его угадать. После каждой попытки выводится подсказка «больше» или «меньше». Количество попыток и диапазон чисел задаются через настройки.

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

```
homework_5_SOLID/
│
├── Domain/
│   ├── Models/
│   │   ├── GameSettings.cs
│   │   └── GuessResult.cs
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
│   │       ├── ISettingsUI.cs
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
│   │   ├── ConsoleSettingsUI.cs
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
| `GameSettings.cs` | `record GameSettings(int MinValue, int MaxValue, int MaxAttempts)`. Неизменяемая модель настроек. |
| `GuessResult.cs` | `enum GuessResult { Less, More, Guessed }`. Результат одной попытки. |

### Domain/Contracts

| Файл | Ответственность |
|------|-----------------|
| `INumberGenerator.cs` | Контракт генерации числа: `int Generate(int min, int max)`. |
| `IInputReader.cs` | Контракт чтения сырого ввода: `string ReadInput()`. |
| `IGameSettingsReader.cs` | Контракт чтения настроек: `GameSettings GetSettings()`. |
| `IGameSettingsWriter.cs` | Контракт записи настроек: `SaveSettings(GameSettings)`, `ResetToDefaults()`. |
| `IGameUI.cs` | Контракт отображения игры: приглашение, результат, победа, поражение. |
| `ISettingsUI.cs` | Контракт отображения настроек: меню настроек |
| `IMenuUI.cs` | Контракт отображения меню: главное меню. |

### Domain/Services

| Файл | Ответственность |
|------|-----------------|
| `GuessChecker.cs` | Чистая функция сравнения. Принимает загаданное число и попытку, возвращает `GuessResult`. Без состояния, без зависимостей. |

### Application

| Файл | Ответственность |
|------|-----------------|
| `Application.cs` | Главный оркестратор. Крутит цикл приложения: показывает главное меню, запускает `GameSession` или `SettingsEditor` по выбору пользователя. |
| `GameSession.cs` | Оркестратор одной партии. Владеет состоянием: загаданное число, счётчик попыток, текущие настройки. |
| `SettingsEditor.cs` | Оркестратор редактирования настроек. Показывает меню, принимает изменения, сохраняет через `IGameSettingsWriter`. |

### Infrastructure/Generators

| Файл | Ответственность |
|------|-----------------|
| `RandomNumberGenerator.cs` | Реализация `INumberGenerator` через `System.Random`. |
| `SequentialNumberGenerator.cs` | Реализация `INumberGenerator` для тестов: возвращает числа по порядку от `min` до `max`. |

### Infrastructure/Persistence

| Файл | Ответственность |
|------|-----------------|
| `JsonSettingsRepository.cs` | Реализует `IGameSettingsReader` и `IGameSettingsWriter`. Читает и пишет настройки в JSON-файл через `System.Text.Json`. |

### Infrastructure/Presentation

| Файл | Ответственность |
|------|-----------------|
| `ConsoleGameUI.cs` | Реализация `IGameUI` для консоли. |
| `ConsoleMenuUI.cs` | Реализация `IMenuUI` для консоли. |
| `ConsoleSettingsUI.cs` | Реализация `ISettingsUI` для консоли. |
| `ConsoleInputReader.cs` | Реализация `IInputReader` через `Console.ReadLine()`. |

### Infrastructure/Parsing

| Файл | Ответственность |
|------|-----------------|
| `InputParser.cs` | Парсит строку в число. Не знает про консоль — работает только со строкой. |

### Корневые файлы

| Файл | Ответственность |
|------|-----------------|
| `Program.cs` | Точка входа. Создаёт DI-контейнер (`Microsoft.Extensions.DependencyInjection`), регистрирует зависимости, запускает `Application`. |
| `homework_5_SOLID.csproj` | Файл проекта. Целевой фреймворк: `net10.0`. |

---

## Диаграмма зависимостей

```
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
                    ├── IGameSettingsReader     (Domain)
                    ├── IGameUI / IMenuUI       (Domain)
                    └── IInputReader            (Domain)

Infrastructure реализует интерфейсы из Domain:
    - RandomNumberGenerator     : INumberGenerator
    - SequentialNumberGenerator : INumberGenerator
    - JsonSettingsRepository    : IGameSettingsReader, IGameSettingsWriter
    - ConsoleGameUI             : IGameUI
    - ConsoleMenuUI             : IMenuUI
    - ConsoleInputReader        : IInputReader
```

---

## Маппинг SOLID

| Принцип | Где и как демонстрируется |
|---------|---------------------------|
| **SRP** (единственная ответственность) | Каждый класс имеет одну причину для изменения: `GameSession` — логика партии, `ConsoleGameUI` — отрисовка в консоли, `JsonSettingsRepository` — работа с JSON, `Application` — навигация по меню, `InputParser` — парсинг строки в число, `ConsoleInputReader` — чтение из консоли. |
| **OCP** (открытость/закрытость) | `INumberGenerator` — добавление `SequentialNumberGenerator` не требует правки `GameSession`. Новые режимы игры добавляются через новые реализации интерфейсов, а не через `switch` в существующих классах. |
| **LSP** (подстановка Лисков) | `RandomNumberGenerator` и `SequentialNumberGenerator` полностью взаимозаменяемы: `GameSession` не замечает подмены. Предусловия (`min < max`) и постусловия (число в диапазоне `[min, max]`) контракта `INumberGenerator` соблюдаются в обеих реализациях. |
| **ISP** (разделение интерфейса) | Интерфейс настроек разделён на `IGameSettingsReader` (используется `GameSession`) и `IGameSettingsWriter` (используется `SettingsEditor`). Ни один клиент не зависит от методов, которые не использует. UI-интерфейсы разделены на `IGameUI` и `IMenuUI` — у них разные причины для изменения. |
| **DIP** (инверсия зависимостей) | Высокоуровневые модули (`GameSession`, `SettingsEditor`, `Application`) зависят только от абстракций из `Domain/Contracts`. Конкретные реализации (консоль, JSON, `Random`) живут в `Infrastructure` и внедряются через DI-контейнер в `Program.cs`. Зависимости направлены внутрь — от деталей к абстракциям. |

---

## Ключевые архитектурные решения

1. **Владелец состояния партии — `GameSession`.** Загаданное число и счётчик попыток живут в сессии, а не передаются между сервисами. Это исключает рассинхронизацию состояния.

2. **`GuessChecker` — чистая функция без интерфейса.** Логика сравнения тривиальна и не имеет альтернативных реализаций. Интерфейс здесь был бы паразитной абстракцией.

3. **Разделение `IInputReader` и `InputParser`.** Чтение из консоли и парсинг строки — разные причины для изменения (SRP). `InputParser` можно тестировать без моков консоли.

4. **Разделение `IGameSettingsReader` и `IGameSettingsWriter`.** Классический пример ISP: `GameSession` не должна знать о существовании методов записи.

5. **Composition Root в `Program.cs`.** Все `new` конкретных реализаций собраны в одной точке. Нигде в коде нет жёстких зависимостей от `Console`, `Random` или `System.Text.Json`.

6. **`record GameSettings` — неизменяемый.** Настройки фиксируются на момент старта игры и не могут измениться в процессе, что гарантирует предсказуемость.

---

## Покрытие критериев оценки

| Критерий | Баллы | Где видно |
|----------|-------|-----------|
| SRP | 2 | Разделение `GameSession` / `ConsoleGameUI` / `JsonSettingsRepository` / `InputParser` / `ConsoleInputReader` |
| DIP | 1 | Все зависимости через интерфейсы, сборка в `Program.cs` через DI |
| ISP | 2 | `IGameSettingsReader` / `IGameSettingsWriter`, `IGameUI` / `IMenuUI` |
| OCP | 2 | `INumberGenerator` с двумя реализациями, расширение без правки сессии |
| LSP | 2 | Взаимозаменяемость `RandomNumberGenerator` и `SequentialNumberGenerator` |
| CodeStyle / архитектура | 1 | Чистая слоистая структура, DI, неизменяемые модели |