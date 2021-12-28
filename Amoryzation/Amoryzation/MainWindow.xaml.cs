using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

namespace Amoryzation
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        DataTable dataTable = new DataTable();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataTable = SQLClass.ReturnDT("SELECT * FROM MainTable");
            SQLClass.DTtoTrace(dataTable);
            FormatDT(dataTable);
            DT(dataTable);
            SQLClass.DTtoTrace(dataTable);
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void FormatDT(DataTable dataTable)
        {
            dataTable.Columns.Add("Цена за месяц", typeof(decimal));
            dataTable.Columns.Add("Отбитая цена", typeof(decimal));
            dataTable.Columns.Add("Остатавшаяся цена", typeof(decimal));
        }

        public void DT(DataTable dataTable)
        {
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                try
                {
                    decimal cost = Convert.ToDecimal(dataTable.Rows[i].ItemArray[2].ToString());
                    int prognoz = Convert.ToInt32(dataTable.Rows[i].ItemArray[3].ToString());
                    int fact = Convert.ToInt32(dataTable.Rows[i].ItemArray[4].ToString());
                    dataTable.Rows[i].SetField(5, cost / prognoz);
                    dataTable.Rows[i].SetField(6, (cost / prognoz) * fact);
                    dataTable.Rows[i].SetField(7, cost - ((cost / prognoz) * fact));
                }
                catch(Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
        }
    }
}
