using Parking.Helpes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel
{
    public class MainViewModel: Helpes.ObservableObject
    {
        IDialogService dialogService;
        IShowWindowService showWindow;

        public MainViewModel()
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();

        }



    }
}
