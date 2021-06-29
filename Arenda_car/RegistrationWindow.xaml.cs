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
using Arenda_car.Models;

namespace Arenda_car
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private int _userId;
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (fioBox.Text.Length == 0 ||
               birthdayBox.SelectedDate == null ||
               adressBox.Text.Length == 0 ||
               loginBox.Text.Length == 0 ||
               passBox.Password.Length == 0 ||
               againpassBox.Password.Length == 0 ||
               passportBox.Text.Length == 0
               )
            {
                MessageBox.Show("Вы не ввели какие либо данные");
            }
            else if (!passBox.Password.Equals(againpassBox.Password))
            {
                MessageBox.Show("Пароли не совпадают");
            }
            else
            {
                using (Modeldb db = new Modeldb())
                {
                    Users users = new Users()
                    {
                        login = loginBox.Text,
                        password = passBox.Password,
                        type = 1
                    };
                    db.Users.Add(users);
                    db.SaveChanges();
                    var query = from a in db.Users
                                where loginBox.Text.Equals(a.login)
                                select a;
                    foreach (var i in query)
                    {
                        _userId = i.id;
                    }
                    Clients client = new Clients()
                    {
                        id = _userId,
                        name = fioBox.Text,
                        birthday = birthdayBox.DisplayDate.Date,
                        address = adressBox.Text,
                        passport = passportBox.Text
                    };
                    db.Clients.Add(client);
                    db.SaveChanges();
                    MainWindow main = new MainWindow();
                    main.Show();
                    Close();
                }
            }
        }
    }
}
