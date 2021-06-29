using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для CarList.xaml
    /// </summary>
    public partial class CarList : Window
    {
        private int _userId;
        private int _carId;
        private int? _countCars;
        public CarList(int id)
        {
            InitializeComponent();
            _userId = id;
            Update();
        }
        private void Update()
        {
            brandBox.Items.Clear();
            priceBox.Items.Clear();
            powerBox.Items.Clear();
            carsList.ItemsSource = null;
            using (Modeldb db = new Modeldb())
            {
                var a = from b in db.Car
                        select new
                        {
                            id = b.id,
                            Название = b.name,
                            Год_выпуска = b.age,
                            Мощность_двигателя = b.power_engine + " л.с",
                            Тип_двигателя = b.type_engine,
                            Кузов = b.body,
                            Цвет = b.color_body,
                            Количество = b.count,
                            Стоимость = b.price + " руб/сут"
                        };
                carsList.ItemsSource = a.ToList();
                var brands = db.Car.GroupBy(x => x.brand).Select(x => x.FirstOrDefault());
                foreach (var i in brands)
                {
                    brandBox.Items.Add(i.brand);
                }
                foreach (var i in a)
                {
                    priceBox.Items.Add(i.Стоимость);
                    powerBox.Items.Add(i.Мощность_двигателя);
                }
            }
            try
            {
                using (Modeldb db = new Modeldb())
                {
                    int? id;
                    var a = from b in db.Contract
                            where b.date_end == DateTime.Now
                            select b;
                    if (a.Count() != 0)
                    {
                        foreach (var i in a)
                        {
                            id = i.id_car;
                            db.Database.ExecuteSqlCommand($"UPDATE Car SET count = 'ISNULL(count, 0)+1' WHERE id = {id}");
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void brandBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (brandBox.SelectedIndex != -1) // только марка
            {

                string brands = brandBox.SelectedValue.ToString();
                using (Modeldb db = new Modeldb())
                {
                    powerBox.Items.Clear();
                    priceBox.Items.Clear();
                    carsList.ItemsSource = null;
                    var a = from b in db.Car
                            where b.brand.Equals(brands)
                            select new
                            {
                                id = b.id,
                                Название = b.name,
                                Год_выпуска = b.age,
                                Мощность_двигателя = b.power_engine + " л.с",
                                Тип_двигателя = b.type_engine,
                                Кузов = b.body,
                                Цвет = b.color_body,
                                Количество = b.count,
                                Стоимость = b.price + " руб/сут"
                            };
                    carsList.ItemsSource = a.ToList();
                    var power_engine = db.Car.GroupBy(x => x.power_engine).Select(x => x.FirstOrDefault()).Where(x => x.brand.Equals(brands));
                    foreach (var i in power_engine)
                    {
                        powerBox.Items.Add(i.power_engine + " л.с");
                    }
                    var price = db.Car.GroupBy(x => x.price).Select(x => x.FirstOrDefault()).Where(x => x.brand.Equals(brands));
                    foreach (var i in price)
                    {
                        priceBox.Items.Add(i.price + " руб/сут");
                    }
                }
            }
            else if (powerBox.SelectedIndex != -1 && brandBox.SelectedIndex != -1) // мощность марка
            {
                string brands = brandBox.SelectedValue.ToString();
                string powers = GetPower(powerBox.SelectedValue.ToString());
                using (Modeldb db = new Modeldb())
                {
                    priceBox.Items.Clear();
                    carsList.ItemsSource = null;
                    var a = from b in db.Car
                            where b.power_engine.Equals(powers) && b.brand.Equals(brands)
                            select new
                            {
                                id = b.id,
                                Название = b.name,
                                Год_выпуска = b.age,
                                Мощность_двигателя = b.power_engine + " л.с",
                                Тип_двигателя = b.type_engine,
                                Кузов = b.body,
                                Цвет = b.color_body,
                                Количество = b.count,
                                Стоимость = b.price + " руб/сут"
                            };
                    carsList.ItemsSource = a.ToList();
                    var price = db.Car.GroupBy(x => x.price).Select(x => x.FirstOrDefault()).Where(x => x.brand.Equals(brands) && x.power_engine.Equals(powers));
                    foreach (var i in price)
                    {
                        priceBox.Items.Add(i.price + " руб/сут");
                    }
                }
            }
            else if (priceBox.SelectedIndex != -1 && brandBox.SelectedIndex != -1) // цена марка
            {
                powerBox.Items.Clear();
                string brands = brandBox.SelectedValue.ToString();
                decimal price = decimal.Parse(GetPower(priceBox.SelectedValue.ToString()));
                using (Modeldb db = new Modeldb())
                {
                    carsList.ItemsSource = null;
                    var a = from b in db.Car
                            where b.price == price && b.brand.Equals(brands)
                            select new
                            {
                                id = b.id,
                                Название = b.name,
                                Год_выпуска = b.age,
                                Мощность_двигателя = b.power_engine + " л.с",
                                Тип_двигателя = b.type_engine,
                                Кузов = b.body,
                                Цвет = b.color_body,
                                Количество = b.count,
                                Стоимость = b.price + " руб/сут"
                            };
                    carsList.ItemsSource = a.ToList();
                    var power_engine = db.Car.GroupBy(x => x.power_engine).Select(x => x.FirstOrDefault()).Where(x => x.brand.Equals(brands) && x.price == price);
                    foreach (var i in power_engine)
                    {
                        powerBox.Items.Add(i.power_engine);
                    }
                }
            }
            else if (priceBox.SelectedIndex != -1 && brandBox.SelectedIndex != -1 && powerBox.SelectedIndex != -1) // все 3 
            {
                string powers = GetPower(powerBox.SelectedValue.ToString());
                string brands = brandBox.SelectedValue.ToString();
                decimal price = decimal.Parse(GetPower(priceBox.SelectedValue.ToString()));
                using (Modeldb db = new Modeldb())
                {
                    carsList.ItemsSource = null;
                    var a = from b in db.Car
                            where b.price == price && b.brand.Equals(brands) && b.power_engine.Equals(powers)
                            select new
                            {
                                id = b.id,
                                Название = b.name,
                                Год_выпуска = b.age,
                                Мощность_двигателя = b.power_engine + " л.с",
                                Тип_двигателя = b.type_engine,
                                Кузов = b.body,
                                Цвет = b.color_body,
                                Количество = b.count,
                                Стоимость = b.price + " руб/сут"
                            };
                    carsList.ItemsSource = a.ToList();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            brandBox.SelectedIndex = -1;
            priceBox.SelectedIndex = -1;
            powerBox.SelectedIndex = -1;
            Update();
        }

        private void powerBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (powerBox.SelectedIndex != -1 && brandBox.SelectedIndex != -1) // мощность марка
            {
                string brands = brandBox.SelectedValue.ToString();
                string powers = GetPower(powerBox.SelectedValue.ToString());
                using (Modeldb db = new Modeldb())
                {
                    priceBox.Items.Clear();
                    carsList.ItemsSource = null;
                    var a = from b in db.Car
                            where b.power_engine.Equals(powers) && b.brand.Equals(brands)
                            select new
                            {
                                id = b.id,
                                Название = b.name,
                                Год_выпуска = b.age,
                                Мощность_двигателя = b.power_engine + " л.с",
                                Тип_двигателя = b.type_engine,
                                Кузов = b.body,
                                Цвет = b.color_body,
                                Количество = b.count,
                                Стоимость = b.price + " руб/сут"
                            };
                    carsList.ItemsSource = a.ToList();
                    var price = db.Car.GroupBy(x => x.price).Select(x => x.FirstOrDefault()).Where(x => x.brand.Equals(brands) && x.power_engine.Equals(powers));
                    foreach (var i in price)
                    {
                        priceBox.Items.Add(i.price + " руб/сут");
                    }
                }
            }
            else if (powerBox.SelectedIndex != -1) // только мощность
            {
                priceBox.Items.Clear();
                brandBox.Items.Clear();
                string powers = GetPower(powerBox.SelectedValue.ToString());
                using (Modeldb db = new Modeldb())
                {
                    carsList.ItemsSource = null;
                    var a = from b in db.Car
                            where b.power_engine.Equals(powers)
                            select new
                            {
                                id = b.id,
                                Название = b.name,
                                Год_выпуска = b.age,
                                Мощность_двигателя = b.power_engine + " л.с",
                                Тип_двигателя = b.type_engine,
                                Кузов = b.body,
                                Цвет = b.color_body,
                                Количество = b.count,
                                Стоимость = b.price + " руб/сут"
                            };
                    carsList.ItemsSource = a.ToList();
                    var price = db.Car.GroupBy(x => x.price).Select(x => x.FirstOrDefault()).Where(x => x.power_engine.Equals(powers));
                    foreach (var i in price)
                    {
                        priceBox.Items.Add(i.price + " руб/сут");
                    }
                    var brand = db.Car.GroupBy(x => x.brand).Select(x => x.FirstOrDefault()).Where(x => x.power_engine.Equals(powers));
                    foreach (var i in price)
                    {
                        priceBox.Items.Add(i.price + " руб/сут");
                    }
                }
            }
            else if (priceBox.SelectedIndex != -1 && brandBox.SelectedIndex != -1 && powerBox.SelectedIndex != -1) // все 3 
            {
                string powers = GetPower(powerBox.SelectedValue.ToString());
                string brands = brandBox.SelectedValue.ToString();
                decimal price = decimal.Parse(GetPower(priceBox.SelectedValue.ToString()));
                using (Modeldb db = new Modeldb())
                {
                    carsList.ItemsSource = null;
                    var a = from b in db.Car
                            where b.price == price && b.brand.Equals(brands) && b.power_engine.Equals(powers)
                            select new
                            {
                                id = b.id,
                                Название = b.name,
                                Год_выпуска = b.age,
                                Мощность_двигателя = b.power_engine + " л.с",
                                Тип_двигателя = b.type_engine,
                                Кузов = b.body,
                                Цвет = b.color_body,
                                Количество = b.count,
                                Стоимость = b.price + " руб/сут"
                            };
                    carsList.ItemsSource = a.ToList();
                }
            }
        }

        private void priceBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (priceBox.SelectedIndex != -1 && brandBox.SelectedIndex != -1) // выбраны цена и марка
            {
                powerBox.Items.Clear();
                string brands = brandBox.SelectedValue.ToString();
                decimal price = decimal.Parse(GetPower(priceBox.SelectedValue.ToString()));
                using (Modeldb db = new Modeldb())
                {
                    carsList.ItemsSource = null;
                    var a = from b in db.Car
                            where b.price == price && b.brand.Equals(brands)
                            select new
                            {
                                id = b.id,
                                Название = b.name,
                                Год_выпуска = b.age,
                                Мощность_двигателя = b.power_engine + " л.с",
                                Тип_двигателя = b.type_engine,
                                Кузов = b.body,
                                Цвет = b.color_body,
                                Количество = b.count,
                                Стоимость = b.price + " руб/сут"
                            };
                    carsList.ItemsSource = a.ToList();
                    var power_engine = db.Car.GroupBy(x => x.power_engine).Select(x => x.FirstOrDefault()).Where(x => x.brand.Equals(brands) && x.price == price);
                    foreach (var i in power_engine)
                    {
                        powerBox.Items.Add(i.power_engine);
                    }
                }

            }
            else if (priceBox.SelectedIndex != -1) // выбрана только цена
            {
                string brands = brandBox.SelectedValue.ToString();
                decimal price = decimal.Parse(GetPower(priceBox.SelectedValue.ToString()));
                using (Modeldb db = new Modeldb())
                {
                    carsList.ItemsSource = null;
                    var a = from b in db.Car
                            where b.price == price && b.brand.Equals(brands)
                            select new
                            {
                                id = b.id,
                                Название = b.name,
                                Год_выпуска = b.age,
                                Мощность_двигателя = b.power_engine + " л.с",
                                Тип_двигателя = b.type_engine,
                                Кузов = b.body,
                                Цвет = b.color_body,
                                Количество = b.count,
                                Стоимость = b.price + " руб/сут"
                            };
                    carsList.ItemsSource = a.ToList();
                }
            }
            else if (priceBox.SelectedIndex != -1 && brandBox.SelectedIndex != -1 && powerBox.SelectedIndex != -1) // выбраны все 3 фильтра
            {
                string powers = GetPower(powerBox.SelectedValue.ToString());
                string brands = brandBox.SelectedValue.ToString();
                decimal price = decimal.Parse(GetPower(priceBox.SelectedValue.ToString()));
                using (Modeldb db = new Modeldb())
                {
                    carsList.ItemsSource = null;
                    var a = from b in db.Car
                            where b.price == price && b.brand.Equals(brands) && b.power_engine.Equals(powers)
                            select new
                            {
                                id = b.id,
                                Название = b.name,
                                Год_выпуска = b.age,
                                Мощность_двигателя = b.power_engine + " л.с",
                                Тип_двигателя = b.type_engine,
                                Кузов = b.body,
                                Цвет = b.color_body,
                                Количество = b.count,
                                Стоимость = b.price + " руб/сут"
                            };
                    carsList.ItemsSource = a.ToList();
                }
            }
        }
        public string GetPower(string a)
        {
            string[] b = a.Split(' ');
            string result = $"{b[0]}";
            return result;
        }
        private int GetCarId(string a)
        {
            int index = a.IndexOf(',');
            int result = int.Parse(a.Substring(7, index - 7).Trim());
            return result;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (carsList.SelectedIndex != -1 && _countCars != 0)
            {
                RentCarWindow rentCar = new RentCarWindow(_userId, _carId,_countCars);
                rentCar.ShowDialog();
            }
            else if(_countCars == 0)
            {
                MessageBox.Show("Машин нет в наличии");
            }
        }

        private void carsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (carsList.SelectedIndex != -1)
            {
                _carId = GetCarId(carsList.SelectedItem.ToString());
                using (Modeldb db = new Modeldb())
                {
                    var a = from b in db.Car
                            where b.id == _carId
                            select b;
                    foreach (var i in a)
                    {
                        _countCars = i.count;
                    }
                }
            }
            
        }    
    }
}
