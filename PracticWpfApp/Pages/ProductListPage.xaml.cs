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

        public void SortingAndFiltration() //сортировка и фильтрация
        {
            products = BaseClass.EM.Product.ToList();
            tbFirst.Text = products.Count.ToString();

            if (cbDiscount.SelectedIndex != 0) //фильтрация по скидке
            {
                switch (cbDiscount.SelectedIndex)
                {
                    case 1:
                        {
                            products = BaseClass.EM.Product.Where(x => x.ProductDiscountAmount >= 0 && x.ProductDiscountAmount < 10).ToList();
                        }
                        break;
                    case 2:
                        {
                            products = BaseClass.EM.Product.Where(x => x.ProductDiscountAmount >= 10 && x.ProductDiscountAmount < 15).ToList();
                        }
                        break;
                    case 3:
                        {
                            products = BaseClass.EM.Product.Where(x => x.ProductDiscountAmount >= 15).ToList();
                        }
                        break;
                }
            }
            else if (cbDiscount.SelectedIndex != -1)
            {
                products = BaseClass.EM.Product.ToList();
            }

            if ((cbSort.SelectedIndex != -1)) //сортировка по стоимости
            {

                if (cbSort.SelectedIndex == 0)
                {
                    products = products.OrderBy(x => x.ProductCost).ToList();
                }
                else
                {
                    products = products.OrderByDescending(x => x.ProductCost).ToList();
                }

            }

            if (!string.IsNullOrWhiteSpace(tbFindByTitle.Text))//По названию
            {
                products = products.Where(x => x.ProductName.ToLower().Contains(tbFindByTitle.Text.ToLower())).ToList();
            }


            tbSecond.Text = products.Count.ToString();
            listProduct.ItemsSource = products;
            if (products.Count == 0)
            {
                MessageBox.Show("Нет данных");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new AutorizationPage());
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new OrdersPage(users));
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
