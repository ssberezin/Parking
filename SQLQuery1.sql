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