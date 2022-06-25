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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoElbrus
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBDemoEntities1 _context = new DBDemoEntities1();
        int counter = 0;
        string syms = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwyz1234567890";
        string curCapcha = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateWorker(Worker worker, string type)
        {
            using (DBDemoEntities1 db = new DBDemoEntities1())
            {
                db.Workers.Attach(worker);
                worker.EntryType = type;
                worker.LastStay = DateTime.Now;
                db.SaveChanges();
            }
        }

        private void logBtn_Click(object sender, RoutedEventArgs e)
        {
            if (counter < 3)
            {
                if (logTxt.Text != "" && passTxt.Text != "")
                {
                    foreach (var worker in _context.Workers)
                    {
                        if (logTxt.Text == worker.Login)
                        {
                            if (passTxt.Text == worker.Password)
                            {
                                UpdateWorker(worker, "Успешно");
                                if (worker.Role == "Администратор")
                                {
                                    AdminWindow adminWindow = new AdminWindow();
                                    adminWindow.Show();
                                    this.Close();
                                }
                                else
                                {
                                    if (worker.Role == "Продавец")
                                    {
                                        ShiftSupervisor window = new ShiftSupervisor();
                                        window.Show();
                                    }
                                    else if (worker.Role == "Старший смены")
                                    {
                                        ShiftSupervisor window = new ShiftSupervisor(true);
                                        window.Show();
                                    }
                                    else
                                        MessageBox.Show("Ваша роль не опознана.");
                                    this.Close();
                                }
                            }
                            else
                                UpdateWorker(worker, "Неуспешно");
                        }
                    }
                    foreach (var client in _context.Clients)
                    {
                        if (logTxt.Text == client.Email && passTxt.Text == client.Password)
                        {
                            ClientWindow clientWindow = new ClientWindow(client);
                            clientWindow.Show();
                            this.Close();
                        }
                    }
                    counter++;
                    if (counter > 2)
                    {
                        NewCaptcha();
                        CapchaPan.Visibility = Visibility.Visible;
                    }
                }
                else
                    MessageBox.Show("Заполните все поля");
            }
        }

        private void NewCaptcha()
        {
            int symsl = syms.Length;
            Random rand = new Random();
            curCapcha = "";
            for (int i = 0; i < 5; i++)
                curCapcha += syms[rand.Next(symsl - 1)];
            CaptchaText.Content = curCapcha;
        }

        private void ApproveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CaptchaTxt.Text == curCapcha)
            {
                counter = 0;
                CapchaPan.Visibility = Visibility.Collapsed;
            }
            else
                MessageBox.Show("Неверный текст");
        }
    }
}
