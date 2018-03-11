function FromUtcEpocTime ([long]$UnixTime)
{
    $epoch = New-Object System.DateTime (1970, 1, 1, 0, 0, 0, [System.DateTimeKind]::Utc);
    return $epoch.AddSeconds($UnixTime);
}
$time = FromUtcEpocTime -UnixTime 1520758800
$date = Get-Date $time
Write-Host $date