# DressForWeather
Проект, который будет помогать одеться по погоде

# Где что находится:

DressForWeather.WebAPI - основной проект, в котором реализованы REST API и основные алгоритмы.

DressForWeather.SharedModels - "внешние" модели, которые сервер принимает и возвращает.

DressForWeather.WebAPI.BackendModels - "внутренние" модели, которые используются только на стороне сервера.

DressForWeather.WebAPI.DbContexts - там находится контекст базы данных, описывающий .net сущностями таблицу в базе данных.

DressForWeather.WebAPI.PostgreMigrations - автосгенерированные миграции для базы данных PostgreSQL.

DressForWeather.WebAPI.Tests - интеграционные тесты для REST API, реализованного в DressForWeather.WebAPI.

см. README.md в папке DressForWeather.WebAPI