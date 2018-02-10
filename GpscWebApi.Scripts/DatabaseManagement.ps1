$Password = '$$Dv781@pp'
$ConnectionString = "Data Source=kow.cloudapp.net;Initial Catalog=pms_dev;Persist Security Info=True;User ID=sa;Password=$($Password);MultipleActiveResultSets=True;Application Name=EntityFramework"

function InsertEnerygyGenTable() {
    [CmdletBinding()] 
    Param (
        [Parameter(Mandatory=$true, Position=1)]
        [string]$EnergyGenValue,
        [Parameter(Mandatory=$true, Position=2)]
        [string]$EnergyLatestUpdate
    )
    $QueryString = "INSERT INTO [dbo].[EnergyGen]
           ([EnergyGen_Value]
           ,[EnergyGen_LatestUpdate]
           ,[EnergyGen_Weather]
           ,[EnergyGen_Wind_String]
           ,[EnergyGen_Solarradiation]
           ,[EnergyGen_UV]
           ,[EnergyGen_Forecast_Url]
           ,[EnergyGen_History_Url]
           ,[EnergyGen_Icon]
           ,[EnergyGen_Icon_Url])
     VALUES
           ('$($EnergyGenValue)'
           ,'$($EnergyLatestUpdate)'
           ,'Weather'
           ,'Wind String'
           ,'Solarradiation'
           ,'UV'
           ,'http://www.forecast.com'
           ,'http://www.history.com'
           ,'http://via.placeholder.com/50x50'
           ,'http://via.placeholder.com/50x50')"
    Invoke-Sqlcmd -Query $QueryString -ConnectionString $ConnectionString
}

function InsertPlantTable() {
    [CmdletBinding()]
    Param(
        [Parameter(Mandatory=$true, Position=1)]
        [string]$EnergyGenId,
        [Parameter(Mandatory=$true, Position=2)]
        [string]$CreatedBy
    )
    $QueryString = "INSERT INTO [dbo].[Plant]
           ([CountryID]
           ,[CompanyID]
           ,[PlantTypeID]
           ,[EnergyGenID]
           ,[Location_Latitude]
           ,[Location_Longitude]
           ,[Power_Gen]
           ,[Plant_Layout_Path]
           ,[Irradiation]
           ,[AMB_Temp]
           ,[SharedHolder_Id]
           ,[SharedHolder_Percentage]
           ,[Electricity_Gen]
           ,[Customer_Id]
           ,[CreatedBy])
     VALUES
           (1
           ,5
           ,1
           ,'$($EnergyGenId)'
           ,11.2145
           ,99.2154
           ,3500000
           ,'LayoutPath'
           ,700.00
           ,33.40
           ,1
           ,100
           ,50
           ,1
           ,'$($CreatedBy)')"
    Invoke-Sqlcmd -Query $QueryString -ConnectionString $ConnectionString
}