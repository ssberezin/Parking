Use DBParking

----------------------returns main table for SecondWindow.xaml

drop proc sp_GetparkingPlacesRecord

Create proc sp_GetparkingPlacesRecord
@ppId int
as
Select PP.ParkingPlaceId 'ParkingPlaceId', PP.ParkPlaceNumber 'PlaceNumber', PP.FreeStatus 'FreeStatus', PP.Released 'WentInOrWentOut',
	 Cl.ClientId 'ClientId', Cl.OrgName 'OrgName',
	 Pers.PersonId 'PersonId',   Pers.SecondName 'SecondName', Pers.FirstName 'FirstName', Pers.Patronimic 'Patronimic',
	 Ctn.ContactsId 'ContactsId', Ctn.Phone 'Phone', Veh.VehicleId 'VehicleId', Veh.RegNumber 'VehicleRegNumber', Veh.Color 'VehicleColor', Veh.TypeName 'VehicleTypeName',
	 PPL.ParkingPlaceLogId  'PPLId', PPL.DeadLine 'DeadLine', Pers.TrustedPerson_Id 'TrustedPerson_Id', Ctn.Adress 'DriversAdress'
From ParkingPlaces as PP 
left join Clients as Cl on PP.SomeClient_ClientId=Cl.ClientId
left Join People as Pers on Cl.PersonCustomer_PersonId = pers.PersonId
left join Vehicles as Veh on Cl.ClientId=Veh.ClientOwner_ClientId
left join Contacts as Ctn on Ctn.SomePerson_PersonId=Pers.PersonId
left join ParkingPlaceLogs as PPL on PPL.SomeParkingPlace_ParkingPlaceId=PP.ParkingPlaceId

Where PP.ParkingPlaceId = @ppId

execute sp_GetparkingPlacesRecord '1'

create proc sp_GetTrustedPerson
@TrustPersId int
as
Select 
	 Pers.SecondName 'SecondName', Pers.FirstName 'FirstName', Pers.Patronimic 'Patronimic',
	 Ctn.ContactsId 'ContactsId', Ctn.Phone 'Phone' 
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
Select Emp.EmployeeId  'EmployeeId', Emp.Salary 'Salary', Emp.HireDate 'HireDate', Emp.FireDate 'FireDate', Emp.Description 'Description',
	   pers.PersonId  'PersonId', pers.SecondName 'SecondName', pers.FirstName 'FirstName', pers.Patronimic 'Patronimic', pers.Male 'Male',
	   pers.Female 'Female', pers.DayBirthday 'DayBirthday', pers.Photo 'Photo',
	   ctn.ContactsId 'ContactsId', ctn.Phone 'Phone', ctn.Adress 'Adress', users.AccessName 'Status', emp.Position 'Position'
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