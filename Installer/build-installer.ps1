# PasswordManagerApp Installer Build Script

# Set location to the script directory
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $ScriptDir

Write-Host "1. Publishing PasswordManagerApp..." -ForegroundColor Cyan

# Publish directory
$PublishDir = Join-Path $ScriptDir "publish"
if (Test-Path $PublishDir) {
    Remove-Item -Recurse -Force $PublishDir
}

# Run dotnet publish
# Targeting win-x64, framework-dependent (Framework-dependent is smaller, standard C# runtime required)
dotnet publish ..\PasswordManagerApp.csproj -c Release -o $PublishDir -r win-x64 --self-contained false

if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to publish the application."
    Exit 1
}

Write-Host "`n2. Locating Inno Setup compiler (ISCC.exe)..." -ForegroundColor Cyan

# Find ISCC.exe
$isccPath = ""
$searchPaths = @(
    "ISCC.exe",  # If in PATH
    "$env:LOCALAPPDATA\Programs\Inno Setup 6\ISCC.exe",
    "$env:ProgramFiles\Inno Setup 6\ISCC.exe",
    "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe"
)

foreach ($path in $searchPaths) {
    if ($path -eq "ISCC.exe") {
        $check = Get-Command "ISCC.exe" -ErrorAction SilentlyContinue
        if ($check) {
            $isccPath = $check.Source
            break
        }
    } elseif (Test-Path $path) {
        $isccPath = $path
        break
    }
}

if (-not $isccPath) {
    Write-Error "Inno Setup compiler (ISCC.exe) not found."
    Write-Host "Please make sure Inno Setup 6 is installed." -ForegroundColor Yellow
    Exit 1
}

Write-Host "Found ISCC.exe at: $isccPath" -ForegroundColor Green

Write-Host "`n3. Building EXE Installer..." -ForegroundColor Cyan

# Run ISCC
& $isccPath .\installer.iss

if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to compile the installer."
    Exit 1
}

$outputExe = Join-Path $ScriptDir "Output\PasswordManagerAppSetup.exe"
if (Test-Path $outputExe) {
    Write-Host "`nInstaller generated successfully!" -ForegroundColor Green
    Write-Host "Installer Path: $outputExe" -ForegroundColor Green
} else {
    Write-Error "Installer binary was not found in output folder."
    Exit 1
}
