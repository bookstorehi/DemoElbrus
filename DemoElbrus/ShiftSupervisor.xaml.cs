using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DemoElbrus
{
    /// <summary>
    /// Логика взаимодействия для ShiftSupervisor.xaml
    /// </summary>
    public partial class ShiftSupervisor : Window
    {
        DBDemoEntities1 _context = new DBDemoEntities1();
        public ShiftSupervisor(bool addclient = false)
        {
            InitializeComponent();
            if (addclient)
            {
                addclientbar.Visibility = Visibility.Visible;
                addClient.IsEnabled = true;
            }
            RefreshOrders();
        }

        public void RefreshOrders()
        {
            using (DBDemoEntities1 db = new DBDemoEntities1())
            {
                OrdersGrid.ItemsSource = db.Orders.ToList();
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }

        private void QrCodeBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderQrCode window = new OrderQrCode((sender as Button).DataContext as Order);
            window.ShowDialog();
        }

        private void StatusBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderStatus window = new OrderStatus(this, (sender as Button).DataContext as Order);
            window.ShowDialog();
        }

        private void addClient_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient(this);
            addClient.Show();
            this.Close();
        }
    }
}
