 use DbParking

 

 drop proc sp_GetparkingPlacesRecord

 create proc sp_GetparkingPlacesRecord                        
                            @ppId int
                            as
                            Select PP.ParkingPlaceId '0_ParkingPlaceId', PP.ParkPlaceNumber '1_PlaceNumber', PP.FreeStatus '2_FreeStatus', PP.Released '3_Released',
	                             Cl.ClientId '4_ClientId', Cl.OrgName '5_OrgName',
	                             Pers.PersonId '6_PersonId',   Pers.SecondName '7_SecondName', Pers.FirstName '8_FirstName', Pers.Patronimic '9_Patronimic',
	                             Ctn.ContactsId '10_ContactsId', Ctn.Phone '11_Phone', Veh.VehicleId '12_VehicleId', Veh.RegNumber '13_VehicleRegNumber', vehCol.ColorName '14_VehicleColor',
	                             PPL.ParkingPlaceLogId  '15_PPLId', PPL.DeadLine '16_DeadLine', Pers.TrustedPerson_Id '17_TrustedPerson_Id', Ctn.Adress '18_DriversAdress',
	                             VT.VehicleTypeId '19_VehicleTypeId', VT.TypeName  '20_VTypeName', Pers.Sex '21_Pers_Sex', Veh.VPhoto '22_VPhoto', vehCol.VehicleColorId '23_VehicleColorId'
                            From ParkingPlaces as PP 
							left join Vehicles as Veh on Veh.ParkingPlace_ParkingPlaceId=pp.ParkingPlaceId
                            left join Clients as Cl on veh.ClientOwner_ClientId=cl.ClientId
                            left Join People as Pers on Cl.PersonCustomer_PersonId = pers.PersonId                            
                            left join Contacts as Ctn on Ctn.SomePerson_PersonId=Pers.PersonId
                            left join ParkingPlaceLogs as PPL on PPL.SomeParkingPlace_ParkingPlaceId=PP.ParkingPlaceId
                            left join Vehicletypes as VT on VT.VehicleTypeId=Veh.SomeVehicleType_VehicleTypeId  
							left join VehicleColors vehCol on vehCol.VehicleColorId=veh.SomeVehicleColor_VehicleColorId
                            Where PP.ParkingPlaceId = @ppId

execute sp_GetparkingPlacesRecord '1'

           create proc sp_GetTrustedPerson
                            @TrustPersId int
                            as
                            Select 
                              Pers.SecondName 'SecondName', Pers.FirstName 'FirstName', Pers.Patronimic 'Patronimic',
                              Ctn.ContactsId 'ContactsId', Ctn.Phone 'Phone', Pers.Sex 'TrustPersSex' 
                            From  People as Pers
                            join Contacts as Ctn on Ctn.SomePerson_PersonId=Pers.PersonId
                            Where  pers.PersonId=@TrustPersId   



create proc sp_GetPPHistory
@clId int, 
@startDate date,
@endDate date,
@ppNumber int
as
Select PP.ParkPlaceNumber '1_ParkPlaceNumber', PPL.DateOfChange '2_DateOfEvent',  PPl.Released '4_Released'
From ParkingPlaces PP
join ParkingPlaceLogs PPl on PPL.SomeParkingPlace_ParkingPlaceId=PP.ParkingPlaceId
join Vehicles veh on veh.ParkingPlace_ParkingPlaceId = pp.ParkingPlaceId
join Clients Cl on Cl.ClientId=veh.ClientOwner_ClientId
where Cl.ClientId=@clId and Pp.ParkPlaceNumber= @ppNumber and Ppl.DateOfChange>=@startDate and ppl.DateOfChange<=@endDate

 create proc sp_GetPPByVehNumber
@vehNumber nvarchar (8)
as
Select pp.ParkingPlaceId '0_ParkingPlaceId', pp.ParkPlaceNumber '1_ParkPlaceNumber', pp.FreeStatus '2_Status', pp.Released '3_In/out'
From ParkingPlaces pp
join Vehicles veh on veh.ParkingPlace_ParkingPlaceId=pp.ParkingPlaceId
where veh.RegNumber = @vehNumber and pp.FreeStatus=0 and pp.Released=0

create proc sp_GetPPHistory
                        @clId int, 
                        @startDate date,
                        @endDate date,
                        @ppNumber int
                        as
                        Select PP.ParkPlaceNumber '1_ParkPlaceNumber', PPL.DateOfChange '2_DateOfEvent',  PPl.Released '4_Released'
                        From ParkingPlaces PP
                        join ParkingPlaceLogs PPl on PPL.SomeParkingPlace_ParkingPlaceId=PP.ParkingPlaceId
                        join Vehicles veh on veh.ParkingPlace_ParkingPlaceId = pp.ParkingPlaceId
                        join Clients Cl on Cl.ClientId=veh.ClientOwner_ClientId
                        where Cl.ClientId=@clId and Pp.ParkPlaceNumber= @ppNumber and Ppl.DateOfChange>=@startDate and ppl.DateOfChange<=@endDate


 create proc sp_GetClRepRecord
                        @ppId int,
                        @startDate date,
                        @endDate date
                        as
                        Select  veh.VehicleId '0_VehicleId',veh.RegNumber '1_RegNumber',
                        PPl.DateOfChange '2_DateOfChange'
                        ,Us.UserId '3_UserId' 
                        ,Pers.SecondName+' '+Pers.FirstName+' '+pers.Patronimic '4_PIB'
                        From Clients Cl                         
                        join Vehicles veh on veh.ClientOwner_ClientId=Cl.ClientId
                        join ParkingPlaces PP on pp.ParkingPlaceId=veh.ParkingPlace_ParkingPlaceId
                        join ParkingPlaceLogs PPl on Ppl.SomeParkingPlace_ParkingPlaceId=pp.ParkingPlaceId                         
                        join.Users us on ppl.SomeUser_UserId=us.UserId
                        join People Pers on cl.PersonCustomer_PersonId=pers.PersonId
                        where pp.ParkingPlaceId=@ppId and Ppl.DateOfChange>=@startDate and Ppl.DateOfChange <@endDate
                        

 create proc sp_GetClRepOnlyRecord
                        @ppId int,
                        @vehId int
                        as
                        Select  veh.VehicleId '0_VehicleId',veh.RegNumber '1_RegNumber',
                        PPl.DateOfChange '2_DateOfChange'
                        ,Us.UserId '3_UserId' 
                        ,Pers.SecondName+' '+Pers.FirstName+' '+pers.Patronimic '4_PIB'
                        From Clients Cl                         
                        join Vehicles veh on veh.ClientOwner_ClientId=Cl.ClientId
                        join ParkingPlaces PP on pp.ParkingPlaceId=veh.ParkingPlace_ParkingPlaceId
                        join ParkingPlaceLogs PPl on Ppl.SomeParkingPlace_ParkingPlaceId=pp.ParkingPlaceId                         
                        join.Users us on ppl.SomeUser_UserId=us.UserId
                        join People Pers on cl.PersonCustomer_PersonId=pers.PersonId
                        where pp.ParkingPlaceId=@ppId and veh.VehicleId=@vehId

exec sp_GetClRepOnlyRecord '2','1'


drop proc sp_GetClientInfoForReportByStatus
create proc sp_GetClientInfoForReportByStatus
@free bit,
@startDate date,
@endDate date
                            as
                            Select Cl.ClientId '0_ClientId' ,  Cl.OrgName '1_OrgName', Pers.PersonId '2_PersonId', Pers.SecondName + ' ' + pers.FirstName+' '+ pers.Patronimic '3_FIO',
                                  MAX(ppl.DeadLine) '4_Max deadline', pp.FreeStatus '5_FreeStatus'
                            From Clients Cl
                            join People Pers on Cl.PersonCustomer_PersonId=Pers.PersonId
							join Vehicles veh on veh.ClientOwner_ClientId=Cl.ClientId
                            join ParkingPlaces PP on pp.ParkingPlaceId=veh.ParkingPlace_ParkingPlaceId
                            join ParkingPlaceLogs PPl on PP.ParkingPlaceId=Ppl.SomeParkingPlace_ParkingPlaceId
                            where pp.FreeStatus = @free and ppl.DeadLine>=@startDate and ppl.DeadLine<=@endDate
                            group by Cl.ClientId ,  Cl.OrgName , Pers.PersonId , Pers.SecondName + ' ' + pers.FirstName+' '+ pers.Patronimic, pp.FreeStatus

							sp_GetClientInfoForReportByStatus '0', '2022.04.01','2022.05.20'

drop proc sp_GetClientInfoForReportAllStatuses
create proc sp_GetClientInfoForReportAllStatuses
@startDate date,
@endDate date
                            as
                            Select Cl.ClientId 'ClientId_0' ,  Cl.OrgName 'OrgName_1', Pers.PersonId 'PersonId_2', Pers.SecondName + ' ' + pers.FirstName+' '+ pers.Patronimic 'FIO_3',
                                  MAX(ppl.DeadLine) 'MaxDeadline_4', pp.FreeStatus 'FreeStatus_5'
                            From Clients Cl
                            join People Pers on Cl.PersonCustomer_PersonId=Pers.PersonId
							join Vehicles veh on veh.ClientOwner_ClientId=Cl.ClientId
                            join ParkingPlaces PP on pp.ParkingPlaceId=veh.ParkingPlace_ParkingPlaceId
                            join ParkingPlaceLogs PPl on PP.ParkingPlaceId=Ppl.SomeParkingPlace_ParkingPlaceId
                            where  ppl.DeadLine>=@startDate	 and ppl.DeadLine<=@endDate
                            group by Cl.ClientId ,  Cl.OrgName , Pers.PersonId , Pers.SecondName + ' ' + pers.FirstName+' '+ pers.Patronimic, pp.FreeStatus

sp_GetClientInfoForReportAllStatuses  '2022.04.01','2022.10.20'

drop proc sp_GetDeptors
create proc sp_GetDeptors
@curDate date
                            as
                            Select Cl.ClientId 'ClientId_0' ,  Cl.OrgName 'OrgName_1', Pers.PersonId 'PersonId_2', Pers.SecondName + ' ' + pers.FirstName+' '+ pers.Patronimic 'FIO_3',
                                  MAX(ppl.DeadLine) 'MaxDeadline_4', pp.FreeStatus 'FreeStatus_5'
                            From Clients Cl
                            join People Pers on Cl.PersonCustomer_PersonId=Pers.PersonId
							join Vehicles veh on veh.ClientOwner_ClientId=Cl.ClientId
                            join ParkingPlaces PP on pp.ParkingPlaceId=veh.ParkingPlace_ParkingPlaceId
                            join ParkingPlaceLogs PPl on PP.ParkingPlaceId=Ppl.SomeParkingPlace_ParkingPlaceId
                            where  ppl.DeadLine < @curDate and pp.FreeStatus=0
                            group by Cl.ClientId ,  Cl.OrgName , Pers.PersonId , Pers.SecondName + ' ' + pers.FirstName+' '+ pers.Patronimic, pp.FreeStatus

execute sp_GetDeptors '2022.10.20'

--Create temp table

--drop proc sp_FindByFIO

--create proc sp_FindByFIO
--@str nvarchar(100),
--@startDate date,
--@endDate date,
--@status bit           
--as
--CREATE TABLE #tmpTable (
--	ClientId_0 INT,
--	OrgName_1 VARCHAR(255),
--	PersonId_2 int,
--	FIO_3 VARCHAR(100),
--	MaxDeadline_4 Date,
--	FreeStatus_5 bit
--	);
--Insert into #tmpTable
--exec sp_GetClientInfoForReportByStatus @status, @startDate, @endDate
--Select *
--From #tmpTable
--Where FIO_3 like @str

--execute sp_FindByFIO '%Петров%','2022.04.01','2022.10.20','0'

--GO

--Storing values from procedure into temp table



create proc sp_GetClientInfoForReport
                            as
                            Select Cl.ClientId '0_ClientId' ,  Cl.OrgName '1_OrgName', Pers.PersonId '2_PersonId', Pers.SecondName + ' ' + pers.FirstName+' '+ pers.Patronimic '3_FIO',
                                  MAX(ppl.DeadLine) '4_Max deadline'
                            From Clients Cl
                            join People Pers on Cl.PersonCustomer_PersonId=Pers.PersonId
							join Vehicles veh on veh.ClientOwner_ClientId=Cl.ClientId
                            join ParkingPlaces PP on Pp.ParkingPlaceId=veh.ParkingPlace_ParkingPlaceId
                            join ParkingPlaceLogs PPl on PP.ParkingPlaceId=Ppl.SomeParkingPlace_ParkingPlaceId
                            where pp.FreeStatus = 0
                            group by Cl.ClientId ,  Cl.OrgName , Pers.PersonId , Pers.SecondName + ' ' + pers.FirstName+' '+ pers.Patronimic		