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
    /// Логика взаимодействия для RentCarWindow.xaml
    /// </summary>
    public partial class RentCarWindow : Window
    {
        private int days;
        private int? _countCar;
        private int _userId;
        private int _carId;
        private decimal? _price;
        private string _passport;
        private string _name;
        public RentCarWindow(int userid, int carid,int? count)
        {
            InitializeComponent();
            dayEnd.DisplayDateStart = DateTime.Now.AddDays(1);
            dayEnd.SelectedDate = DateTime.Now.AddDays(1);
            _userId = userid;
            _carId = carid;
            _countCar = count -1;
            Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Modeldb db = new Modeldb())
                {
                    Contract contract = new Contract()
                    {
                        id_client = _userId,
                        name_client = _name,
                        passport = _passport,
                        id_car = _carId,
                        date_start = DateTime.Now,
                        date_end = dayEnd.SelectedDate,
                        price = days * _price
                    };
                    db.Contract.Add(contract);
                    db.Database.ExecuteSqlCommand($"UPDATE Car SET count = '{_countCar}' WHERE id = '{_carId}'");
                    db.SaveChanges();
                    MessageBox.Show("Вы успешно взяли средство в аренду");
                    Close();
                }
        }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
}

        private void dayEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            days = (dayEnd.SelectedDate.Value - DateTime.Now).Days + 1;
            dayCount.Content = days;
            priceCount.Content = days * _price + " руб";
        }
        private void Update()
        {
            try
            {
                using (Modeldb db = new Modeldb())
                {
                    var a = from b in db.Car
                            where b.id == _carId
                            select b;
                    foreach (var i in a)
                    {
                        carName.Content = i.name;
                        _price = i.price;
                        priceCount.Content = i.price + " руб";
                    }
                    var x = from y in db.Clients
                            where y.id == _userId
                            select y;
                    foreach (var i in x)
                    {
                        _name = i.name;
                        _passport = i.passport;
                    }

                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
