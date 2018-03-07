function LocusAuthenticatePassword($ClientId, $ClientSecret, $Username, $Password) {
    $url = "https://api.locusenergy.com/oauth/token"
    $GrantType = "password"
    $Params =  @{
        "grant_type" = $GrantType;
        "client_id" = $ClientId;
        "client_secret" = $ClientSecret;
        "username" = $Username;
        "password" = $Password;
    }
    $response = Invoke-WebRequest -Uri $url -Method Post -Body $Params -ContentType "application/x-www-form-urlencoded"
    return $response | ConvertFrom-Json
}

function GetComponentsForPartner($AccessToken, $PartnerId) {
    $url = "https://api.locusenergy.com/v3/partners/$($PartnerId)/components"
    $HeaderParams = @{
        "Authorization" = "Bearer $($AccessToken)"
    }
    $response = Invoke-WebRequest -Uri $url -Method Get -Headers $HeaderParams
    return $response | ConvertFrom-Json
}