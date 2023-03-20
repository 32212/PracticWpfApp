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

namespace PracticWpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        List<Product> products;
        List<OrderProduct> orderProducts = new List<OrderProduct>();
        User users;

        public ProductListPage() //гость
        {
            InitializeComponent();
            SortingAndFiltration();

            tbNameUser.Text = "Гость";

            btnBasket.Visibility = Visibility.Hidden;
            btnOrder.Visibility = Visibility.Hidden;
        }

        public ProductListPage(User user) //конструктор дял клиента
        {
            InitializeComponent();
            SortingAndFiltration();

            btnOrder.Visibility = Visibility.Hidden;
            tbNameUser.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;

            if (user.RoleID == 1) //скрываем заказы для клиента
            {
                btnOrder.Visibility = Visibility.Hidden;
            }
            else
            { 
                btnOrder.Visibility= Visibility.Visible;
            }
            
            users = user;

        }

        public ProductListPage(char b) //конструктор для администратора
        {
            InitializeComponent();
        }

        public ProductListPage(string c) //конструктор для менеджера
        {
            InitializeComponent();
        }

        public ProductListPage(int a, int b) //конструктор для гостя
        {
            InitializeComponent();
        }

        public void SortingAndFiltration() //сортировка и фильтрация
        {
            
        }

        private void btnBasket_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbCost_Loaded(object sender, RoutedEventArgs e) //показываем или скрываем цену
        {
            TextBlock tb = (TextBlock)sender;

            if (tb.Uid != null)
            {
                tb.Visibility = Visibility.Visible;
            }
            else
            {
                tb.Visibility = Visibility.Collapsed;
            }
        }

        private void tbFindByTitle_TextChanged(object sender, TextChangedEventArgs e) //поиск ао названию
        {
            SortingAndFiltration();
        }

        private void cbDiscount_Changed(object sender, SelectionChangedEventArgs e) //скидки
        {
            SortingAndFiltration();
        }

        private void cbSort_Changed(object sender, SelectionChangedEventArgs e) //сортировка
        {
            SortingAndFiltration();
        }
    }
}
