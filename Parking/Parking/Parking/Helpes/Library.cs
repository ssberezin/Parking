using Parking.Model;
using System;
using System.Collections.Generic;
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

    }
}
