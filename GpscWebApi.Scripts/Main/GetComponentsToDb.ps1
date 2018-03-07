Import-Module -Name ($PSScriptRoot + "\LocusHelper.ps1")

$PartnerId = "654156"

$ClientId = "9300aee943490d96c73a5b177e5a2df6"
$ClientSecret = "5dcdfbec7590646170dcbfd1b627fb5d"
$Username = "VectorCuatroJapan"
$Password = "welcome"

Write-Host "Loging In..."
$Authenticated = LocusAuthenticatePassword -ClientId $ClientId -ClientSecret $ClientSecret -Username $Username -Password $Password
Write-Host "Login Success."
$AccessToken = $Authenticated.access_token
$RefreshToken = $Authenticated.refresh_token

Write-Host "Retrieving the Coomponents..."
$ComponentResponse = GetComponentsForPartner -AccessToken $AccessToken -PartnerId $PartnerId
if ($ComponentResponse.statusCode -ne 200) {
    Write-Error "Retrieve Components Fail."
    return
}
$Components = $ComponentResponse.components

$Components | ft