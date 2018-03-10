Import-Module -Name ($PSScriptRoot + "\LocusHelper-Mockup.ps1")
Import-Module -Name ($PSScriptRoot + "\DbHelper.ps1")

$PartnerId = "654156"

$ClientId = "9300aee943490d96c73a5b177e5a2df6"
$ClientSecret = "5dcdfbec7590646170dcbfd1b627fb5d"
$Username = "VectorCuatroJapan"
$Password = "welcome"

# Common Workflow
# 1. Get the ComponentId
#    - https://api.locusenergy.com/v3/partners/{PartnerId}/components?nodeId={NodeId}
# 2. Get Component Data Available
#    - https://api.locusenergy.com/v3/components/{Component.id}/dataavailable
# 3. Get Component Data
#    - https://api.locusenergy.com/v3/components/54369/data?start=2014-01-01T00:00:00&end=2014-03-01T00:00:00&tz=US/Pacific&gran=monthly&fields=W_avg,Wh_sum


Write-Host "Loging In..."
$Authenticated = LocusAuthenticatePassword -ClientId $ClientId -ClientSecret $ClientSecret -Username $Username -Password $Password
Write-Host "Login Success."
$AccessToken = $Authenticated.access_token
$RefreshToken = $Authenticated.refresh_token

#Write-Host "AccessToken: $($AccessToken)"

Write-Host "Retrieving the Coomponents..."
$ComponentResponse = GetComponentsForPartner -AccessToken $AccessToken -PartnerId $PartnerId
if ($ComponentResponse.statusCode -ne 200) {
    Write-Error "Retrieve Components Fail."
    return
}
$Components = $ComponentResponse.components

$Components | ft