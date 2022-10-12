using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab7.Models
{
    public class MyBrush : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        ISolidColorBrush[] brush = new ISolidColorBrush[5];
        public ISolidColorBrush this[int i]
        {
            get
            {
                return brush[i];
            }
            set
            {
                brush[i] = value;
                NotifyPropertyChanged();
            }
        }
    }
}
