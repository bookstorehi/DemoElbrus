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
    /// Логика взаимодействия для OrderStatus.xaml
    /// </summary>
    public partial class OrderStatus : Window
    {
        private DBDemoEntities1 _context = new DBDemoEntities1();
        private Order _order;
        ShiftSupervisor _window;
        public OrderStatus(ShiftSupervisor window, Order order)
        {
            InitializeComponent();
            _order = order;
            _window = window;
            OrderCode.Content = order.OrderCode;
            CurStatus.Content = order.Status;
        }

        private void StatusList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StatusList.Text != _order.Status)
            {
                _context.Orders.Attach(_order);
                _order.Status = StatusList.Text;
                _context.SaveChanges();
                _window.RefreshOrders();
                this.Hide();
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
        }
    }
}
