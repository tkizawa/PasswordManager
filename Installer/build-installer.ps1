# Installer build helper for PasswordManagerApp
# Requires: .NET SDK and WiX Toolset Restore via NuGet PackageReference

$installerProject = Join-Path $PSScriptRoot 'PasswordManagerAppInstaller.wixproj'
Write-Host "Building MSI installer from project: $installerProject"

dotnet build $installerProject -c Release

if ($LASTEXITCODE -eq 0) {
    $msiPath = Join-Path $PSScriptRoot 'bin\Release\PasswordManagerAppInstaller.msi'
    if (Test-Path $msiPath) {
        Write-Host "Installer generated: $msiPath"
    } else {
        Write-Warning "ビルドは成功しましたが MSI が見つかりませんでした。Installer/bin/Release を確認してください。"
    }
} else {
    throw "MSI ビルドに失敗しました。ログを確認してください。"
}
