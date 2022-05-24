using Parking.Model;
using Parking.ViewModel.DictionaryOps;
using Parking.ViewModel.FilterOps;
using Parking.ViewModel.ReportOps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


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


        public bool CheckSqlConnection()
        {
            
            string servNameFromFile = ReadServerNameFromFile();
            string connectString = ConfigurationManager.ConnectionStrings["ParkingDB"].ToString();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectString);
            string serverNameInConString = builder.DataSource;
            //if (servNameFromFile == serverNameInConString)
            return servNameFromFile == serverNameInConString;

        }

        public void WriteServerNameToFile(string serverName)
        {
            string flePath = "ServerName.txt";
            using (FileStream fs = new FileStream(flePath,
            FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(serverName);
                }
            }
        }

        public string ReadServerNameFromFile()
        {
            string flePath = "ServerName.txt";
            string result;
            using (FileStream fs = new FileStream(flePath,FileMode.Open))
            {
                using (StreamReader sr =
                new StreamReader(fs))
                {
                    result= sr.ReadToEnd();
                }
            }
            return result;
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
            ctnId =0; persId = 0;
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
                            {
                                dialogService.ShowMessage("Добре, тоді необхідно задати інший номер телефону. ");
                                ctnId = null;
                            }
                            
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

        public VehicleColor GetVehicleColor()
        {
            using (DBConteiner db = new DBConteiner())
            {
                try                {
                    
                    return db.Colors.Where(c => c.ColorName == "-не задано-").FirstOrDefault();

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

                                CurrentRecord.VehColor = new VehicleColor 
                                {
                                    VehicleColorId = (int)result.GetValue(23),
                                    ColorName = (string)result.GetValue(14),
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

        public ParkingPlace GetPPByVehNumber(string vehNumber)
        {


            string sqlExpression = "sp_GetPPByVehNumber";

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
                        ParameterName = "@vehNumber",
                        Value = vehNumber
                    };
                    command.Parameters.Add(firstParam);

                    SqlDataReader result = command.ExecuteReader();


                    if (result.HasRows)
                    {
                        ParkingPlace Record = new ParkingPlace();

                        while (result.Read())
                        {

                            Record.ParkingPlaceId = (int)result.GetValue(0);
                            Record.ParkPlaceNumber = (int)result.GetValue(1);
                            Record.FreeStatus = (bool)result.GetValue(2);
                            Record.Released = (bool)result.GetValue(3);                            
                        }
                        return Record;

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

        //Check for existing color. If it doesn't exist in DB it'll be
       public void CheckColor(string vehColor, out int colId)
        {
            colId = 0;
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    VehicleColor col = db.Colors.Where(w => w.ColorName == vehColor).FirstOrDefault();
                    if (col is null)
                    {
                        VehicleColor newColor = new VehicleColor { ColorName = vehColor };
                        db.Colors.Add(newColor);
                        db.SaveChanges();
                        colId = newColor.VehicleColorId;
                    }
                    else
                        colId = col.VehicleColorId;
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

        public bool CheckAddVehicleColor(string colorName)
        {
            if (colorName == "-не задано-")
                return false;

            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    VehicleColor vehColor = db.Colors.Where(w => w.ColorName == colorName).FirstOrDefault();

                    if (vehColor is null)
                    {
                        db.Colors.Add(new VehicleColor { ColorName = colorName });
                        db.SaveChanges();
                        return true;
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
            return false;
        }

        public bool EditColor(string colorName, int id)
        {
            if (id == 1)//-не задано-
                return false;

            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    VehicleColor vehColor1 = db.Colors.Where(w => w.VehicleColorId == id).FirstOrDefault();
                    VehicleColor incomeColor = db.Colors.Where(w => w.ColorName == colorName).FirstOrDefault();
                    List<Vehicle> vehicles = db.Vehicles.Where(v => v.SomeVehicleColor.ColorName == colorName).ToList();
                    if (vehicles.Count() == 0)
                        return false;
                    if (incomeColor is null)
                    {
                        db.Entry(vehColor1).State = EntityState.Modified;
                        vehColor1.ColorName = colorName;
                        db.SaveChanges();
                        return true;
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
            return false;
        }



        //if false  - 'не можна видалити цю позицію або такого кольору не існує'
        // if null - 'Щось пішло не так з видалення або в\n БД э ТЗ з цим кольором.\n Не можна видалити цю позицію'
        public bool CheckDeleteVehicleColor(SomeType  obj)
        {
            if ( obj.Id == 1)//-не задано-
                return false;

            using (DBConteiner db = new DBConteiner())
            {
                try
                {

                    List <Vehicle> vehs = db.Vehicles.Where(v => v.SomeVehicleColor.VehicleColorId == obj.Id).ToList();
                    if (vehs.Count()==0)
                    {
                        db.Colors.Remove(db.Colors.Find(obj.Id));
                        db.SaveChanges();
                        return true;
                    }
                    else
                        return false;

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
            return false;
        }


        //Check for existing color. If it doesn't exist in DB it'll be
        public void CheckVehicleType(string vehTypeName, out int vTypeId)
        {
            vTypeId = 0;
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    VehicleType col = db.VehicleTypes.Where(w => w.TypeName == vehTypeName).FirstOrDefault();
                    if (col is null)
                    {
                        VehicleType newType = new VehicleType { TypeName = vehTypeName };
                        db.VehicleTypes.Add(new VehicleType { TypeName = vehTypeName });
                        db.SaveChanges();
                        vTypeId = newType.VehicleTypeId; 
                    }
                    else
                        vTypeId = col.VehicleTypeId;
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

        //if false  - 'не можна видалити цю позицію або такого кольору не існує'
        // if null - 'Щось пішло не так з видалення або в\n БД э ТЗ з цим кольором.\n Не можна видалити цю позицію'
        public bool CheckDeleteVehicleType(SomeType obj)
        {
            if (obj.Id == 1)//-не задано-
                return false;

            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    List <Vehicle> vehs = db.Vehicles.Where(v => v.SomeVehicleType.VehicleTypeId == obj.Id).ToList();
                    if (vehs.Count()==0)
                    {
                        db.VehicleTypes.Remove(db.VehicleTypes.Find(obj.Id));
                        db.SaveChanges();
                        return true;
                    }
                    else
                        return false;

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
            return false;
        }



        public bool CheckAddVehicleType(string vehTypeName)
        {
            if (vehTypeName == "-не задано-")
                return false;

            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    VehicleType vehType = db.VehicleTypes.Where(w => w.TypeName == vehTypeName).FirstOrDefault();
                    if (vehType is null)
                    {
                        db.VehicleTypes.Add(new VehicleType { TypeName = vehTypeName });
                        db.SaveChanges();
                        return true;
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
            return false;
        }

        public bool EditVehType(string typeName, int id)
        {
            if (id == 1)//-не задано-
                return false;

            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    VehicleType vehType = db.VehicleTypes.Where(w => w.VehicleTypeId == id).FirstOrDefault();
                    VehicleType incomeType = db.VehicleTypes.Where(w => w.TypeName == typeName).FirstOrDefault();
                    List<Vehicle> vehicles = db.Vehicles.Where(v => v.SomeVehicleType.TypeName == typeName).ToList();
                    if (vehicles.Count() == 0)
                        return false;
                    if (incomeType is null)
                    {
                        db.Entry(vehType).State = EntityState.Modified;
                        vehType.TypeName = typeName;
                        db.SaveChanges();
                        return true;
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
            return false;
        }

        //adding storage procedures to DB
        public void AddSP()
        {
            
            using (DBConteiner db = new DBConteiner())
            {
                if (db.Persons.Count() != 0)
                    return;
                try
                {

                    db.Database.ExecuteSqlCommand
                       (@"
                         Create proc sp_GetAllParkingPlaces
                            as
                            Select *
                            from ParkingPlaces              

                        ");

                    db.Database.ExecuteSqlCommand
                        (@"
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

                        ");

                    db.Database.ExecuteSqlCommand
                       (@"
                         create proc sp_GetTrustedPerson
                            @TrustPersId int
                            as
                            Select 
                              Pers.SecondName 'SecondName', Pers.FirstName 'FirstName', Pers.Patronimic 'Patronimic',
                              Ctn.ContactsId 'ContactsId', Ctn.Phone 'Phone', Pers.Sex 'TrustPersSex' 
                            From  People as Pers
                            join Contacts as Ctn on Ctn.SomePerson_PersonId=Pers.PersonId
                            Where  pers.PersonId=@TrustPersId         

                        ");

                    db.Database.ExecuteSqlCommand
                       (@"
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
                        ");

                    db.Database.ExecuteSqlCommand
                       (@"
                       create proc sp_GetPPByVehNumber
                        @vehNumber nvarchar (8)
                        as
                        Select pp.ParkingPlaceId '0_ParkingPlaceId', pp.ParkPlaceNumber '1_ParkPlaceNumber', pp.FreeStatus '2_Status', pp.Released '3_In/out'
                        From ParkingPlaces pp
                        join Vehicles veh on veh.ParkingPlace_ParkingPlaceId=pp.ParkingPlaceId
                        where veh.RegNumber = @vehNumber and pp.FreeStatus=0 and pp.Released=0
                         ");


                    db.Database.ExecuteSqlCommand
                       (@"
                         create proc sp_GetEmployeesRecords
                            as
                            Select Emp.EmployeeId  '0_EmployeeId', Emp.Salary '1_Salary', Emp.HireDate '2_HireDate', Emp.FireDate '3_FireDate', Emp.Description '4_Description',
                            pers.PersonId  '5_PersonId', pers.SecondName '6_SecondName', pers.FirstName '7_FirstName', pers.Patronimic '8_Patronimic', pers.Sex '9_Sex',
                            pers.DayBirthday '10_DayBirthday', pers.Photo '11_Photo',
                            ctn.ContactsId '12_ContactsId', ctn.Phone '13_Phone', ctn.Adress '14_Adress', users.AccessName '15_Status', 
                            EmpPos.PositionName '16_Position', EmpPos.EmployeePositionId '17_EmployeePositionId', users.Pass '18_Pass', users.Login '19_Login', Users.UserId  '20_UserId',
                            pers.TaxCode '21 TaxCode'

                            From Employees as Emp
                            join People as pers on Emp.SomePerson_PersonId=Pers.PersonId
                            join Contacts as ctn on Pers.PersonId=Ctn.SomePerson_PersonId
                            left join Users on Users.SomeEmployee_EmployeeId = Emp.EmployeeId
                            join EmployeePositionEmployees EEemp on EEemp.Employee_EmployeeId=Emp.EmployeeId
                            join EmployeePositions EmpPos on EmpPos.EmployeePositionId=EEemp.EmployeePosition_EmployeePositionId
                                      ");



                    db.Database.ExecuteSqlCommand
                       (@"
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

                        ");
                    db.Database.ExecuteSqlCommand
                     (@"
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

                         ");
                    db.Database.ExecuteSqlCommand
                    (@"
                     create proc sp_GetClientParkPlaces
                        @clId int
                        as
                        Select PP.ParkingPlaceId '0_ParkingPlaceId', pp.ParkPlaceNumber '1_ParkPlaceNumber', pp.FreeStatus '2_FreeStatus', pp.Released '3_Released'	  
                        From Clients Cl
                        join Vehicles veh on veh.ClientOwner_ClientId=Cl.ClientId
                        join ParkingPlaces PP on veh.ParkingPlace_ParkingPlaceId=pp.ParkingPlaceId
                        join ParkingPlaceLogs PPl on Ppl.SomeParkingPlace_ParkingPlaceId=ppl.ParkingPlaceLogId
                        where cl.ClientId=@clId and pp.FreeStatus = 0
                         ");
                    db.Database.ExecuteSqlCommand
                      (@"
                       create proc sp_GetPreviousStuffRecord
                        as
                        select emp.EmployeeId '0_EmployeeId', pers.SecondName +' '+pers.FirstName+' '+pers.Patronimic '1_PIB', emp.HireDate '2_HireDate', pers.PersonId '3_PersId', us.UserId '4_UserId'
                        From People Pers 
                        join Employees Emp on Pers.PersonId=Emp.SomePerson_PersonId
                        join Users us on us.SomeEmployee_EmployeeId=emp.EmployeeId
                        join ParkingPlaceLogs PPl on PPl.SomeUser_UserId=us.UserId
	                    Where emp.FireDate is null
                        Group by us.UserId, pers.PersonId, emp.EmployeeId ,pers.SecondName, pers.FirstName,pers.Patronimic
						                        , emp.HireDate
                            ");
                    db.Database.ExecuteSqlCommand
                       (@"
                            create proc sp_GetDetalesStuffRecord
                            @userId int,
	                        @startDate date,
							@endDate date
                                as
                                Select veh.VehicleId '0_VehicleId',veh.RegNumber '1_RegNumber', ppl.ParkingPlaceLogId '2_ParkingPlaceLogId', 
            					                    ppl.Money '3_Money', ppl.PayingDate '4_PayingDate', ppl.DeadLine '5_DeadLine'
                                From Vehicles veh
                                join ParkingPlaces PP on Veh.ParkingPlace_ParkingPlaceId = Pp.ParkingPlaceId
                                join ParkingPlaceLogs PPl on pp.ParkingPlaceId = Ppl.SomeParkingPlace_ParkingPlaceId
                                join Users Us on Ppl.SomeUser_UserId = us.UserId
                                 where us.UserId = @userId and ppl.Money>0 and ppl.PayingDate>=@startDate and ppl.PayingDate<=@endDate
                       ");

                    db.Database.ExecuteSqlCommand
                      (@"
                            create proc sp_TotalSumForPeriod                           
                                @startDate date,
                                @endDate date
                                as
                                Select Sum(ppl.Money)
                                From Vehicles veh
                                join ParkingPlaces PP on Veh.ParkingPlace_ParkingPlaceId = Pp.ParkingPlaceId
                                join ParkingPlaceLogs PPl on pp.ParkingPlaceId = Ppl.SomeParkingPlace_ParkingPlaceId
                                join Users Us on Ppl.SomeUser_UserId = us.UserId
                                where  ppl.Money>0 and ppl.PayingDate>=@startDate and ppl.PayingDate<=@endDate
                       ");

//-----------------------------------------------FOR FILTERING ------------------------------------------

                    db.Database.ExecuteSqlCommand
                     (@"
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

                       ");

                    db.Database.ExecuteSqlCommand
                     (@"
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


                       ");

                    db.Database.ExecuteSqlCommand
                    (@"
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


                       ");

                    db.Database.ExecuteSqlCommand
                  (@"
                         Create proc sp_GetVehPayDateMoney
                            @vehId int
                            as
                            select ppl.PayingDate, ppl.Money
                            From ParkingPlaceLogs ppl
                            Where ppl.PayingDate=(Select  max(ppl.PayingDate) 'PayingDate'
                            From Vehicles veh
                            join ParkingPlaces pp on veh.ParkingPlace_ParkingPlaceId=pp.ParkingPlaceId
                            join ParkingPlaceLogs ppl on pp.ParkingPlaceId=ppl.SomeParkingPlace_ParkingPlaceId
                            where veh.VehicleId=@vehId and ppl.Money!=0)
                       ");


                    db.SaveChanges();


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


        public decimal  GetTotalSum(DateTime startDate, DateTime endDate)
        {
                        
            string sqlExpression = "sp_TotalSumForPeriod";

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
                        ParameterName = "@startDate",
                        Value = startDate
                    };
                    SqlParameter secondParam = new SqlParameter
                    {
                        ParameterName = "@endDate",
                        Value = endDate
                    };
                    
                    command.Parameters.Add(firstParam);
                    command.Parameters.Add(secondParam);
                    

                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {                        
                        while (result.Read())
                        {
                            return result.GetValue(0) is DBNull?0: (decimal)result.GetValue(0);
                        };
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
            return 0;
        }

        public void GetLastVehicleDateAndPay(int vehicleId, out decimal  coast, out DateTime? lastDate )
        {
            lastDate = null; coast = 0;


            string sqlExpression = "sp_GetVehPayDateMoney";

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
                        ParameterName = "@vehId",
                        Value = vehicleId
                    };
                  
                    command.Parameters.Add(firstParam);                    


                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {                            
                            if (result.GetValue(0) is DBNull)
                                lastDate = null;
                            else
                                lastDate = (DateTime)result.GetValue(0);
                            coast = result.GetValue(1) is DBNull ? 0:(decimal)result.GetValue(1);
                        };
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

        //---------------------------FILTERING OPERATIONS ---------------------------------

        //---------------------------For first filter ---------------------------------


        public ObservableCollection<OwnerRecord> GetFiltered1OwnerRecordsAllStatuses( DateTime startDate, DateTime endDate)
        {
            ObservableCollection<OwnerRecord> tmpCollection = new ObservableCollection<OwnerRecord>();

            string sqlExpression = "sp_GetClientInfoForReportAllStatuses";

            var connectionString = ConfigurationManager.ConnectionStrings["ParkingDB"].ConnectionString;
            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

            using (SqlConnection connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                   
                    SqlParameter secondParam = new SqlParameter
                    {
                        ParameterName = "@startDate",
                        Value = startDate
                    };
                    SqlParameter thirdParam = new SqlParameter
                    {
                        ParameterName = "@endDate",
                        Value = endDate
                    };
                                        
                    command.Parameters.Add(secondParam);
                    command.Parameters.Add(thirdParam);


                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {

                        while (result.Read())
                        {
                            OwnerRecord rec = new OwnerRecord
                            {
                                ClientId = (int)result.GetValue(0),
                                OwnerName = result.GetValue(1) is DBNull ? "" : (string)result.GetValue(1),
                                PersonId = (int)result.GetValue(2),
                                MaxDeadLine = (DateTime)result.GetValue(4)
                            };


                            if (rec.OwnerName == "" || rec.OwnerName == "не задано")
                                rec.OwnerName = (string)result.GetValue(3);
                            tmpCollection.Add(rec);

                        };
                    }                    

                    return tmpCollection;
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

        public ObservableCollection<OwnerRecord> GetFiltered1OwnerRecordsByStatusAndDate(bool status, DateTime startDate, DateTime endDate)
        {
            ObservableCollection<OwnerRecord> tmpCollection = new ObservableCollection<OwnerRecord>();

            string sqlExpression = "sp_GetClientInfoForReportByStatus";

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
                        ParameterName = "@free",
                        Value = status
                    };
                    SqlParameter secondParam = new SqlParameter
                    {
                        ParameterName = "@startDate",
                        Value = startDate
                    };
                    SqlParameter thirdParam = new SqlParameter
                    {
                        ParameterName = "@endDate",
                        Value = endDate
                    };

                    command.Parameters.Add(firstParam);
                    command.Parameters.Add(secondParam);
                    command.Parameters.Add(thirdParam);

                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {

                        while (result.Read())
                        {
                            OwnerRecord rec = new OwnerRecord
                            {
                                ClientId = (int)result.GetValue(0),
                                OwnerName = result.GetValue(1) is DBNull ? "" : (string)result.GetValue(1),
                                PersonId = (int)result.GetValue(2),
                                MaxDeadLine = (DateTime)result.GetValue(4)
                            };


                            if (rec.OwnerName == "" || rec.OwnerName == "не задано")
                                rec.OwnerName = (string)result.GetValue(3);
                            tmpCollection.Add(rec);

                        };
                    }
                    

                    return tmpCollection;
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

        public ObservableCollection<OwnerRecord> GetDeptors( DateTime curDate)
        {
            ObservableCollection<OwnerRecord> tmpCollection = new ObservableCollection<OwnerRecord>();

            string sqlExpression = "sp_GetDeptors";

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
                        ParameterName = "@curDate",
                        Value = curDate
                    };

                    command.Parameters.Add(firstParam);                

                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {

                        while (result.Read())
                        {
                            OwnerRecord rec = new OwnerRecord
                            {
                                ClientId = (int)result.GetValue(0),
                                OwnerName = result.GetValue(1) is DBNull ? "" : (string)result.GetValue(1),
                                PersonId = (int)result.GetValue(2),
                                MaxDeadLine = (DateTime)result.GetValue(4)
                            };


                            if (rec.OwnerName == "" || rec.OwnerName == "не задано")
                                rec.OwnerName = (string)result.GetValue(3);
                            tmpCollection.Add(rec);

                        };
                    }
                   

                    return tmpCollection;
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


        //---------------------------------FOR SECOND FILTER IN REPORToPSwINDOW-----------------------------------

        public ObservableCollection<ParPlaceRecord> GetFilteredParPlaceRecords(bool check1, bool check2, int pNum, bool released, ObservableCollection<ParPlaceRecord> records)
        {
            ObservableCollection<ParPlaceRecord> tmpCollecton = new ObservableCollection<ParPlaceRecord>();

            if (check1)
            {
                tmpCollecton = FindByNum(pNum, records, tmpCollecton);
                if (tmpCollecton.Count() == 0)
                    return tmpCollecton;
            }
            if (check2)
            {
                tmpCollecton = FindByReleased(released, records, tmpCollecton);
                if (tmpCollecton.Count() == 0)
                    return tmpCollecton;
            }
            return tmpCollecton;
        }

        public ObservableCollection<ParPlaceRecord> FindByNum(int pNum, ObservableCollection<ParPlaceRecord> records, ObservableCollection<ParPlaceRecord> tmpRec1)
        {
            ObservableCollection<ParPlaceRecord> tmpRec2 = new ObservableCollection<ParPlaceRecord>();
            if (tmpRec1.Count() == 0)
            {
                foreach (ParPlaceRecord item in records)
                    if (item.PPlace.ParkPlaceNumber == pNum)
                        tmpRec1.Add(item);
            }
            else
            {
                foreach (ParPlaceRecord item in records)
                    if (item.PPlace.ParkPlaceNumber == pNum)
                        tmpRec2.Add(item);
                if (tmpRec2.Count() != 0)
                {
                    tmpRec1.Clear();
                    tmpRec1 = CopyRecords(tmpRec2);
                    tmpRec2.Clear();
                }
                else
                    return tmpRec2;

            }
            return tmpRec1;
        }

        public ObservableCollection<ParPlaceRecord> FindByReleased(bool released, ObservableCollection<ParPlaceRecord> records, ObservableCollection<ParPlaceRecord> tmpRec1)
        {
            ObservableCollection<ParPlaceRecord> tmpRec2 = new ObservableCollection<ParPlaceRecord>();
            if (tmpRec1.Count() == 0)
            {
                foreach (ParPlaceRecord item in records)
                    if (item.PPlace.Released == released)
                        tmpRec1.Add(item);
            }
            else
            {
                foreach (ParPlaceRecord item in records)
                    if (item.PPlace.Released == released)
                        tmpRec2.Add(item);
                if (tmpRec2.Count() != 0)
                {
                    tmpRec1.Clear();
                    tmpRec1 = CopyRecords(tmpRec2);
                    tmpRec2.Clear();
                }
                else
                    return tmpRec2;

            }
            return tmpRec1;
        }

        public static ObservableCollection<T> CopyRecords<T>(ObservableCollection<T> Records)
        {
            ObservableCollection<T> tmpRecords = new ObservableCollection<T>();
            foreach (T item in Records)
                tmpRecords.Add(item);
            return tmpRecords;
        }

        //---------------------------------FOR THIRD FILTER IN REPORTOPSWINDOW-----------------------------------

        public ObservableCollection<ReportOpsRecord> GetFilteredParPlaceRecords(FilterContext filter, DateTime startDay, DateTime endDay, ObservableCollection<ReportOpsRecord> records)
        {
            ObservableCollection<ReportOpsRecord> tmpCollecton = new ObservableCollection<ReportOpsRecord>();

            if (filter.CheckArg1)
            {
                tmpCollecton = FindByRegNumber(filter.StrArg1, records, tmpCollecton);                    
                if (tmpCollecton.Count() == 0)
                    return tmpCollecton;
            }
            if (filter.CheckArg2)
            {
                tmpCollecton = FindByDate(startDay,endDay, records, tmpCollecton);                    
                if (tmpCollecton.Count() == 0)
                    return tmpCollecton;
            }
            if (filter.CheckArg3)
            {
                tmpCollecton = FindByPIB(filter.StrArg2, records, tmpCollecton);
                if (tmpCollecton.Count() == 0)
                    return tmpCollecton;
            }
            return tmpCollecton;
        }

        public ObservableCollection<ReportOpsRecord> FindByRegNumber(string pNum, ObservableCollection<ReportOpsRecord> records, ObservableCollection<ReportOpsRecord> tmpRec1)
        {
            ObservableCollection<ReportOpsRecord> tmpRec2 = new ObservableCollection<ReportOpsRecord>();
            if (tmpRec1.Count() == 0)
            {
                foreach (ReportOpsRecord item in records)
                    if (item.VehicleNumber is null && pNum is null || (item.VehicleNumber !=null && pNum != null && item.VehicleNumber == pNum ))
                        tmpRec1.Add(item);                  
            }
            else
            {
                foreach (ReportOpsRecord item in records)
                    if (item.VehicleNumber is null && pNum is null || (item.VehicleNumber != null && pNum != null && item.VehicleNumber == pNum))
                        tmpRec2.Add(item);
                if (tmpRec2.Count() != 0)
                {
                    tmpRec1.Clear();
                    tmpRec1 = CopyRecords(tmpRec2);
                    tmpRec2.Clear();
                }
                else
                    return tmpRec2;

            }
            return tmpRec1;
        }

        
        public ObservableCollection<ReportOpsRecord> FindByDate(DateTime startDate, DateTime endDate, ObservableCollection<ReportOpsRecord> records, ObservableCollection<ReportOpsRecord> tmpRec1)
        {
            ObservableCollection<ReportOpsRecord> tmpRec2 = new ObservableCollection<ReportOpsRecord>();
            if (tmpRec1.Count() == 0)
            {
                foreach (ReportOpsRecord item in records)
                    if (item.EventDate>=startDate && item.EventDate<=endDate )
                        tmpRec1.Add(item);
            }
            else
            {
                foreach (ReportOpsRecord item in records)
                    if (item.EventDate >= startDate && item.EventDate <= endDate)
                        tmpRec2.Add(item);
                if (tmpRec2.Count() != 0)
                {
                    tmpRec1.Clear();
                    tmpRec1 = CopyRecords(tmpRec2);
                    tmpRec2.Clear();
                }
                else
                    return tmpRec2;

            }
            return tmpRec1;
        }

        public ObservableCollection<ReportOpsRecord> FindByPIB(string pib,  ObservableCollection<ReportOpsRecord> records, ObservableCollection<ReportOpsRecord> tmpRec1)
        {
            ObservableCollection<ReportOpsRecord> tmpRec2 = new ObservableCollection<ReportOpsRecord>();
            if (tmpRec1.Count() == 0)
            {
                foreach (ReportOpsRecord item in records)
                    if (item.UserData.Contains(pib))
                        tmpRec1.Add(item);
            }
            else
            {
                foreach (ReportOpsRecord item in records)
                    if (item.UserData.Contains(pib))
                        tmpRec2.Add(item);
                if (tmpRec2.Count() != 0)
                {
                    tmpRec1.Clear();
                    tmpRec1 = CopyRecords(tmpRec2);
                    tmpRec2.Clear();
                }
                else
                    return tmpRec2;

            }
            return tmpRec1;
        }

    }
}
