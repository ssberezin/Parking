using Parking.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parking.Helpes
{
    public class Library
    {
        IDialogService dialogService;
        IShowWindowService showWindow;

        public Library()
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
        }

        public byte[] CopyPhoto(byte[] obj)
        {
            byte[] result = new byte[obj.Length];
            for (int i = 0; i < obj.Length; i++)
                result[i] = obj[i];
            return result;
        }

        private static readonly Regex reg = new Regex(@"\D"); //regex that matches disallowed text
        public string PhoneNumberValidation(string ph)
        {
            if (ph is null)
                return "";

            ph = reg.Replace(ph, "");
            if (ph.Length > 13)
                ph = ph.Remove(ph.Length - 1, 1);
            return ph == "" ? "" : "+" + ph;
        }
        private static readonly Regex regTax = new Regex(@"\D"); //regex that matches disallowed text
        public string TaxCodeValidation(string code)
        {
            return code is null? "": regTax.Replace(code, "");
        }


        //this is for AddnewData methode in ParkPlaceWindowContext.cs 
        public  void GetPersonAndContactsIds (string phoneNumber, Person tmpPerson, out int? ctnId, out int? persId)
        {
            ctnId = null; persId = null;
            using (DBConteiner db = new DBConteiner())
            {
                try
                {                   
                    Contacts ctn = db.Contacts.Where(ct => ct.Phone == phoneNumber).FirstOrDefault();

                    if (ctn is null)
                    {
                        ctnId = 0;
                        persId = 0; ;                        
                    }
                    else
                    {
                        //here we'd to send user a message about existing some persone in db with tha same contacts 
                        Person tmpOwnerPerson = db.Persons.Where(pers => pers.PersonId == ctn.SomePerson.PersonId).FirstOrDefault();
                        if (tmpPerson.SecondName == tmpOwnerPerson.SecondName &&
                            tmpPerson.FirstName == tmpOwnerPerson.FirstName &&
                            tmpPerson.Patronimic == tmpOwnerPerson.Patronimic &&
                            tmpPerson.Sex == tmpOwnerPerson.Sex)
                        {
                            ctnId = ctn.ContactsId;
                            persId = ctn.SomePerson.PersonId;
                        }
                        else
                        {
                            if (dialogService.YesNoDialog("В системі вже є данні особи з цим номером телефону.\n" +
                                                     phoneNumber + "\n" + tmpPerson.SecondName +
                                                      "\n" + tmpPerson.FirstName + "\n" + tmpPerson.Patronimic + "\n\n" +
                                                      "Приймаэмо ці данні як основні ? "))
                            {
                                ctnId = ctn.ContactsId;
                                persId = ctn.SomePerson.PersonId;
                            }
                            else                            
                                dialogService.ShowMessage("Добре, тоді необхідно задати інший номер телефону. ");                                
                            
                        }
                    }


                    }
                catch (ArgumentNullException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (OverflowException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
            }            
        }

        public string GetUserData(int userId)
        {
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    User usver = db.Users.Where(us => us.UserId == userId).FirstOrDefault();
                    return usver.SomeEmployee.SomePerson.SecondName + " " + usver.SomeEmployee.SomePerson.FirstName + " " + usver.SomeEmployee.SomePerson.Patronimic;             


                }
                catch (ArgumentNullException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (OverflowException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
            }
            return null;
        }

        public Company GetCompanyData()
        {
            using (DBConteiner db = new DBConteiner())
            {
                try
                {   
                    return db.OwnerCompany.Where(co => co.CompanyId == 1).FirstOrDefault();
                }
                catch (ArgumentNullException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (OverflowException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
            }
            return null;
        }

        public ParkingPlaceRecord  GetPPRecord(int pPId)
        {
                      

            string sqlExpression = "sp_GetparkingPlacesRecord";

            var connectionString = ConfigurationManager.ConnectionStrings["ParkingDB"].ConnectionString;
            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

            using (SqlConnection connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter firstParam = new SqlParameter
                    {
                        ParameterName = "@ppId",
                        Value = pPId
                    };
                    command.Parameters.Add(firstParam);

                    SqlDataReader result = command.ExecuteReader();


                    if (result.HasRows)
                    {
                        ParkingPlaceRecord CurrentRecord = new ParkingPlaceRecord();

                        while (result.Read())
                        {
                            CurrentRecord = new ParkingPlaceRecord()
                            {
                                SomeParkingPlace = new ParkingPlace
                                {
                                    ParkingPlaceId = (int)result.GetValue(0),
                                    ParkPlaceNumber = (int)result.GetValue(1),
                                    FreeStatus = (bool)result.GetValue(2),
                                    Released = (bool)result.GetValue(3)
                                }
                            };


                            if (!CurrentRecord.SomeParkingPlace.FreeStatus.Value)
                            {

                                CurrentRecord.SomeClient = new Client
                                {
                                    ClientId = (int)result.GetValue(4),
                                    OrgName = result.GetValue(4) is System.DBNull ? (string)result.GetValue(7) + " " + (string)result.GetValue(8) + " " + (string)result.GetValue(7) : (string)result.GetValue(5)
                                };

                                CurrentRecord.SomePerson = new Person
                                {
                                    PersonId = (int)result.GetValue(6),
                                    SecondName = (string)result.GetValue(7),
                                    FirstName = (string)result.GetValue(8),
                                    Patronimic = (string)result.GetValue(9),
                                    Sex = (bool)result.GetValue(21)
                                };
                                CurrentRecord.FemaleOwnPers = !CurrentRecord.SomePerson.Sex;

                                CurrentRecord.SomeContacts = new Contacts
                                {
                                    ContactsId = (int)result.GetValue(10),
                                    Phone = (string)result.GetValue(11),
                                    Adress = result.GetValue(18) is System.DBNull ? null : (string)result.GetValue(18)
                                };
                                CurrentRecord.SomeVehicle = new Vehicle
                                {
                                    VehicleId = (int)result.GetValue(12),
                                    RegNumber = (string)result.GetValue(13),
                                    Color = (string)result.GetValue(14),
                                    VPhoto = result.GetValue(22) is System.DBNull ? null : (byte[])result.GetValue(22)

                                };
                                CurrentRecord.SomeParkingPlaceLog = new ParkingPlaceLog
                                {
                                    ParkingPlaceLogId = (int)result.GetValue(15),
                                    DeadLine = (DateTime)result.GetValue(16)
                                };
                                CurrentRecord.SomeVehicleType = new VehicleType
                                {
                                    VehicleTypeId = (int)result.GetValue(19),
                                    TypeName = (string)result.GetValue(20)
                                };


                                if (CurrentRecord.SomeClient.OrgName == null || CurrentRecord.SomeClient.OrgName == "не вказано")
                                    CurrentRecord.SomeClient.OrgName = CurrentRecord.SomePerson.SecondName + " " + CurrentRecord.SomePerson.FirstName + " " + CurrentRecord.SomePerson.Patronimic;

                                if (!(result.GetValue(18) is System.DBNull))
                                    CurrentRecord.SomePerson.TrustedPerson_Id = (int?)result.GetValue(17);

                                if (CurrentRecord.SomePerson.TrustedPerson_Id != null)
                                {

                                    GetTrustedPerson(CurrentRecord.SomePerson.TrustedPerson_Id.Value, out Person nPerson, out Contacts nContacts);
                                    CurrentRecord.TrustedPerson = new Person 
                                                                 { PersonId = nPerson.PersonId,
                                                                   SecondName  = nPerson.SecondName,
                                                                   FirstName = nPerson.FirstName,
                                                                   Patronimic = nPerson.Patronimic,
                                                                   Sex = nPerson.Sex,
                                                                   Photo = nPerson.Photo,
                                                                   TaxCode = nPerson.TaxCode
                                                                 };
                                    CurrentRecord.FemaleTrustPers = !CurrentRecord.TrustedPerson.Sex;
                                    CurrentRecord.TrContacts = new Contacts 
                                                                {
                                                                   ContactsId = nContacts.ContactsId,
                                                                   Phone = nContacts.Phone,
                                                                   Adress = nContacts.Adress
                                                                };

                                }
                                
                            }
                            else
                            {
                                //in case if parking place is free                               
                                CurrentRecord.SomeClient = new Client
                                {
                                    OrgDetals = "не задано",
                                    OrgName = "не задано",

                                };
                                CurrentRecord.SomePerson = new Person();
                                CurrentRecord.FemaleOwnPers = !CurrentRecord.SomePerson.Sex;
                                CurrentRecord.SomeContacts = new Contacts();
                                CurrentRecord.SomeVehicle = new Vehicle();

                                CurrentRecord.SomeParkingPlaceLog = new ParkingPlaceLog { DeadLine = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) };

                                CurrentRecord.SomeVehicleType = new VehicleType
                                {
                                    VehicleTypeId = 1,
                                    TypeName = "легкове авто"
                                };
                                CurrentRecord.TrustedPerson = new Person();
                                CurrentRecord.FemaleTrustPers = !CurrentRecord.TrustedPerson.Sex;
                                CurrentRecord.TrContacts = new Contacts();
                                DateTime dt = DateTime.Now;                                
                            }
                        };
                        return CurrentRecord;

                    }
                    else
                    {

                        dialogService.ShowMessage("Щось пішло не так при зчитуванні данних про паркувальні місця");
                    }
                }

                catch (ArgumentNullException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (OverflowException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }

            }
            return null;
        }

        private void GetTrustedPerson(int trustedPersId, out Person nPerson, out Contacts nContacts)
        {

            nPerson = new Person();
            nContacts = new Contacts();

            string sqlExpression = "sp_GetTrustedPerson";

            var connectionString = ConfigurationManager.ConnectionStrings["ParkingDB"].ConnectionString;
            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

            using (SqlConnection connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                try
                {

                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter firstParam = new SqlParameter
                    {
                        ParameterName = "@TrustPersId",
                        Value = trustedPersId
                    };

                    command.Parameters.Add(firstParam);


                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            nPerson.PersonId = trustedPersId;
                            nPerson.SecondName = (string)result.GetValue(0);
                            nPerson.FirstName = (string)result.GetValue(1);
                            nPerson.Patronimic = (string)result.GetValue(2);
                            nPerson.Sex = (bool)result.GetValue(5);


                            nContacts = new Contacts { ContactsId = (int)result.GetValue(3), Phone = (string)result.GetValue(4) };
                        };

                    }
                    else
                        dialogService.ShowMessage("Щось пішло не так при зчитуванні данних про паркувальні місця");
                }

                catch (ArgumentNullException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (OverflowException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
            }
        }


    }
}
