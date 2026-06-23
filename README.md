```
# 🛒 AutoPartsStore — Интернет-магазин автозапчастей

Fullstack веб-приложение интернет-магазина на ASP.NET Blazor WebAssembly с авторизацией, ролевой моделью и управлением каталогом товаров.

---

## Стек технологий

| Компонент | Технология |
|-----------|-----------|
| Frontend + Backend | C#, ASP.NET Blazor (WebAssembly) |
| UI-компоненты | MudBlazor |
| ORM | Entity Framework Core (Lazy Loading) |
| База данных | SQLite |
| Авторизация | ASP.NET Core Identity |
| Платформа | .NET 10 |

---

## Функциональность

- Каталог товаров с карточками и фильтрацией
- Корзина покупателя
- Регистрация и авторизация пользователей (ASP.NET Identity)
- Ролевая модель (пользователь / администратор)
- Загрузка файлов (до 50 МБ)
- Автоматическое применение миграций при старте
- Начальное заполнение базы данных (Seed Data)
- Обработка 404 и ошибок через кастомные страницы

---

## Запуск проекта

### Требования

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Git

### Шаги

```bash
# 1. Клонировать репозиторий
git clone https://github.com/Ficus1/AutoPartsStore.git
cd AutoPartsStore

# 2. Запустить приложение
dotnet run
```

База данных `autoparts.db` создаётся и мигрируется автоматически при первом запуске.

Приложение доступно по адресу: **https://localhost:5001**

---

## Структура проекта

```
AutoPartsStore/
├── Components/         # Blazor-компоненты (страницы, UI)
│   └── Account/        # Компоненты авторизации (Identity UI)
├── Data/               # DbContext, модели, миграции, SeedData
├── wwwroot/            # Статические файлы (CSS, JS, изображения)
├── Program.cs          # Точка входа, конфигурация сервисов
├── appsettings.json    # Настройки приложения
└── autoparts.db        # SQLite база данных
```
