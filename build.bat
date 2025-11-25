@echo off
REM -----------------------------
REM Сборка всего решения RgrSharp через MSBuild с UTF-8
REM -----------------------------

REM Устанавливаем кодировку UTF-8 для консоли
chcp 65001 > nul

REM Файл решения (может быть .sln или .slnx)
set SOLUTION_FILE=%~dp0RgrSharp.slnx

REM Очистка старых сборок
echo Очистка решения...
msbuild "%SOLUTION_FILE%" /t:Clean /p:Configuration=Debug

REM Сборка всего решения
echo Сборка решения...
msbuild "%SOLUTION_FILE%" /t:Build /p:Configuration=Debug

REM Копирование конфигурационного файла
echo Копирование тарифов...
if exist "%~dp0Parking.ConsoleApp\tariffs.json" (
    copy "%~dp0Parking.ConsoleApp\tariffs.json" "%~dp0Parking.ConsoleApp\bin\Debug\net10.0\" /Y
) else (
    echo tariffs.json не найден, пропускаем копирование
)

REM Копирование папки plugins (опционально)
if exist "%~dp0Parking.ConsoleApp\plugins" (
    xcopy "%~dp0Parking.ConsoleApp\plugins" "%~dp0Parking.ConsoleApp\bin\Debug\net10.0\plugins" /E /I /Y
) else (
    echo Папка plugins не найдена, пропускаем
)

echo -----------------------------
echo Сборка завершена!
pause
