 use DbParking                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                

 execute sp_GetEmployeesRecords
 
 create proc sp_GetEmployeesRecords
 as
                        Select Emp.EmployeeId  '0_EmployeeId', Emp.Salary '1_Salary', Emp.HireDate '2_HireDate', Emp.FireDate '3_FireDate', Emp.Description '4_Description',
	                           pers.PersonId  '5_PersonId', pers.SecondName '6_SecondName', pers.FirstName '7_FirstName', pers.Patronimic '8_Patronimic', pers.Sex '9_Sex',
	                           pers.DayBirthday '10_DayBirthday', pers.Photo '11_Photo'
							   ,
	                           ctn.ContactsId '12_ContactsId', ctn.Phone '13_Phone', ctn.Adress '14_Adress',
							   users.AccessName '15_Status'
							   --, 
	         --                  EmpPos.PositionName '16_Position', EmpPos.EmployeePositionId '17_EmployeePositionId', users.Pass '18_Pass', users.Login '19_Login', Users.UserId  '20_UserId',
	         --                  pers.TaxCode '21 TaxCode'
	   
                        From Employees as Emp
                        join People as pers on Emp.SomePerson_PersonId=Pers.PersonId
                        join Contacts as ctn on Pers.PersonId=Ctn.SomePerson_PersonId
                        left join Users on Users.SomeEmployee_EmployeeId = Emp.EmployeeId
                        join EmployeePositions as EmpPos on EmpPos.SomeEmployee_EmployeeId =Emp.EmployeeId





drop proc sp_GetClientInfoForReport

create proc sp_GetClientInfoForReport
as
Select Cl.ClientId '0_ClientId' ,  Cl.OrgName '1_OrgName', Pers.PersonId '2_PersonId', Pers.SecondName + ' ' + pers.FirstName+' '+ pers.Patronimic '3_FIO',
					 MAX(ppl.DeadLine) '4_Max deadline'
From Clients Cl
join People Pers on Cl.PersonCustomer_PersonId=Pers.PersonId
join ParkingPlaces PP on Cl.ClientId=PP.SomeClient_ClientId
join ParkingPlaceLogs PPl on PP.ParkingPlaceId=Ppl.SomeParkingPlace_ParkingPlaceId
group by Cl.ClientId ,  Cl.OrgName , Pers.PersonId , Pers.SecondName + ' ' + pers.FirstName+' '+ pers.Patronimic			 


exec sp_GetClientInfoForReport

		


drop proc sp_GetClRepRecord
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
join ParkingPlaces PP on Cl.ClientId=Pp.SomeClient_ClientId
join ParkingPlaceLogs PPl on Ppl.SomeParkingPlace_ParkingPlaceId=ppl.ParkingPlaceLogId
join.Users us on ppl.SomeUser_UserId=us.UserId
join People Pers on cl.PersonCustomer_PersonId=pers.PersonId
join Vehicles Veh on cl.ClientId=Veh.ClientOwner_ClientId 
where pp.ParkingPlaceId=@ppId and Ppl.DateOfChange>=@startDate and Ppl.DateOfChange <@endDate



execute sp_GetClRepRecord '1', '2022.04.01','2022.05.15'

create proc sp_GetAllClRepRecords
@startDate date,
@endDate date
as
Select PP.ParkingPlaceId '0_ParkingPlaceId', pp.ParkPlaceNumber '1_ParkPlaceNumber', pp.FreeStatus '2_FreeStatus', pp.Released '3_Released'
	  ,PPl.DateOfChange '4_DateOfChange'
	   ,Us.UserId '5_UserId' 
	   ,Pers.SecondName+' '+Pers.FirstName+' '+pers.Patronimic '6_PIB', ppl.PayingDate '7_PayingDate', ppl.Money '8_Money'
	   ,veh.VehicleId '9_VehicleId', veh.RegNumber '10_RegNumber'
From Clients Cl
join ParkingPlaces PP on Cl.ClientId=Pp.SomeClient_ClientId
join ParkingPlaceLogs PPl on Ppl.SomeParkingPlace_ParkingPlaceId=ppl.ParkingPlaceLogId
join.Users us on ppl.SomeUser_UserId=us.UserId
join People Pers on cl.PersonCustomer_PersonId=pers.PersonId
join Vehicles Veh on cl.ClientId=Veh.ClientOwner_ClientId 
where  Ppl.PayingDate>=@startDate and Ppl.PayingDate <@endDate

execute sp_GetAllClRepRecords  '2022.05.01','2022.05.13'


drop proc sp_GetClientParkPlaces
create proc sp_GetClientParkPlaces
@clId int
as
Select PP.ParkingPlaceId '0_ParkingPlaceId', pp.ParkPlaceNumber '1_ParkPlaceNumber', pp.FreeStatus '2_FreeStatus', pp.Released '3_Released'	  
From Clients Cl
join ParkingPlaces PP on Cl.ClientId=Pp.SomeClient_ClientId
join ParkingPlaceLogs PPl on Ppl.SomeParkingPlace_ParkingPlaceId=ppl.ParkingPlaceLogId

where cl.ClientId=@clId and pp.FreeStatus = 0

execute sp_GetClientParkPlaces  '2'


 