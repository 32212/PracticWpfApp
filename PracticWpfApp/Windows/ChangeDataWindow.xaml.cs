using PracticWpfApp.Classes;
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

namespace PracticWpfApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeDataWindow.xaml
    /// </summary>
    public partial class ChangeDataWindow : Window
    {
        Order order;
        public ChangeDataWindow(Order order)
        {
            InitializeComponent();
            this.order = order;

            dpDate.SelectedDate = order.OrderDeliveryDate; //дата по умолчанию
        }

        private void btnChange_Click(object sender, RoutedEventArgs e) //закрываем окно
        {
            this.Close();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e) //сохраняем данные и закрываем окно
        {
            try
            {
                order.OrderDeliveryDate = (DateTime)dpDate.SelectedDate;
                BaseClass.EM.SaveChanges();
                MessageBox.Show("Дата изменена");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при изменении даты");
            }
        }
    }
}
