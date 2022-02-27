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


namespace Курисовик_3
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();
            DGridProducts.ItemsSource = coffeeEntities.GetContext().products.ToList();
            var alltypes = coffeeEntities.GetContext().Type1.ToList();
            alltypes.Insert(0, new Type1
            {
                name = "Все типы"

            });

            Combotype.ItemsSource = alltypes;
            Combotype.SelectedIndex = 0;

            
        }
        private void Updateproduct()
        {
            var currentProduct = coffeeEntities.GetContext().Type1.ToList();

            if (Combotype.SelectedIndex > 0)
                currentProduct = currentProduct.Where(p => p.name.Contains(Combotype.SelectedItem.ToString())).ToList();

            currentProduct = currentProduct.Where(p => p.name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            DGridProducts.ItemsSource = currentProduct.OrderBy(p => p.name).ToList();
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs args)
        {
            Updateproduct();
        }
       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddPage(null));
        }
        private void Page_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                coffeeEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridProducts.ItemsSource = coffeeEntities.GetContext().products.ToList();
            }
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddPage((sender as Button).DataContext as products));
        }


        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var productsForRemoving = DGridProducts.SelectedItems.Cast<products>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить слудеющие {productsForRemoving.Count()} элементы?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    coffeeEntities.GetContext().products.RemoveRange(productsForRemoving);
                    coffeeEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridProducts.ItemsSource = coffeeEntities.GetContext().products.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

       
    }
}
