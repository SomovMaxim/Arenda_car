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
using Arenda_car.Models;
using Arenda_car.Client;
using Arenda_car.SuperUser;

namespace Arenda_car
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
        }
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text.Length == 0 || passBox.Password.Length == 0)
            {
                MessageBox.Show("Введите логин или пароль");
            }
            else
            {
                using (Modeldb db = new Modeldb())
                {
                    var a = from b in db.Users
                            where b.login.Equals(loginBox.Text) &&
                                  b.password.Equals(passBox.Password)
                            select b;
                    if (a.Count() != 0)
                    {
                        foreach (var i in a)
                        {
                            if (i.type == 1)
                            {
                                UserWindow user = new UserWindow(i.id);
                                user.Show();
                                this.Close();
                            }
                            else
                            {
                                SuperUserWin super = new SuperUserWin();
                                super.Show();
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введенные данные не корректны!");
                    }
                }
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registration = new RegistrationWindow();
            registration.Show();
            Close();
        }

        private void showpass_Click(object sender, RoutedEventArgs e)
        {
            if (showpass.IsChecked == true)
            {
                showpassBox.Text = passBox.Password;
                passBox.Visibility = Visibility.Hidden;
                showpassBox.Visibility = Visibility.Visible;
            }
            else
            {
                passBox.Password = showpassBox.Text;
                showpassBox.Visibility = Visibility.Hidden;
                passBox.Visibility = Visibility.Visible;
            }
        }
    }
}
