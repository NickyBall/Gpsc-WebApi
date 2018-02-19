use pms_dev

create partition function [PF_PlantDataYear](datetime) as range right for values (
N'2014-01-01T00:00:00', N'2015-01-01T00:00:00', N'2016-01-01T00:00:00',
N'2017-01-01T00:00:00', N'2018-01-01T00:00:00', N'2019-01-01T00:00:00'
)