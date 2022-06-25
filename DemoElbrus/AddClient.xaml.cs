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
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        DBDemoEntities1 _context = new DBDemoEntities1();
        public AddClient(ShiftSupervisor window)
        {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShiftSupervisor back = new ShiftSupervisor(true);
            back.Show();
            this.Close();
        }

        private void logBtn_Click(object sender, RoutedEventArgs e)
        {
            if (surnameTxt.Text != "")
            {
                if (nameTxt.Text != "")
                {
                    if (pasptSerTxt.Text == "" || pasptSerTxt.Text.Length == 4)
                    {
                        if (pasptNoTxt.Text == "" || pasptNoTxt.Text.Length == 6)
                        {
                            if (addressTxt.Text != "")
                            {
                                if (passTxt.Text.Length > 7)
                                {
                                    _context.Clients.Add(new Client
                                    {
                                        Surname = surnameTxt.Text,
                                        Name = nameTxt.Text,
                                        Patronymic = patronomicTxt.Text,
                                        PassportSer = Convert.ToInt32(pasptSerTxt.Text),
                                        PassportNo = Convert.ToInt32(pasptNoTxt.Text),
                                        Birthday = DateTime.Parse(birthdayTxt.Text),
                                        Address = addressTxt.Text,
                                        Email = emailTxt.Text,
                                        Password = passTxt.Text,
                                    });
                                    _context.SaveChanges();
                                    MessageBox.Show("Клиент зарегистрирован!");

                                    surnameTxt.Text = "";
                                    nameTxt.Text = "";
                                    patronomicTxt.Text = "";
                                    pasptSerTxt.Text = "";
                                    pasptNoTxt.Text = "";
                                    birthdayTxt.Text = "";
                                    addressTxt.Text = "";
                                    emailTxt.Text = "";
                                    passTxt.Text = "";

                                    try
                                    {
                                        
                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show("Ошибка: ", ex.Message);
                                    }
                                }
                                else
                                    MessageBox.Show("Пароль должен содержать как минимум 8 символов.");
                            }
                            else
                                MessageBox.Show("Введите адрес");
                        }
                        else if (pasptNoTxt.Text.Length != 6)
                            MessageBox.Show("Введите корректный номер паспорта");
                    }
                    else if (pasptSerTxt.Text.Length != 4)
                        MessageBox.Show("Введите корректную серию паспорта");
                }
                else
                    MessageBox.Show("Введите имя");
            }
            else
                MessageBox.Show("Введите фамилию");
        }
    }
}
