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
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        DBDemoEntities1 _context = new DBDemoEntities1();
        Client _client;
        List<Service> services = new List<Service>();
        public ClientWindow(Client client)
        {
            _client = client;
            InitializeComponent();
            ServicesGrid.ItemsSource = _context.Services.ToList();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow back = new MainWindow();
            back.Show();
            this.Close();
        }

        private void BookOrder_Checked(object sender, RoutedEventArgs e)
        {
            services.Add((sender as CheckBox).DataContext as Service);
        }

        private void BookOrder_Unchecked(object sender, RoutedEventArgs e)
        {
            services.Remove((sender as CheckBox).DataContext as Service);
        }

        private void GoBook_Click(object sender, RoutedEventArgs e)
        {
            if (services.Count > 0)
            {
                bool isnum = int.TryParse(BookTime.Text, out int num);
                if (isnum)
                {
                    using (DBDemoEntities1 db = new DBDemoEntities1())
                    {
                        DateTime date = DateTime.Now;
                        string code = _client.ClientCode + "/" + date.ToString();
                        Order neworder = new Order();
                        neworder.OrderCode = code;
                        neworder.Date = date;
                        neworder.Time = date - DateTime.Today;
                        neworder.ClientCode = _client.ClientCode;
                        neworder.Status = "Новая";
                        neworder.RentTime = num;

                        db.Orders.Add(neworder);
                        foreach (var service in services)
                        {
                            //neworder.Services.Add(service);
                        }
                        db.SaveChanges();
                        MessageBox.Show("Заказ создан!");

                        ServicesGrid.ItemsSource = db.Services.ToList();
                    }
                    
                }
                else
                    MessageBox.Show("Введите число в минутах.");
            }
            else
                MessageBox.Show("Выберите услугу");
        }
    }
}
