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
    /// Логика взаимодействия для SuperUserWin.xaml
    /// </summary>
    public partial class SuperUserWin : Window
    {
        private static int _carId;
        public SuperUserWin()
        {
            InitializeComponent();
            Update();
        }
        private void Update()
        {
            try
            {
                carList.ItemsSource = null;
                using (Modeldb db = new Modeldb())
                {
                    var a = from b in db.Car
                            select new
                            {
                                id = b.id,
                                Название = b.name,
                                Количество = b.count
                            };
                    carList.ItemsSource = a.ToList();
                }
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
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
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewCar carAdd = new AddNewCar();
            carAdd.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (carList.SelectedIndex != -1)
            {
                    AddNewCar carEdit = new AddNewCar(_carId);
                    carEdit.ShowDialog();
            }
            else MessageBox.Show("Средство не было выбрано");
        }
        private int GetId(string a)
        {
            int index = a.IndexOf(',');
            int result = int.Parse(a.Substring(7, index - 7));
            return result;
        }

        private void carList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (carList.SelectedIndex != -1) _carId = GetId(carList.SelectedItem.ToString());
        }
    }
}
