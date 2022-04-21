
using System.Windows;

namespace Parking.Helpes
{
    public class DefaultShowWindowService : IShowWindowService
    {
        public void ShowWindow(Window window)
        {
            window.Show();
        }
        public void CloseWindow(Window window)
        {
            if (window != null)
                window.Close();
        }

        public void ShowDialog(Window window)
        {
            window.ShowDialog();
        }
    }
}
