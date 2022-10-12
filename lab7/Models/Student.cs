using System;
using Avalonia.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Collections.Generic;

namespace lab7.Models
{
    public class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Student(string fn, string sn, string p, string[] control)
        {
            FirstName = fn;
            SecondName = sn;
            Patronymic = p;
            Control = control;
            BoxChecked = false;
            resetAverage();
        }
        public Student(string fn, string sn, string p, string[] control, bool check) : this(fn, sn, p, control)
        {
            BoxChecked = check;
        }
        public Student(Student old)
        {
            FirstName = old.FirstName;
            SecondName = old.SecondName;
            Patronymic = old.Patronymic;
            Control = old.Control;
            BoxChecked = old.BoxChecked;
            resetAverage();
        }

        string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                NotifyPropertyChanged();
            }
        }

        string secondName;
        public string SecondName
        {
            get { return secondName; }
            set
            {
                secondName = value;
                NotifyPropertyChanged();
            }
        }

        string patronymic;
        public string Patronymic
        {
            get { return patronymic; }
            set
            {
                patronymic = value;
                NotifyPropertyChanged();
            }
        }

        string[] control = new string[5];
        public string[] Control
        {
            get { return control; }
            set
            {
                control = value;
                resetAverage();
            }
        }
        public string this[int i]
        {
            get
            {
                return control[i];
            }
            set
            {
                int x;
                if (value.Length < 2)
                    if (Int32.TryParse(value, out x))
                    {
                        if (x > -1 && x < 3)
                        {
                            control[i] = value;
                        }
                        else
                        {
                            control[i] = "#ERROR";
                        }
                    }
                    else
                    {
                        control[i] = "#ERROR";
                    }
                else
                {
                    control[i] = "#ERROR";
                }
                resetAverage();
            }
        }
        ISolidColorBrush brush;
        public ISolidColorBrush Brush
        {
            get { return brush; }
            set
            {
                brush = value;
                NotifyPropertyChanged();
            }
        }

        bool boxChecked;
        public bool BoxChecked
        {
            get { return boxChecked; }
            set
            {
                boxChecked = value;
                NotifyPropertyChanged();
            }
        }

        string average;
        public string Average
        {
            get
            {
                return average;
            }
            set
            {
                average = value;
                double x;
                if (Double.TryParse(average, out x))
                {
                    if (x < 1) Brush = Brushes.Red;
                    if (x >= 1.5) Brush = Brushes.Green;
                    if (x >= 1 && x < 1.5) Brush = Brushes.Yellow;
                }
                else
                {
                    Brush = Brushes.White;
                }
                NotifyPropertyChanged();
            }
        }

        private void resetAverage()
        {
            bool err = false;
            double averageL = 0;
            double x;
            foreach (var item in control)
            {
                if (Double.TryParse(item, out x))
                    averageL += x;
                else
                {
                    err = true;
                    break;
                }
            }
            if (!err)
            {
                averageL /= 5;
                Average = averageL.ToString();
            }
            else
                Average = "#ERROR";
        }
    }
}
