
using System.Windows;

namespace Parking.Helpes
{
    public interface IShowWindowService
    {
        void ShowWindow(Window window);
        void CloseWindow(Window window);
        void ShowDialog(Window window);
    }
}
