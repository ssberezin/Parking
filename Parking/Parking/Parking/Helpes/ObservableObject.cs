using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Parking.Helpes
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged2;
        protected void OnPropertyChanged2([CallerMemberName] string propName = "")
        {
            PropertyChanged2?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
