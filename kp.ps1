[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [Alias('n')]
    [string]$Name
)

# Strip .exe extension if the user includes it
$CleanName = $Name -replace '\.exe$', ''

if (Get-Process -Name $CleanName -ErrorAction SilentlyContinue) {
    Stop-Process -Name $CleanName -Force
    Write-Host "Successfully killed process: $CleanName" -ForegroundColor Green
} else {
    Write-Warning "Process '$CleanName' is not currently running."
}
