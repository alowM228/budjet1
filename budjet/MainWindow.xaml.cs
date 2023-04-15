using budjet.бюджетDataSetTableAdapters;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace budjet
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        expense_typesTableAdapter expense_types = new expense_typesTableAdapter();
        expensesTableAdapter expenses = new expensesTableAdapter();
        decimal total = 0;

        public MainWindow()
        {
            InitializeComponent();
            dp.Text = DateTime.Now.ToString();

       

            cb_tip.ItemsSource = expense_types.GetData();
            cb_tip.DisplayMemberPath = "type_name_";

            var allExpenses = expenses.GetData();
            foreach (var exp in allExpenses)
            {
                total += exp.Сумма;
            }
            totalTbk.Text = total.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTbx.Text))
            {
                MessageBox.Show("Ошибка: пустая строка");
            }
            else
            {
                try
                {
                    int amount = Convert.ToInt32(NameTbx.Text);
                    // проверка на отрицательное число
                    if (amount < 0)
                    {
                        expenses.InsertQuery((cb_tip.SelectedItem as DataRowView).Row[1].ToString(), amount, DateTime.Parse(dp.Text).Date);
                        total -= amount;
                    }
                    else
                    {
                        expenses.InsertQuery((cb_tip.SelectedItem as DataRowView).Row[1].ToString(), -amount, DateTime.Parse(dp.Text).Date);
                        total += amount;
                    }

                }
                catch (System.FormatException e_)
                {
                    MessageBox.Show(e_.Message);
                }
                finally
                {
                    Aqw.ItemsSource = expenses.GetData();
                    NameTbx.Text = null;
                    totalTbk.Text = total.ToString();
                }

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Aqw.SelectedItem != null)
            {
                try
                {
                    int id = Convert.ToInt32((Aqw.SelectedItem as DataRowView).Row[0]);
                    int amount = Convert.ToInt32((Aqw.SelectedItem as DataRowView).Row[3]);
                    // проверка на отрицательное число
                    if (amount < 0)
                    {
                        total += amount;
                    }
                    else
                    {
                        total -= amount;
                    }
                    expenses.DeleteQuery(Convert.ToInt32(id));
                    Aqw.ItemsSource = expenses.GetData();
                }
                catch
                {
                    MessageBox.Show("Ошибка: не удалось удалить запись");
                }
                finally
                {
                    Aqw.SelectedItem = null;
                    Aqw.ItemsSource = expenses.GetData();
                    totalTbk.Text = total.ToString();
                }
            }
            else
            {
                MessageBox.Show("Ошибка: элемент для удаления не выбран");
            }
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTbx.Text))
            {
                MessageBox.Show("Ошибка: пустая строка");
            }

            else if (NameTbx.Text.FirstOrDefault(element => char.IsDigit(element)).ToString() != "\0")
            {
                MessageBox.Show("Ошибка: Строка не может содержать цифры");
            }
            else
            {
                try
                {
                    int id = Convert.ToInt32((Aqw.SelectedItem as DataRowView).Row[0]);
                    int amount = Convert.ToInt32(NameTbx.Text);
                    // проверка на отрицательное число
                    if (amount < 0)
                    {
                        expenses.UpdateQuery((cb_tip.SelectedItem as DataRowView).Row[1].ToString(), amount, DateTime.Parse(dp.Text).Date, id);
                        total += amount;
                    }
                    else
                    {
                        expenses.UpdateQuery((cb_tip.SelectedItem as DataRowView).Row[1].ToString(), amount, DateTime.Parse(dp.Text).Date, id);
                        total -= amount;
                    }
                }
                catch (System.FormatException e_)
                {
                    MessageBox.Show(e_.Message);
                }

                catch
                {
                    MessageBox.Show("Ошибка: не удалось изменить запись");
                }
                finally
                {
                    Aqw.SelectedItem = null;
                    Aqw.ItemsSource = expenses.GetData();
                    NameTbx.Text = null;
                    totalTbk.Text = total.ToString();
                }

            }
        }


        private void Aqw_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                NameTbx.Text = (Aqw.SelectedItem as DataRowView).Row[2].ToString();
            }
            catch
            {
                NameTbx.Text = null;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 Window1 = new Window1();
            Window1.Show();
        }

        private void cb_zakachik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object cell = (cb_tip.SelectedItem as DataRowView).Row[0];
            int ID = Convert.ToInt32(cell);
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dp.SelectedDate.HasValue)
            {
                var selectedDate = dp.SelectedDate.Value.Date;
                var data = expenses.GetData().Where(row => row.datetime_ == selectedDate).ToList();
                Aqw.ItemsSource = data;
            }
            else
            {
                MessageBox.Show("Ошибка: не выбрана дата");
            }

        }
    }
}
