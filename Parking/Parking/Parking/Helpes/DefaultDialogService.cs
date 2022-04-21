using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace Parking.Helpes
{
   class DefaultDialogService : IDialogService
    {
        
        bool IDialogService.YesNoDialog(string message)
        {
            MessageBoxResult res = MessageBox.Show(message, "", MessageBoxButton.YesNo);
            return res == MessageBoxResult.Yes ? true : false;
        }

        void IDialogService.ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private static string lastPath = null;


        public string OpenFileDialog(string path)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = lastPath == null ? "c:\\" : lastPath;
            ofd.Filter = "Image files|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
            ofd.Title = "Open image file";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == true)
                lastPath = ofd.FileName;
            return lastPath;
        }

        

        public void PrintQuitance(Window win)
        {
            PrintDialog dialog = new PrintDialog();
            if(dialog.ShowDialog()==true)
                dialog.PrintVisual(win, "Order quitance");
        }

        public void CloseWindow(Window win)
        {
            win.Close();
        }
    }
}
