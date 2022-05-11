using Parking.ViewModel.PersonOperations;
using System.Windows;


namespace Parking.Views.PersonOperations
{
    /// <summary>
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow(int userId)
        {
            InitializeComponent();
            this.DataContext = new EmployeeWindowContext(userId);
        }
    }
}