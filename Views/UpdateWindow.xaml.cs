using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using ToDoListTeldat.Data;
using ToDoListTeldat.Models;

namespace ToDoListTeldat.Views
{
    public partial class UpdatePage : Window
    {
        dbContext db = new dbContext();
        int Id;

        public UpdatePage(int itemId) //wypisanie obecnych danych w formularzu edycji
        {
            InitializeComponent();
            Id = itemId;
            Item updateItem = (from m in db.Items
                               where m.Id == Id
                               select m).Single();
            TitleTextBox.Text = updateItem.Title;
            FinishTimeDatePicker.Value = updateItem.FinishTime;
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e) //aktualizacja danych zadania
        {
            if (TitleTextBox.Text == String.Empty || FinishTimeDatePicker.Value.Value == null)
            {
                MessageBox.Show("wypełnij wszystkie pola!", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Item updateItem = (from m in db.Items
                               where m.Id == Id
                               select m).Single();
            updateItem.Title = TitleTextBox.Text;
            updateItem.FinishTime = FinishTimeDatePicker.Value.Value;
            db.SaveChanges();
            MainWindow.datagrid.ItemsSource = db.Items.Where(x => DbFunctions.TruncateTime(x.FinishTime).Value ==
            DbFunctions.TruncateTime(MainWindow.timeToday.CurrentDay).Value).ToList();
            this.Hide();
        }
    }
}
