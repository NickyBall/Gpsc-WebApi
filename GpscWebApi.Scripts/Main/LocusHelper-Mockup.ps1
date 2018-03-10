function LocusAuthenticatePassword($ClientId, $ClientSecret, $Username, $Password) {
    $response = Get-Content "$($PSScriptRoot)\MockupResponse\AuthenticatedResult.json" | ConvertFrom-Json
    return $response
}

function GetComponentsForPartner($AccessToken, $PartnerId) {
    $response = Get-Content "$($PSScriptRoot)\MockupResponse\GetComponent.json" | ConvertFrom-Json
    return $response
}

function GetDataAvailableForComponent($AccessToken, $ComponentId) {
    $response = Get-Content "$($PSScriptRoot)\MockupResponse\GetDataAvailableForComponent.json" | ConvertFrom-Json
    return $response
}

function GetDataForComponent($AccessToken, $ComponentId) {
    $response = Get-Content "$($PSScriptRoot)\MockupResponse\GetDataForComponent.json" | ConvertFrom-Json
    return $response
}