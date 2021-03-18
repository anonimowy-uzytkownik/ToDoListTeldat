using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using ToDoListTeldat.Data;
using ToDoListTeldat.Models;

namespace ToDoListTeldat.Views
{
    public partial class InsertPage : Window
    {
        dbContext db = new dbContext();

        public InsertPage()
        {
            InitializeComponent();
        }


        private void insertBtn_Click(object sender, RoutedEventArgs e) //dodanie zadania
        {
            if (TitleTextBox.Text == String.Empty || FinishTimeDatePicker.Value.Value == null)
            {
                MessageBox.Show("wypełnij wszystkie pola!", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Item newItem = new Item()
            {
                Title = TitleTextBox.Text,
                FinishTime = FinishTimeDatePicker.Value.Value
            };

            db.Items.Add(newItem);
            db.SaveChanges();
            MainWindow.datagrid.ItemsSource = db.Items.Where(x => DbFunctions.TruncateTime(x.FinishTime).Value == DbFunctions.TruncateTime(MainWindow.timeToday.CurrentDay).Value).ToList();
            this.Hide();
        }
    }
}
