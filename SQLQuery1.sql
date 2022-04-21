Use DBParking

----------------------returns id of winner author by orderId, uses in ForEditOrder.cs

drop proc sp_UserIdentification  

Create Proc sp_UserIdentification 
@login nvarchar(50),
@pass nvarchar(500)
as
Select UserId
From Users 
Where users.Login=@login and users.Pass=@pass
go

Exec sp_UserIdentification  'admin', 'admin'
go