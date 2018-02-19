create view PlantEnergyGenHourlyView2017 as
select a.PlantId, avg(b.EnergyGen_Value) as AverageEnergyGenValue, DATEADD(hour, DATEDIFF(hour, 0, a.CreatedAt),0) as Time_Stamp from EnergyPlantHist as a join EnergyGen as b
on a.EnergyGenId = b.ID where a.CreatedAt >= '2017-01-01 00:00:00' and a.CreatedAt <= '2017-12-31 23:59:59'
group by DATEADD(hour, DATEDIFF(hour, 0, a.CreatedAt),0), a.PlantId

