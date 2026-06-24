@echo off
setlocal

set "REPO_ROOT=%~dp0"
set "FRONTEND_DIR=%REPO_ROOT%src\web\architecture-governance-portal"
set "API_PROJECT=%REPO_ROOT%src\api\ArchitectureGovernance.Api\ArchitectureGovernance.Api.csproj"

echo Starting AI-Assisted Architecture Governance locally...
echo.

where dotnet >nul 2>nul
if errorlevel 1 (
    echo ERROR: .NET SDK was not found on PATH.
    echo Install .NET 8 SDK, then run this file again.
    pause
    exit /b 1
)

where npm >nul 2>nul
if errorlevel 1 (
    echo ERROR: npm was not found on PATH.
    echo Install Node.js 20, 21, or 22, then run this file again.
    pause
    exit /b 1
)

if not exist "%API_PROJECT%" (
    echo ERROR: Backend project was not found:
    echo %API_PROJECT%
    pause
    exit /b 1
)

if not exist "%FRONTEND_DIR%\package.json" (
    echo ERROR: Frontend package.json was not found:
    echo %FRONTEND_DIR%\package.json
    pause
    exit /b 1
)

if not exist "%FRONTEND_DIR%\node_modules" (
    echo Frontend dependencies are missing. Running npm install...
    pushd "%FRONTEND_DIR%"
    call npm install
    if errorlevel 1 (
        popd
        echo ERROR: npm install failed.
        pause
        exit /b 1
    )
    popd
    echo.
)

echo Backend API:     http://localhost:5080
echo Swagger:         http://localhost:5080/swagger
echo Frontend portal: http://localhost:4200
echo.
echo Opening separate command windows for backend and frontend.
echo Close those windows to stop the services.
echo.

start "Architecture Governance API" cmd /k "cd /d ""%REPO_ROOT%"" && set ""ASPNETCORE_ENVIRONMENT=Development"" && dotnet run --project ""%API_PROJECT%"" --urls http://localhost:5080"
start "Architecture Governance Portal" cmd /k "cd /d ""%FRONTEND_DIR%"" && npm start"

echo Services are starting. Wait for the backend window to show "Now listening on: http://localhost:5080".
echo Backend readiness check: http://localhost:5080/api/v1/platform/readiness
pause
