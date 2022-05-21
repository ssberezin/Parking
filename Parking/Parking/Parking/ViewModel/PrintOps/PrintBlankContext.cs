using Parking.Helpes;
using Parking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Parking.ViewModel.PrintOps
{
    public class PrintBlankContext: Helpes.ObservableObject
    {
        IDialogService dialogService;
        IShowWindowService showWindow;        
        public ParkingPlaceRecord Record { get; set; }
        public string EpmloyeeData { get; set; }
        public string CurrentDate { get; set; }
        public string LastpayDate  { get; set; }
        public string DeadLine { get; set; }
        public Company Comp { get; set; }

        public PrintBlankContext(ParkingPlaceRecord parkRecord, string inputFIO, Company comp)
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            EpmloyeeData = inputFIO;
            Record = parkRecord;
            Comp = comp;
            LastpayDate = parkRecord.SomeParkingPlaceLog.PayingDate.Value.ToString("dd/MM/yyyy")+"\nЧас:  "+
                          parkRecord.SomeParkingPlaceLog.PayingDate.Value.ToString("hh/mm/ss");
            CurrentDate = DateTime.Now.ToString();
            DeadLine = Record.SomeParkingPlaceLog.DeadLine.Value.ToString("dd/MM/yyyy");
            
        }

        private RelayCommand callPrintPrewievCommand;
        public RelayCommand CallPrintPrewievCommand => callPrintPrewievCommand ??
            (callPrintPrewievCommand = new RelayCommand(
                    (obj) =>
                    {
                        dialogService.PrintQuitance(obj as Window);
                    }
                    ));
    }
}
