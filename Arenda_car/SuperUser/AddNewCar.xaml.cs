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

namespace Arenda_car.SuperUser
{
    /// <summary>
    /// Логика взаимодействия для AddNewCar.xaml
    /// </summary>
    public partial class AddNewCar : Window
    {
        private int _indicator;
        private int _carId;
        public AddNewCar()
        {
            _indicator = 0;
            InitializeComponent();
        }
        public AddNewCar(int id)
        {
            _indicator = 1;
            _carId = id;
            InitializeComponent();
            Update();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int a = 0;
                if (insurance.SelectedIndex == 0)
                    a = 0;
                else
                    a = 1;
                if (_indicator == 1)
                {
                    using (Modeldb db = new Modeldb())
                    {
                        db.Database.ExecuteSqlCommand($"Update Car SET vin = '{vin.Text}', " +
                            $"state_number = '{state.Text}', " +
                            $"brand = '{brand.Text}', " +
                            $"category = '{category.Text}', " +
                            $"age = '{year.Text}', " +
                            $"body = '{body.Text}'," +
                            $"color_body = '{color.Text}', " +
                            $"power_engine = '{power.Text}', " +
                            $"type_engine = '{type.Text}', " +
                            $"Insurance = '{a}', " +
                            $"count = '{int.Parse(count.Text)}', " +
                            $"price = '{decimal.Parse(price.Text)}', " +
                            $"name = '{name.Text}' WHERE id = '{_carId}'");
                        db.SaveChanges();
                    }
                }
                else
                {
                    using (Modeldb db = new Modeldb())
                    {
                        Car car = new Car()
                        {
                            vin = vin.Text,
                            state_number = state.Text,
                            brand = brand.Text,
                            category = category.Text,
                            age = year.Text,
                            body = body.Text,
                            color_body = color.Text,
                            power_engine = power.Text,
                            type_engine = type.Text,
                            Insurance = a,
                            count = int.Parse(count.Text),
                            price = decimal.Parse(price.Text),
                            name = name.Text
                        };
                        db.Car.Add(car);
                        db.SaveChanges();
                    }
                }
                MessageBox.Show("Средство успешно добавлено/изменено");
                Close();
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        private void Update()
        {
            using (Modeldb db = new Modeldb())
            {
                var a = from b in db.Car
                        where _carId == b.id
                        select b;
                foreach(var i in a)
                {
                    name.Text = i.name;
                    vin.Text = i.vin;
                    state.Text = i.state_number;
                    brand.Text = i.brand;
                    if (i.category.Equals("A")) category.SelectedIndex = 0;
                    else if (i.category.Equals("B")) category.SelectedIndex = 1;
                    else if (i.category.Equals("C")) category.SelectedIndex = 2;
                    else if (i.category.Equals("D")) category.SelectedIndex = 3;
                    year.Text = i.age;
                    body.Text = i.body;
                    color.Text = i.color_body;
                    power.Text = i.power_engine;
                    if (i.type_engine.Equals("Рядный")) type.SelectedIndex = 0;
                    else if (i.type_engine.Equals("V-образный")) type.SelectedIndex = 1;
                    else if (i.type_engine.Equals("VR-образный")) type.SelectedIndex = 2;
                    else if (i.type_engine.Equals("W-образный")) type.SelectedIndex = 3;
                    else if (i.type_engine.Equals("Оппозитный")) type.SelectedIndex = 4;
                    if (i.Insurance == 0) insurance.SelectedIndex = 0;
                    else if (i.Insurance == 1) insurance.SelectedIndex = 1;
                    count.Text = i.count.ToString();
                    price.Text = i.price.ToString();
                }
            }
        }
    }
}
