using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using ToDoListTeldat.Data;
using ToDoListTeldat.Models;

namespace ToDoListTeldat.Views
{

    public partial class MainWindow : Window
    {
        dbContext db = new dbContext();
        public static DataGrid datagrid;
        public static Date timeToday = new Date();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = timeToday;
            datagrid = myDataGrid;

            var List = db.Items.Where(x => DbFunctions.TruncateTime(x.FinishTime).Value
            == DbFunctions.TruncateTime(timeToday.CurrentDay).Value).ToList();
            myDataGrid.ItemsSource = List;

            NotificationsTimer();
        }



        private void insertBtn_Click(object sender, RoutedEventArgs e) //przejście do ekranu dodawania zadania
        {
            InsertPage Ipage = new InsertPage();
            Ipage.ShowDialog();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e) //przejście do ekranu edytowania zadania
        {
            int Id = (myDataGrid.SelectedItem as Item).Id;
            UpdatePage Upage = new UpdatePage(Id);
            Upage.ShowDialog();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e) //usuwanie zadania
        {
            int Id = (myDataGrid.SelectedItem as Item).Id;
            var deleteMember = db.Items.Where(m => m.Id == Id).Single();
            db.Items.Remove(deleteMember);
            db.SaveChanges();
            var List = db.Items.Where(x => DbFunctions.TruncateTime(x.FinishTime).Value == DbFunctions.TruncateTime(timeToday.CurrentDay).Value).ToList();
            myDataGrid.ItemsSource = List;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) //pokazywanie zadan dla konkretnego dnia
        {
            var List = db.Items.Where(x => DbFunctions.TruncateTime(x.FinishTime).Value == DbFunctions.TruncateTime(timeToday.CurrentDay).Value).ToList();
            myDataGrid.ItemsSource = List;
        }

        private void NotificationsTimer() //sprawdzanie czy nie należy wysłać powiadomienia co 30 sekund
        {
            var _activeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(0.5)
            };
            _activeTimer.Tick += delegate (object sender, EventArgs e)
            {
                Notification();
            };
            _activeTimer.Start();
        }

        private void Notification() //tworzenie powiadomienia gdy znajdzie się zadanie które mamy wykonać za godzine
        {
            Date timeToday = new Date();
            var List = db.Items.Where(x => x.FinishTime.Hour == timeToday.CurrentDay.Hour + 1
            && x.FinishTime.Minute == timeToday.CurrentDay.Minute && x.FinishTime.Day == timeToday.CurrentDay.Day).ToList();
            if (List.Count == 0)
            {
                return;
            }

            Notifier notifier = new Notifier(cfg =>
            {
                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(TimeSpan.FromSeconds(5), MaximumNotificationCount.FromCount(15));
                cfg.PositionProvider = new PrimaryScreenPositionProvider(Corner.BottomRight, 10, 10);
            });

            foreach (var item in List)
            {
                notifier.ShowInformation(item.Title + " za godzine!");
            }
        }
    }
}
