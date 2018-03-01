$StartDate = Get-Date -Year 2016 -Month 1 -Day 1 -Hour 0 -Minute 0 -Second 0 -Millisecond 0
$EndDate = Get-Date -Year 2019 -Month 1 -Day 1 -Hour 0 -Minute 0 -Second 0 -Millisecond 0
$Current = $StartDate
while ($Current -le $EndDate) {
    $TargetValue = Get-Random -Minimum 10000000 -Maximum 20000000
    $Date = "$($Current.Year)-$("{0:d2}" -f $Current.Month)"
    InsertEnergyGenTarget -YearMonth $Date -Target $TargetValue
    $Current = $Current.AddMonths(1)
}