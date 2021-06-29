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

namespace Arenda_car.Client
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private int _userId;
        public UserWindow(int id)
        {
            _userId = id;
            InitializeComponent();
            Update();
        }
        private void Update()
        {
            try
            {
                using (Modeldb db = new Modeldb())
                {
                    var b = from a in db.Clients
                            where _userId == a.id
                            select a;
                    foreach (var i in b)
                    {
                        idLabel.Content = $"id: {i.id}";
                        nameLabel.Content = i.name;
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CarList cars = new CarList(_userId);
            cars.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyAuto myAuto = new MyAuto(_userId);
            myAuto.ShowDialog();
        }
    }
}
