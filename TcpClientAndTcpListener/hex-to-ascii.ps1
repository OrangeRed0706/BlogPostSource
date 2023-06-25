# hex-to-ascii.ps1
param (
    [Parameter(Mandatory=$false)]
    [string]$hex
)

if (-not $hex) {
    Write-Host "No argument supplied. Please provide hex string."
    exit 1
}

$bytes = [System.Convert]::FromHexString($hex)
$ascii = [System.Text.Encoding]::ASCII.GetString($bytes)
Write-Host $ascii

#Example:
#.\hex-to-ascii.ps1 -hex #"53657276657220726563656976656420796f7572206d65737361676521"
#Server received your message!