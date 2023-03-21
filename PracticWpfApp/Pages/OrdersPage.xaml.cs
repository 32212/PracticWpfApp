using PracticWpfApp.Classes;
using PracticWpfApp.Windows;
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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        List<Order> orders;
        double priceSort;
        User user;

        public OrdersPage()
        {
            InitializeComponent();
            SortingAndFiltration();
        }
        public OrdersPage(User user)
        {
            InitializeComponent();
            SortingAndFiltration();
            this.user = user;
        }

        private void SortingAndFiltration() //сортировка и фильтрация
        {
            orders = BaseClass.EM.Order.ToList();

            try
            {
                if (cbDiscount.SelectedIndex != -1) //фильтрация по скидке
                {

                    switch (cbDiscount.SelectedIndex)
                    {
                        case 0:
                            orders = BaseClass.EM.Order.ToList();
                            break;
                        case 1:
                            {
                                orders = BaseClass.EM.Order.Where(x => x.DiscountPrice >= 0 && x.DiscountPrice < 10).ToList();
                            }
                            break;
                        case 2:
                            {
                                orders = BaseClass.EM.Order.Where(x => x.DiscountPrice >= 10 && x.DiscountPrice < 15).ToList();
                            }
                            break;
                        case 3:
                            {
                                orders = BaseClass.EM.Order.Where(x => x.DiscountPrice >= 15).ToList();
                            }
                            break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка фильтрации");
                cbDiscount.SelectedIndex = 0;
            }

            if ((cbSort.SelectedIndex != -1)) //сортировка
            {

                if (cbSort.SelectedIndex == 0)
                {
                    orders = orders.OrderBy(x => x.SumDiscount).ToList();
                }

                else
                {
                    orders = orders.OrderByDescending(x => x.SumDiscount).ToList();
                }
            }

            listOrder.ItemsSource = orders;
            if (orders.Count == 0)
            {
                MessageBox.Show("По вашему запросу ничего не найдено");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e) //возвращаемся на страницу с продуктами
        {
            FrameClass.MainFrame.Navigate(new ProductListPage(user));
        }

        private void btnChangeDate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int index = Convert.ToInt32(btn.Uid);
            Order order = BaseClass.EM.Order.FirstOrDefault(x => x.OrderID == index);
            ChangeDataWindow cODW = new ChangeDataWindow(order);
            cODW.ShowDialog();
        }

        private void tbSumDiscount_Loaded(object sender, RoutedEventArgs e) //считаем общую скидку заказа
        {
            TextBlock tb = (TextBlock)sender; 
            int index = Convert.ToInt32(tb.Uid);
            List<OrderProduct> listOrderProduct = BaseClass.EM.OrderProduct.Where(x => x.OrderID == index).ToList();

            double discount = 0;

            List<int> id = new List<int>();

            foreach (OrderProduct tt in listOrderProduct)
            {
                id.Add(tt.ProductID);
            }

            for (int i = 0; i < id.Count; i++)
            {
                int d = id[i];
                
                List<Product> hc = BaseClass.EM.Product.Where(x => x.ProductID == d).ToList();
                
                foreach (Product hcc in hc)
                {
                    discount += Convert.ToDouble(hcc.ProductCost) - hcc.DiscPriceDouble;
                }

            }
            tb.Text = "Общая скидка заказа: " + discount;
        }

        private void tbSumPrice_Loaded(object sender, RoutedEventArgs e) //считаем общую сумму заказа
        {
            TextBlock tb = (TextBlock)sender; 
            int index = Convert.ToInt32(tb.Uid);
            List<OrderProduct> listOrderProduct = BaseClass.EM.OrderProduct.Where(x => x.OrderID == index).ToList();

            List<int> id = new List<int>();
            double price = 0;
            foreach (OrderProduct op in listOrderProduct)
            {
                id.Add(op.ProductID);

            }
            for (int i = 0; i < id.Count; i++)
            {
                int d = id[i];

                List<Product> listProduct = BaseClass.EM.Product.Where(x => x.ProductID == d).ToList();
                
                foreach (Product lp in listProduct)
                {
                    price += Convert.ToDouble(lp.ProductCost);
                }

            }
            tb.Text = "Общая сумма заказа " + price + " руб.";
            priceSort = price;
        }

        private void tbSostav_Loaded(object sender, RoutedEventArgs e) //отображаем состав заказа
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);

            List<OrderProduct> listOrderProduct = BaseClass.EM.OrderProduct.Where(x => x.OrderID == index).ToList();
            
            string text = "";

            for (int i = 0; i < listOrderProduct.Count; i++)
            {
                if (i == listOrderProduct.Count - 1)
                {
                    text = text + listOrderProduct[i].Product.ProductName + " Количество: " + listOrderProduct[i].Count;
                }
                else
                {
                    text = text + listOrderProduct[i].Product.ProductName + " Количество: " + listOrderProduct[i].Count + "; \n";
                }
            }
            SolidColorBrush mushThen15 = new SolidColorBrush(Color.FromRgb(255, 140, 0));

            tb.Text = "Состав: " + text;
        }

        private void cbDiscount_Change(object sender, SelectionChangedEventArgs e) //фильтрация по скидке
        {
            SortingAndFiltration();
        }

        private void cbSort_Change(object sender, SelectionChangedEventArgs e) //фильтрация по цене
        {
            SortingAndFiltration();
        }
    }
}
