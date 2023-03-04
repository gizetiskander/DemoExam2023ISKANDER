using DemoExam2023.db;
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

namespace DemoExam2023.Pages
{
    
    public partial class DelWindow : Window
    {
        public static ToiletPaperShop_dbEntities dBEntities = new ToiletPaperShop_dbEntities();
        public DelWindow()
        {
            InitializeComponent();
            AgentLst.ItemsSource = dBEntities.Product.ToList();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            AgentListWindow agentWindow = new AgentListWindow();
            agentWindow.Show();
            this.Close();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var q = AgentLst.SelectedItem as Product;
            if (q == null)
            {
                MessageBox.Show("Ничего не выбрано!");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить строку?", "Удалить?", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    dBEntities.Product.Remove(q);
                    AgentLst.ItemsSource = dBEntities.Product.ToList();
                    dBEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Ошибка!");
                }
            }
        }
    }
}
