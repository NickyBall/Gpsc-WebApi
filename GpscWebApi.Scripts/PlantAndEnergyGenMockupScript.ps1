﻿$StartDate = Get-Date -Year 2017 -Month 1 -Day 1 -Hour 0 -Minute 0 -Second 0 -Millisecond 0
$EndDate = Get-Date -Year 2018 -Month 1 -Day 1 -Hour 0 -Minute 0 -Second 0 -Millisecond 0
$Current = $StartDate
while ($Current -le $EndDate) {
    $Current = $Current.AddMinutes(15)
    $EnergyGenValue = Get-Random -Minimum 10000000 -Maximum 20000000
    InsertEnerygyGenTable -EnergyGenValue $EnergyGenValue -EnergyLatestUpdate $Current
    $Result = Invoke-Sqlcmd -Query "SELECT IDENT_CURRENT ('dbo.EnergyGen') AS Current_Identity;" -ConnectionString $ConnectionString
    $EnergyGenId = $Result[0]
    InsertPlantTable -EnergyGenId $EnergyGenId -CreatedBy $Current
    Write-Host "$($Current) Added with GenValue of $($EnergyGenValue)"
}