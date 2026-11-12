# WebStore

Учебный проект интернет-магазина, разработанный для отработки навыков проектирования многослойной архитектуры на платформе ASP.NET Core.

> **Примечание:** Проект создан в 2021 году.

## Архитектура
Проект разделен на независимые слои (проекты в Solution), что обеспечивает слабую связанность и высокую тестируемость:

*   **`WebStore.Domain`** — сущности, DTO, модели представления.
*   **`WebStore.Data`** — доступ к базе данных (EF Core, SQLite / MS SQL), миграции.
*   **`WebStore.Services`** — бизнес-логика, типизированные HTTP-клиенты для общения с Web API.
*   **`WebStore.WebAPI`** — REST API контроллеры для получения данных и управления сущностями.
*   **`WebStore.UI`** — клиентская часть на ASP.NET Core MVC (Razor Pages, TagHelpers, ViewModels).
*   **`WebStore.Tests`** — юнит-тесты для проверки бизнес-логики (xUnit).
*   **`WebStore.Logger`** — кастомный провайдер логирования (Log4Net).

## Технологический стек
*   **Платформа:** C#, .NET, ASP.NET Core (MVC + Web API)
*   **ORM:** Entity Framework Core (Code-First, миграции)
*   **Аутентификация/Авторизация:** ASP.NET Core Identity (Cookies + Claims)
*   **Связь между слоями:** `HttpClient` через паттерн `AddTypedClient` (IHttpClientFactory)
*   **База данных:** SQLite / MS SQL Server
*   **Логирование:** Log4Net + Serilog (в зависимости от ветки)
*   **Тестирование:** xUnit, Moq
*   **CI/CD:** GitHub Actions (`.github/workflows/Testing.yml`)

## Основной функционал
*   Каталог товаров с фильтрацией по категориям и брендам.
*   Корзина покупок (с хранением состояния в Cookies).
*   Личный кабинет пользователя (регистрация, вход, роли).
*   Оформление заказов (разделение логики фронтенда и бэкенда через Web API).
*   Разделение на Admin-панель и клиентскую часть (Areas).

## Как запустить (если решите развернуть локально)
1. Установите .NET SDK (рекомендуется .NET 5 или 6).
2. Клонируйте репозиторий: `git clone https://github.com/Margarita-Kru/WebStore.git`
3. В файле `appsettings.json` укажите строку подключения к вашей БД (или используйте SQLite).
4. Примените миграции: `dotnet ef database update` (в проекте `WebStore.Data`).
5. Запустите проекты `WebStore.WebAPI` и `WebStore.UI` одновременно.

## Результаты тестирования

[![Testing](https://github.com/Margarita-Kru/WebStore/actions/workflows/Testing.yml/badge.svg)](https://github.com/Margarita-Kru/WebStore/actions/workflows/Testing.yml)
