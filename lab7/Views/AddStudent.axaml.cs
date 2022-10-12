using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using lab7.ViewModels;
using lab7.Models;

namespace lab7.Views
{
    public partial class AddStudent : Window
    {
        public AddStudent()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.FindControl<Button>("OK").Click += async delegate
            {
                var context = this.DataContext as AddStudentViewModel;
                context.StudentToReturn.PropertyChanged -=
                context.resetEnable;
                Close(new Student(context.StudentToReturn));
            };
            this.FindControl<Button>("Cancel").Click += async delegate
            {
                Close(null);
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.DataContext = new AddStudentViewModel();
        }
    }
}
