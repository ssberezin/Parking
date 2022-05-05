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

        public event PropertyChangedEventHandler PropertyChanged3;
        protected void OnPropertyChanged3([CallerMemberName] string propName = "")
        {
            PropertyChanged3?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged4;
        protected void OnPropertyChanged4([CallerMemberName] string propName = "")
        {
            PropertyChanged4?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged5;
        protected void OnPropertyChanged5([CallerMemberName] string propName = "")
        {
            PropertyChanged5?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged6;
        protected void OnPropertyChanged6([CallerMemberName] string propName = "")
        {
            PropertyChanged6?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
