create view PlantEnergyGenDailyView2017 as
select a.PlantId, avg(b.EnergyGen_Value) as AverageEnergyGenValue, Time_Stamp=DATEADD(day, DATEDIFF(day, 0, a.CreatedAt),0) from EnergyPlantHist as a join EnergyGen as b
on a.EnergyGenId = b.ID where a.CreatedAt >= '2017-01-01 00:00:00' and a.CreatedAt <= '2017-12-31 23:59:59'
group by DATEADD(day, DATEDIFF(day, 0, a.CreatedAt),0), a.PlantId