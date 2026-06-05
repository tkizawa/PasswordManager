param(
    [string]$PngFile = "lock_icon.png",
    [string]$IcoFile = "lock_icon.ico"
)

Add-Type -AssemblyName System.Drawing

# Load PNG file
$bitmap = [System.Drawing.Bitmap]::FromFile((Resolve-Path $PngFile))

# Create icon from bitmap
$icon = [System.Drawing.Icon]::FromHandle($bitmap.GetHicon())

# Save as ICO
$fileStream = [System.IO.File]::Create($IcoFile)
$icon.Save($fileStream)
$fileStream.Close()
$icon.Dispose()
$bitmap.Dispose()

Write-Host "✓ Converted $PngFile to $IcoFile"
Write-Host "✓ File size: $((Get-Item $IcoFile).Length) bytes"
