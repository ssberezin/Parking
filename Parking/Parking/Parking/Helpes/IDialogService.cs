using System.Windows;

namespace Parking.Helpes
{
    interface IDialogService
    {
        bool YesNoDialog(string message);
        void ShowMessage(string message);
        string OpenFileDialog(string path);        
        void PrintQuitance(Window win);
        void CloseWindow(Window win);
    }
}
