using Parking.Helpes;
using Parking.Model;
using Parking.Views.CompanyOps;
using Parking.Views.ParkPlacesOps;
using Parking.Views.PersonOperations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Parking.ViewModel.CompanyOps
{
   public class CompanyInfoContext:Helpes.ObservableObject
    {
        IDialogService dialogService;
        IShowWindowService showWindow;

        Company PreviousState { get; set; }
        Company FinalState { get; set; }

        private Company ownerCompany;
        public Company OwnerCompany
        {
            get { return ownerCompany; }
            set
            {
                if (ownerCompany != value)
                {
                    ownerCompany = value;
                    OnPropertyChanged(nameof(OwnerCompany));
                }
            }
        }

        public CompanyInfoContext()
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            OwnerCompany = new Company();
            DefaultDataLoad();
            PreviousState = SetState(OwnerCompany);

        }

        private void DefaultDataLoad()
        {

            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    if (db.OwnerCompany.Count() == 0)
                        return;
                    OwnerCompany = db.OwnerCompany.Find(1);
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


      

        private RelayCommand closeWindowCommand;
        public RelayCommand CloseWindowCommand => closeWindowCommand ?? (closeWindowCommand = new RelayCommand(
                    (obj) =>
                    {
                        FinalState = SetState(OwnerCompany);
                        if (!CompareState(PreviousState, FinalState))
                        {
                            if (dialogService.YesNoDialog("Зміни не було збережено. Зберегти?"))
                            {
                                SaveData();
                            } 
                            
                        }

                        showWindow.CloseWindow(obj as Window);
                    }
                    ));

        private RelayCommand saveDataCommand;
        public RelayCommand SaveDataCommand => saveDataCommand ?? (saveDataCommand = new RelayCommand(
                    (obj) =>
                    {
                        FinalState = SetState(OwnerCompany);
                        if (!CompareState(PreviousState, FinalState))
                        {
                            SaveData();
                        }
                        else
                            dialogService.ShowMessage("Вами не було внесено жодних змін");

                        
                    }
                    ));
        private void SaveData()
        {
            if (OwnerCompany.CompanyId == 0)
            {
                SaveNewData();
                PreviousState = SetState(OwnerCompany);
            }
            else
            {
                EditData();
                PreviousState = SetState(OwnerCompany);
            }
            dialogService.ShowMessage("Виконано");
        }
        private void SaveNewData()
        {
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    db.OwnerCompany.Add(OwnerCompany);
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

        private void EditData()
        {
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    Company cp = db.OwnerCompany.Find(OwnerCompany.CompanyId);
                    db.Entry(cp).State = EntityState.Modified;
                    cp.OrgName = OwnerCompany.OrgName;
                    cp.OrgAdress = OwnerCompany.OrgAdress;
                    cp.TaxCode = OwnerCompany.TaxCode;
                    cp.RegNumber = OwnerCompany.RegNumber;
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

        private Company SetState(Company cp)
        {
            return new Company 
            { CompanyId = cp.CompanyId ,
              TaxCode = cp.TaxCode,
              RegNumber= cp.RegNumber,
              OrgName = cp.OrgName,
              OrgAdress = cp.OrgAdress
            };
        }

        private bool CompareState(Company obj1, Company obj2)
        {

            bool OrgNameCompare = obj1.OrgName == obj2.OrgName;
            bool TaxCodcompare = obj1.TaxCode == obj2.TaxCode;
            bool RegNumCompare = obj1.RegNumber == obj2.RegNumber;
            bool AdressCompare = obj1.OrgAdress == obj2.OrgAdress;

            return OrgNameCompare==TaxCodcompare==RegNumCompare==AdressCompare;
        }
    }
}
