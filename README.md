# XmlContentTranslator

![Platform](https://img.shields.io/badge/platform-Windows-blue)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)
![UI](https://img.shields.io/badge/UI-WinForms-0A7E07)
![Languages](https://img.shields.io/badge/README-English%20%7C%20Русский-orange)

English | [Русский](#русский)

## English

### Overview

`XmlContentTranslator` is a Windows Forms desktop application for translating and preparing XML localization content.

It combines three practical workflows in a single tool:

- XML translation workspace for source and target localization files
- XML dictionary generation from WinForms designer files
- helper C# code generation for `ListView` and `DataGridView` column localization

The application is implemented as an MDI desktop utility and targets `.NET 8` on Windows.

### Why This Project

Working with XML localization files is usually fragmented across editors, scripts, and manual cleanup.  
`XmlContentTranslator` brings those tasks into one desktop workflow:

- open a source XML file and prepare a target file
- translate line by line with navigation and visual status
- keep project settings in one place
- generate additional localization assets from WinForms sources

### Key Features

- Open a source XML file and build a translation workspace
- Load an existing target XML file or create an empty target document
- Edit translations in a list and tree-based workflow
- Highlight untranslated and unchanged lines
- Jump to the next blank translation line
- Translate selected lines with built-in online translation services
- Switch between `GoogleWeb` and `YandexWeb`
- Generate localization XML from WinForms `.Designer.cs` files
- Generate helper code for `ListView` and `DataGridView` column naming
- Save project settings including languages, translation services, and UI language
- Use English and Russian UI dictionaries

### Main Windows

- `FrmTranslate`  
  Main translation workspace for XML files

- `FrmGenerateXML`  
  Generates localization XML from WinForms designer sources

- `FrmGenerateCode`  
  Generates helper code for column localization support

- `FrmSettings`  
  Configures source language, target language, and translation services

### Screenshots

Main translation workspace:

![Main window](screen/001.png)

Auxiliary workflow window:

![Additional window](screen/002.png)

### Translation Services

Built-in services:

- `GoogleWeb`
- `YandexWeb`

The available services are defined in the internal translation service registry.

### Project Structure

- `XmlContentTranslator/`  
  Main WinForms application

- `Shared/`  
  Shared project items and supporting infrastructure

- `CodeStyle/`  
  Local code style guides and templates used in the repository

- `samplecode/`  
  Reference and experimental code used during development

- `screen/`  
  Screenshots for documentation

### Requirements

- Windows
- .NET SDK 8.0 or newer
- Internet connection for online translation services

### Build

```powershell
dotnet build .\XmlContentTranslator.sln -v minimal
```

### Run

```powershell
dotnet run --project .\XmlContentTranslator\XmlContentTranslator.csproj
```

### Typical Workflow

1. Start the application.
2. Open an existing project or create a new one.
3. Open the `Translate` window.
4. Load a source XML file.
5. Optionally load an existing target XML file.
6. Select source and target languages.
7. Translate selected lines or edit target text manually.
8. Save the resulting target XML file.
9. Use `XML Generator` or `Code Generator` when auxiliary localization artifacts are needed.

### Technical Notes

- The application stores project-related settings and reuses them between sessions.
- Translation services are online services and their public behavior may change over time.
- Some infrastructure is connected through shared `.projitems`.
- UI dictionaries are copied to the output directory.

### Roadmap

- improve translation service diagnostics and logging
- extend XML format compatibility
- improve generated localization assets
- continue cleanup and unification of form code style

---

## Русский

### Обзор

`XmlContentTranslator` — это настольное приложение Windows Forms для перевода и подготовки XML-файлов локализации.

Оно объединяет три практических сценария в одном инструменте:

- рабочая область перевода исходных и целевых XML-файлов
- генерация XML-словарей из WinForms designer-файлов
- генерация вспомогательного C# кода для локализации столбцов `ListView` и `DataGridView`

Приложение реализовано как MDI-утилита и ориентировано на `.NET 8` под Windows.

### Зачем нужен проект

Работа с XML-файлами локализации обычно разбросана между редакторами, скриптами и ручной правкой.  
`XmlContentTranslator` собирает эти задачи в единый настольный сценарий:

- открыть исходный XML и подготовить целевой файл
- переводить построчно с навигацией и визуальной индикацией состояния
- хранить настройки проекта в одном месте
- генерировать дополнительные локализационные артефакты из WinForms-исходников

### Основные возможности

- Открытие исходного XML-файла и построение рабочей области перевода
- Загрузка существующего целевого XML-файла или автоматическое создание пустого целевого документа
- Редактирование перевода в списке и дереве
- Подсветка непереведённых и неизменённых строк
- Переход к следующей пустой строке перевода
- Перевод выбранных строк через встроенные онлайн-сервисы
- Переключение между `GoogleWeb` и `YandexWeb`
- Генерация локализационного XML по WinForms `.Designer.cs` файлам
- Генерация вспомогательного кода для именования столбцов `ListView` и `DataGridView`
- Сохранение настроек проекта, языков и сервисов перевода
- Поддержка английского и русского интерфейса

### Основные окна

- `FrmTranslate`  
  Основная рабочая область перевода XML-файлов

- `FrmGenerateXML`  
  Генерация локализационного XML из WinForms designer-исходников

- `FrmGenerateCode`  
  Генерация вспомогательного кода для локализации столбцов

- `FrmSettings`  
  Настройка исходного языка, целевого языка и сервисов перевода

### Скриншоты

Основное окно перевода:

![Основное окно](screen/001.png)

Дополнительное окно:

![Дополнительное окно](screen/002.png)

### Сервисы перевода

Встроенные сервисы:

- `GoogleWeb`
- `YandexWeb`

Доступные сервисы определяются внутренним реестром сервисов перевода.

### Структура проекта

- `XmlContentTranslator/`  
  Основное WinForms-приложение

- `Shared/`  
  Общие элементы проекта и вспомогательная инфраструктура

- `CodeStyle/`  
  Локальные правила оформления кода и шаблоны репозитория

- `samplecode/`  
  Справочный и экспериментальный код, используемый в разработке

- `screen/`  
  Скриншоты для документации

### Требования

- Windows
- .NET SDK 8.0 или новее
- Подключение к интернету для онлайн-сервисов перевода

### Сборка

```powershell
dotnet build .\XmlContentTranslator.sln -v minimal
```

### Запуск

```powershell
dotnet run --project .\XmlContentTranslator\XmlContentTranslator.csproj
```

### Типовой сценарий работы

1. Запустите приложение.
2. Откройте существующий проект или создайте новый.
3. Откройте окно `Translate`.
4. Загрузите исходный XML-файл.
5. При необходимости загрузите существующий целевой XML-файл.
6. Выберите исходный и целевой языки.
7. Переводите выбранные строки через сервис или редактируйте текст вручную.
8. Сохраните итоговый целевой XML-файл.
9. Используйте `XML Generator` или `Code Generator`, если нужны вспомогательные артефакты локализации.

### Технические примечания

- Приложение сохраняет настройки проекта и переиспользует их между запусками.
- Сервисы перевода являются онлайн-сервисами, и их публичное поведение может меняться со временем.
- Часть инфраструктуры подключается через общие `.projitems`.
- Файлы словарей интерфейса копируются в выходную директорию.

### Планы развития

- улучшение диагностики и логирования сервисов перевода
- расширение совместимости с XML-форматами
- развитие генерации локализационных артефактов
- дальнейшая унификация и очистка стиля форм
