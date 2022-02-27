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
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        private products _currentproduct = new products();
        public AddPage(products selectedProduct)
        {
            InitializeComponent();
            if (selectedProduct != null)
                _currentproduct = selectedProduct;
            DataContext = _currentproduct;
            typebox.ItemsSource = coffeeEntities.GetContext().Type1.ToList();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder(); //обработка ошибок

            if (string.IsNullOrWhiteSpace(_currentproduct.Наименование))
                errors.AppendLine("Укажите название продукта");
            if (_currentproduct.Вес < 1)
                errors.AppendLine("Значение не может быть меньше 1");
            if (_currentproduct.Цена < 1)
                errors.AppendLine("Значение не может быть меньше 1");
            if (_currentproduct.Наименование == null)
                errors.AppendLine("Выберите тип");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentproduct.id_товара == 0)
                coffeeEntities.GetContext().products.Add(_currentproduct);
            try
            {
                coffeeEntities.GetContext().SaveChanges();
                
                MessageBox.Show("Продукт добавлен");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
