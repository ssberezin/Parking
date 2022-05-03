Use DBParking

----------------------returns main table for SecondWindow.xaml

drop proc sp_GetparkingPlacesRecord

Create proc sp_GetparkingPlacesRecord
@ppId int
as
Select PP.ParkingPlaceId '0_ParkingPlaceId', PP.ParkPlaceNumber '1_PlaceNumber', PP.FreeStatus '2_FreeStatus', PP.Released '3_WentInOrWentOut',
	 Cl.ClientId '4_ClientId', Cl.OrgName '5_OrgName',
	 Pers.PersonId '6_PersonId',   Pers.SecondName '7_SecondName', Pers.FirstName '8_FirstName', Pers.Patronimic '9_Patronimic',
	 Ctn.ContactsId '10_ContactsId', Ctn.Phone '11_Phone', Veh.VehicleId '12_VehicleId', Veh.RegNumber '13_VehicleRegNumber', Veh.Color '14_VehicleColor',
	 PPL.ParkingPlaceLogId  '15_PPLId', PPL.DeadLine '16_DeadLine', Pers.TrustedPerson_Id '17_TrustedPerson_Id', Ctn.Adress '18_DriversAdress',
	 VT.VehicleTypeId '19_VehicleTypeId', VT.TypeName  '20_VTypeName', Pers.Sex 'Pers_Sex'
From ParkingPlaces as PP 
left join Clients as Cl on PP.SomeClient_ClientId=Cl.ClientId
left Join People as Pers on Cl.PersonCustomer_PersonId = pers.PersonId
left join Vehicles as Veh on Cl.ClientId=Veh.ClientOwner_ClientId
left join Contacts as Ctn on Ctn.SomePerson_PersonId=Pers.PersonId
left join ParkingPlaceLogs as PPL on PPL.SomeParkingPlace_ParkingPlaceId=PP.ParkingPlaceId
join Vehicletypes as VT on VT.VehicleTypeId=Veh.SomeVehicleType_VehicleTypeId

Where PP.ParkingPlaceId = @ppId

execute sp_GetparkingPlacesRecord '1'

drop proc sp_GetTrustedPerson
create proc sp_GetTrustedPerson
@TrustPersId int
as
Select 
	 Pers.SecondName 'SecondName', Pers.FirstName 'FirstName', Pers.Patronimic 'Patronimic',
	 Ctn.ContactsId 'ContactsId', Ctn.Phone 'Phone', Pers.Sex 'TrustPersSex' 
From  People as Pers
join Contacts as Ctn on Ctn.SomePerson_PersonId=Pers.PersonId
Where  pers.PersonId=@TrustPersId

execute sp_GetTrustedPerson '1'

select *
From People 
where People.TrustedPerson_Id = 1


Create proc sp_GetparkingPlacesCount
as
Select count(ParkingPlaces.ParkingPlaceId)
from ParkingPlaces

execute sp_GetparkingPlacesCount

execute sp_GetparkingPlacesRecords

Create proc sp_GetAllParkingPlaces
as
Select *
from ParkingPlaces

execute sp_GetAllParkingPlaces
drop proc sp_GetEmployeesRecords

create proc sp_GetEmployeesRecords
as
Select Emp.EmployeeId  '0_EmployeeId', Emp.Salary '1_Salary', Emp.HireDate '2_HireDate', Emp.FireDate '3_FireDate', Emp.Description '4_Description',
	   pers.PersonId  '5_PersonId', pers.SecondName '6_SecondName', pers.FirstName '7_FirstName', pers.Patronimic '8_Patronimic', pers.Sex '9_Sex',
	   pers.DayBirthday '10_DayBirthday', pers.Photo '11_Photo',
	   ctn.ContactsId '12_ContactsId', ctn.Phone '13_Phone', ctn.Adress '14_Adress', users.AccessName '15_Status', emp.Position '16_Position'
From Employees as Emp
join People as pers on Emp.SomePerson_PersonId=Pers.PersonId
join Contacts as ctn on Pers.PersonId=Ctn.SomePerson_PersonId
left join Users on Users.SomeEmployee_EmployeeId = Emp.EmployeeId

execute sp_GetEmployeesRecords


--Create Proc sp_UserIdentification 
--(@login nvarchar(50),
--@pass nvarchar(500),
--@Id int Output
--)
--as
--Select @Id= UserId
--From Users 
--Where users.Login=@login and users.Pass=@pass
--go

--Create Proc sp_UserIdentification 
--@login nvarchar(50),
--@pass nvarchar(500)

--as
--Select Users.UserId
--From Users 
--Where users.Login=@login and users.Pass=@pass
--go


--Exec sp_UserIdentification  'admin', 'admin'


--go

--drop function sf_UserIdentification 

--Create Function sf_UserIdentification (@login nvarchar(50), @pass nvarchar(500))
--Returns int
--begin
--declare @result int
--set @result = (Select UserId
--From Users 
--Where users.Login=@login and users.Pass=@pass)
--return @result
--end

--print dbo.sf_UserIdentification  ('admin', 'admin')


--Select Users.UserId
--From Users
--where users.UserId= dbo.sf_UserIdentification  ('admin', 'admin')

use BerezinStaffDB

select * 
From Departments as dep
where dep.ParentDepartmentId=3