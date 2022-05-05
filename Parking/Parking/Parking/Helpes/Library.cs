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

        private static readonly Regex regex = new Regex("[^0-9+]+"); //regex that matches disallowed text
        public string PhoneNumberValidation(string ph)
        {

            //if (ph == null ||  ph.Length < 13) return null;
            //int len = ph.Length;
            //string tmp = null,
            //       plus = null;            

            //    if ((int)ph[0] == 43)//if first symbol is "+"
            //        tmp += ph[0];
            //    else
            //        return null;

            //    for (int i = 1; i < ph.Length; i++)
            //        if ((int)ph[i] >= 48 && (int)ph[i] <= 57)
            //            tmp += ph[i];
            //        else
            //        {
            //            if (ph[i] == '+')
            //                return null;
            //        }
            if (regex.IsMatch(ph))
            {
                ph= ph.Remove(ph.Length - 1, 1);
            }
            if (ph.Length > 1 && ph[ph.Length - 1] == '+')
            {
                 ph=ph.Remove(ph.Length - 1, 1);
            }

            return ph;
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

    }
}
