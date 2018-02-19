create view PlantEnergyGenHourlyView as
select	ROW_NUMBER() OVER(ORDER BY DATEADD(hour, DATEDIFF(hour, 0, a.CreatedAt),0) ASC) AS Row, 
		a.PlantId, 
		avg(b.EnergyGen_Value) as AverageEnergyGenValue, 
		DATEADD(hour, DATEDIFF(hour, 0, a.CreatedAt),0) as Time_Stamp 
from EnergyPlantHist as a join EnergyGen as b
on a.EnergyGenId = b.ID
group by DATEADD(hour, DATEDIFF(hour, 0, a.CreatedAt),0), a.PlantId