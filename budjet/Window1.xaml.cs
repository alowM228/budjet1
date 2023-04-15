using System;
using System.Collections.Generic;
using System.Data;
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
using budjet.бюджетDataSetTableAdapters;

namespace budjet
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        expense_typesTableAdapter tip = new expense_typesTableAdapter();
        public Window1()
        {
            InitializeComponent();
            Aqw.ItemsSource = tip.GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            object id = (Aqw.SelectedItem as DataRowView);
            tip.InsertQuery (NameTbx.Text);
            Aqw.ItemsSource = tip.GetData();
            MainWindow mainWindow = new MainWindow();
            this.Close();


        }

        private void Aqw_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            object id = (Aqw.SelectedItem as DataRowView).Row[0];
            tip.DeleteQuery(Convert.ToInt32(id));
            Aqw.ItemsSource = tip.GetData();
        }
    }
}
