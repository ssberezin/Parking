using Parking.Helpes;
using Parking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel.DictionaryOps
{
    public class DictOpsContext: Helpes.ObservableObject
    {

        IDialogService dialogService;
        IShowWindowService showWindow;
        public ObservableCollection<SomeType> SomeTypes { get; set; }

        private SomeType selectedType;
        public SomeType SelectedType
        {
            get { return selectedType; }
            set
            {
                if (selectedType != value)
                {
                    selectedType = value;
                    OnPropertyChanged(nameof(SelectedType));
                }
            }
        }

        private string winTitle;
        public string WinTitle
        {
            get { return winTitle; }
            set
            {
                if (winTitle != value)
                {
                    winTitle = value;
                    OnPropertyChanged(nameof(WinTitle));
                }
            }
        }

        private string someText;
        public string SomeText
        {
            get { return someText; }
            set
            {
                if (someText != value)
                {
                    someText = value;
                    OnPropertyChanged(nameof(SomeText));
                }
            }
        }

        Library lib;

        bool IsColor { get; set; }
        bool IsVehType { get; set; }

        public DictOpsContext() {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
        }

        public DictOpsContext(IColorTypeInterface obj)
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            SomeTypes = new ObservableCollection<SomeType>();
            lib = new Library();
            DefaultDataLoad(obj);

        }

        private void DefaultDataLoad(IColorTypeInterface obj)
        {
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    SomeTypes.Clear();

                    if (obj is VehicleColor)
                    {
                        var tmp = db.Colors.ToList();
                        foreach (VehicleColor item in tmp)
                            SomeTypes.Add(new SomeType { Id = item.VehicleColorId, SomeName=item.ColorName});                        
                        IsColor = true;
                        WinTitle = "Редагування кольорів";                        
                        return;
                    }
                    if (obj is VehicleType)
                    {
                        var tmp = db.VehicleTypes.ToList();
                        foreach (VehicleType item in tmp)
                            SomeTypes.Add(new SomeType { Id = item.VehicleTypeId, SomeName = item.TypeName });
                        IsVehType = true;
                        WinTitle = "Редагування типів ТЗ";                      

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

       

        private RelayCommand saveDataCommand;
        public RelayCommand SaveDataCommand => saveDataCommand ?? (saveDataCommand = new RelayCommand(
                    (obj) =>
                    {
                        SomeText = obj as string;
                        if (IsVehType)
                        {
                            if (lib.CheckAddVehicleType(SomeText))
                            {
                                DefaultDataLoad(new VehicleType());
                                dialogService.ShowMessage("Виконано");
                            }
                            else
                                dialogService.ShowMessage("Не можна додати цю позіцію");
                            return;
                        }
                        if (IsColor)
                        {
                            if (lib.CheckAddVehicleColor(SomeText))
                            {
                                DefaultDataLoad(new VehicleColor());
                                dialogService.ShowMessage("Виконано");
                            }
                            else
                                dialogService.ShowMessage("Не можна додати цю позіцію");
                            return;
                        }
                    }
                    ));

        private RelayCommand editDataCommand;
        public RelayCommand EditDataCommand => editDataCommand ?? (editDataCommand = new RelayCommand(
                    (obj) =>
                    {
                        SomeText = obj as string;
                        if (IsVehType)
                        {
                            if (lib.EditVehType(SomeText, SelectedType.Id))
                            {
                                DefaultDataLoad(new VehicleType());
                                dialogService.ShowMessage("Виконано");
                            }
                            else
                                dialogService.ShowMessage("Не можна редагувати цю позіцію");
                            return;
                        }
                        if (IsColor)
                        {
                            if (lib.EditColor(SomeText, SelectedType.Id))
                            {
                                DefaultDataLoad(new VehicleColor());
                                dialogService.ShowMessage("Виконано");
                            }
                            else
                                dialogService.ShowMessage("Не можна редагувати цю позіцію");
                            return;
                        }
                    }
                    ));

        private RelayCommand deleteDataCommand;
        public RelayCommand DeleteDataCommand => deleteDataCommand ?? (deleteDataCommand = new RelayCommand(
                    (obj) =>
                    {
                        if (SelectedType is null)
                        {
                            dialogService.ShowMessage("Немає такої позиції в БД");
                            return;
                        }

                        if (IsColor)
                        {
                            if (lib.CheckDeleteVehicleColor(SelectedType))
                            {
                                DefaultDataLoad(new VehicleColor());
                                dialogService.ShowMessage("Виконано");
                            }
                            else
                                dialogService.ShowMessage("Не можна видалити цю позіцію");
                            return;
                        }
                        if (IsVehType)
                        {
                            if (lib.CheckDeleteVehicleType(SelectedType))
                            {
                                DefaultDataLoad(new VehicleType());
                                dialogService.ShowMessage("Виконано");
                            }
                            else
                                dialogService.ShowMessage("Не можна видалити цю позіцію");
                            return;
                        }
                    }
                    ));

    }

    

    public class SomeType:Helpes.ObservableObject
    { 
        public int Id { get; set; }

        private string someName;
        public string SomeName
        {
            get { return someName; }
            set
            {
                if (someName != value)
                {
                    someName = value;
                    OnPropertyChanged(nameof(SomeName));
                }
            }
        }
    }
}
