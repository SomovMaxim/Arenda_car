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
    /// Логика взаимодействия для MyAuto.xaml
    /// </summary>
    public partial class MyAuto : Window
    {
        private int _userId;
        private int? _carId;
        private string _carName;
        public MyAuto(int id)
        {
            InitializeComponent();
            _userId = id;
            Update();
        }
        private void Update()
        {
            using (Modeldb db = new Modeldb())
            {
                var a = from b in db.Contract
                        where b.id_client == _userId
                        select b;
                foreach (var i in a)
                {
                    _carId = i.id_car;
                }
                var q = from w in db.Car
                        where w.id == _carId
                        select w;
                foreach(var i in q)
                {
                    _carName = i.name;
                }
                var z = from c in db.Contract
                        where c.id_client == _userId
                        select new
                        {
                            Машина = _carName,
                            Дата_начала = c.date_start.ToString(),
                            Дата_окончания = c.date_end.ToString(),
                            Стоимость = c.price
                        };
                carList.ItemsSource = z.ToList();
            }
        }
    }
}
