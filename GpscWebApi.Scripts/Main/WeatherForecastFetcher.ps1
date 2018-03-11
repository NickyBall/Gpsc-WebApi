$app_id = "ca4877920e4c706e6087b469fff5af5b"

function Call5Day3Hour($lat, $lon) {
    $url = "http://api.openweathermap.org/data/2.5/forecast?lat=$($lat)&lon=$($lon)&appid=$($app_id)"
    $response = Invoke-WebRequest -Uri $url -Method Get | ConvertFrom-Json
    return $response
}

$weathers = Call5Day3Hour -lat 16.4282875 -lon 102.8339389
$weathers | ft