using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;



namespace Курисовик_3
{
    /// <summary>
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        private readonly List<products> currentProduct = coffeeEntities.GetContext().products.ToList();
        
        public ListPage()
        {
            InitializeComponent();
            //var currentProduct = coffeeEntities.GetContext().products.ToList(); //отображение списка продукции
            LViewP.ItemsSource = currentProduct;
            var alltypes = coffeeEntities.GetContext().Type1.ToList(); //выплывающий список типов 
            alltypes.Insert(0, new Type1
            {
                name = "Все типы"

            });

            ComboType.ItemsSource = alltypes;
            ComboType.SelectedIndex = 0;
        }
        private void Updateproduct()
        {
            var currentProduct = coffeeEntities.GetContext().products.ToList();



            currentProduct = currentProduct.Where(p => p.Наименование.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList(); //поиск по наименованию
            LViewP.ItemsSource = currentProduct.OrderBy(p => p.Наименование).ToList();
        }



        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Updateproduct();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Updateproduct();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            Manager.MainFrame.Navigate(new AddPage((sender as Button).DataContext as products)); //переход на страницу с добавлением
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var productsForRemoving = LViewP.SelectedItems.Cast<products>().ToList(); //удаление продукта
            if (MessageBox.Show($"Вы точно хотите удалить слудеющие {productsForRemoving.Count()} элементы?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    coffeeEntities.GetContext().products.RemoveRange(productsForRemoving);
                    coffeeEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    LViewP.ItemsSource = coffeeEntities.GetContext().products.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Btnprint_Click(object sender, RoutedEventArgs e)
        {
            //PrintDialog p = new PrintDialog();
            //if (p.ShowDialog() == true)
            //{
            //    p.PrintVisual(LViewP, "Печать");
            //}

            
        }
    }
    
}

