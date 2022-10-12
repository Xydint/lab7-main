using System;
using System.IO;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using lab7.ViewModels;
using lab7.Models;

namespace lab7.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.FindControl<Button>("AddStudent").Click += ClickEventAdd;
            this.FindControl<Button>("DeleteStudents").Click += ClickEventDelete;
            this.FindControl<MenuItem>("Save").Click += ClickEventSave;
            this.FindControl<MenuItem>("Load").Click += ClickEventLoad;
            this.FindControl<MenuItem>("Exit").Click += ClickEventExit;
            this.FindControl<MenuItem>("About").Click += ClickEventAbout;
        }
        private async void ClickEventAdd(object? sender, RoutedEventArgs e)
        {
            var context = (this.DataContext as MainWindowViewModel);
            Student? st = await new AddStudent().ShowDialog<Student?>((Window)this.VisualRoot);
            if (st != null)
            {
                st.PropertyChanged += context.ContentCollectionChanged;
                context.Items.Add(st);
                context.resetAverage();
            }
        }
        private async void ClickEventDelete(object? sender, RoutedEventArgs e)
        {
            var context = (this.DataContext as MainWindowViewModel);
            var items = context.Items;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].BoxChecked) { items.Remove(items[i]); i--; };
            }
            context.resetAverage();
        }
        private async void ClickEventSave(object? sender, RoutedEventArgs e)
        {
            string? path;
            var taskPath = new SaveFileDialog()
            {
                Title = "Save file",
                Filters = new List<FileDialogFilter>()
            };
            taskPath.Filters.Add(new FileDialogFilter() { Name = "Текстовый файл (.txt)", Extensions = { "txt" } });

            path = await taskPath.ShowAsync((Window)this.VisualRoot);
            if (path is not null)
            {
                var items = (this.DataContext as MainWindowViewModel).Items;
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(items.Count.ToString());
                    foreach (var item in items)
                    {
                        sw.WriteLine(item.FirstName);
                        sw.WriteLine(item.SecondName);
                        sw.WriteLine(item.Patronymic);
                        for (int i = 0; i < 5; i++)
                            sw.WriteLine(item[i]);
                    }
                }
            }
        }
        private async void ClickEventLoad(object? sender, RoutedEventArgs e)
        {
            string? path;
            var taskPath = new OpenFileDialog()
            {
                Title = "Open file",
                Filters = new List<FileDialogFilter>()
            };
            taskPath.Filters.Add(new FileDialogFilter() { Name = "TXT files", Extensions = { "txt" } });

            string[]? pathArray = await taskPath.ShowAsync((Window)this.VisualRoot);
            path = pathArray is null ? null : string.Join(@"\", pathArray);

            if (path is not null)
            {
                var items = (this.DataContext as MainWindowViewModel).Items;
                items.Clear();
                using (StreamReader sr = File.OpenText(path))
                {
                    int count;
                    if (!Int32.TryParse(sr.ReadLine(), out count)) return;
                    for (int i = 0; i < count; i++)
                    {
                        items.Add
                            (new Student(sr.ReadLine(), sr.ReadLine(), sr.ReadLine(),
                            new string[5] { sr.ReadLine(), sr.ReadLine(), sr.ReadLine(),
                                sr.ReadLine(), sr.ReadLine() }));
                    }
                    foreach (var item in items)
                    {
                        item.PropertyChanged +=
                            (this.DataContext as MainWindowViewModel).ContentCollectionChanged;
                    }
                }
                (this.DataContext as MainWindowViewModel).resetAverage();
            }
        }
        private async void ClickEventExit(object? sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void ClickEventAbout(object? sender, RoutedEventArgs e)
        {

            await new About().ShowDialog((Window)this.VisualRoot);
        }
    }
}
