Use DBParking

----------------------returns id of winner author by orderId, uses in ForEditOrder.cs

drop proc sp_UserIdentification  

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

Create Proc sp_UserIdentification 
@login nvarchar(50),
@pass nvarchar(500)

as
Select Users.UserId
From Users 
Where users.Login=@login and users.Pass=@pass
go


Exec sp_UserIdentification  'admin', 'admin'


go

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

