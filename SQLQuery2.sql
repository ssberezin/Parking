 use DbParking

 create proc sp_GetparkingPlacesRecord--------------------------------------------------------
                            @ppId int
                            as
                            Select PP.ParkingPlaceId '0_ParkingPlaceId', PP.ParkPlaceNumber '1_PlaceNumber', PP.FreeStatus '2_FreeStatus', PP.Released '3_Released',
	                             Cl.ClientId '4_ClientId', Cl.OrgName '5_OrgName',
	                             Pers.PersonId '6_PersonId',   Pers.SecondName '7_SecondName', Pers.FirstName '8_FirstName', Pers.Patronimic '9_Patronimic',
	                             Ctn.ContactsId '10_ContactsId', Ctn.Phone '11_Phone', Veh.VehicleId '12_VehicleId', Veh.RegNumber '13_VehicleRegNumber', Veh.Color '14_VehicleColor',
	                             PPL.ParkingPlaceLogId  '15_PPLId', PPL.DeadLine '16_DeadLine', Pers.TrustedPerson_Id '17_TrustedPerson_Id', Ctn.Adress '18_DriversAdress',
	                             VT.VehicleTypeId '19_VehicleTypeId', VT.TypeName  '20_VTypeName', Pers.Sex '21_Pers_Sex', Veh.VPhoto '22_VPhoto'
                            From ParkingPlaces as PP 
							left join Vehicles as Veh on Veh.ParkingPlace_ParkingPlaceId=pp.ParkingPlaceId
                            left join Clients as Cl on veh.ClientOwner_ClientId=cl.ClientId
                            left Join People as Pers on Cl.PersonCustomer_PersonId = pers.PersonId                            
                            left join Contacts as Ctn on Ctn.SomePerson_PersonId=Pers.PersonId
                            left join ParkingPlaceLogs as PPL on PPL.SomeParkingPlace_ParkingPlaceId=PP.ParkingPlaceId
                            left join Vehicletypes as VT on VT.VehicleTypeId=Veh.SomeVehicleType_VehicleTypeId  
							join    
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