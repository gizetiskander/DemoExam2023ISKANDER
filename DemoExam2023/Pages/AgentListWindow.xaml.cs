using DemoExam2023.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public partial class AgentListWindow : Window
    {
        //начал делать, как агентов, потом увидел, что лопушок из-за этого названия приближены к агентам

        public static ToiletPaperShop_dbEntities dBEntities = new ToiletPaperShop_dbEntities();
        public AgentListWindow()
        {
            InitializeComponent();
            dBEntities.SaveChanges();
            RefreshComboBox();
            RefreshButtons();
            AgentLst.ItemsSource = dBEntities.Product.ToList();

            foreach (var serv in AgentListWindow.dBEntities.TypeProd)
            {
                FilterCB.ItemsSource = dBEntities.TypeProd.ToList();

            }
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(AgentLst.ItemsSource);
            view.Filter = UserFilter;
        }

        int pageSize;
        int pageNumber;
        List<Product> prod1 = dBEntities.Product.ToList();

        private void RefreshComboBox()
        {
            CBNumberWrite.Items.Add("20");
        }


        private void CBNumberWrite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pageSize = Convert.ToInt32(CBNumberWrite.SelectedItem.ToString());
            RefreshPagination();
            RefreshButtons();
        }


        private void AgentLst_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var insert = AgentLst.SelectedItem as Product;
            InsertAgentWindow insertAgentWindow = new InsertAgentWindow(insert);
            insertAgentWindow.Show();
            this.Close();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var TBSQ = dBEntities.Product.OrderBy(a => a.Name).ToList();
            TBSQ = TBSQ.Where(a => a.Name.ToLower().Contains(SearchTB.Text.ToLower())).ToList();
            AgentLst.ItemsSource = TBSQ;
        }


        private void FilterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var typeName = ((TypeProd)FilterCB.SelectedItem).NameType;
            var type = AgentListWindow.dBEntities.TypeProd.Where(x => x.NameType == typeName).FirstOrDefault();
            AgentLst.ItemsSource = dBEntities.Product.Where(x => x.TypeProd.NameType == typeName).ToList();


        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchTB.Text))
                return true;
            else
                return ((item as Product).Name.IndexOf(SearchTB.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortCB.SelectedIndex == 0)
            {
                AgentLst.ItemsSource = dBEntities.Product.ToList();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(AgentLst.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));


            }
            else if (SortCB.SelectedIndex == 1)
            {
                AgentLst.ItemsSource = dBEntities.Product.ToList();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(AgentLst.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));


            }
            else if (SortCB.SelectedIndex == 2)
            {
                AgentLst.ItemsSource = dBEntities.Product.ToList();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(AgentLst.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("MinCostForAgent", ListSortDirection.Ascending));

            }
            else if (SortCB.SelectedIndex == 3)
            {
                AgentLst.ItemsSource = dBEntities.Product.ToList();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(AgentLst.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("MinCostForAgent", ListSortDirection.Descending));

            }
            else if (SortCB.SelectedIndex == 4)
            {
                AgentLst.ItemsSource = dBEntities.Product.ToList();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(AgentLst.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("Id_Prod", ListSortDirection.Ascending));

            }
            else if (SortCB.SelectedIndex == 5)
            {
                AgentLst.ItemsSource = dBEntities.Product.ToList();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(AgentLst.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("Id_Prod", ListSortDirection.Descending));

            }
        }


        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddAgentWindow addAgentWindow = new AddAgentWindow();
            addAgentWindow.Show();
            this.Close();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            AgentLst.ItemsSource = dBEntities.Product.ToList();

            SortCB.SelectedItem = null;
            SearchTB.Clear();
        }

        private void RefreshPagination()
        {
            AgentLst.ItemsSource = null;
            AgentLst.ItemsSource = prod1.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        private void RefreshButtons()
        {
            WPButtons.Children.Clear();
            if (prod1.Count % pageSize == 0)
            {
                for (int i = 0; i < prod1.Count / pageSize; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += RefreshBtn_Click;
                    button.Margin = new Thickness(0, 5, 0, 5);
                    button.Width = 25;
                    button.Height = 25;
                    button.BorderBrush = Brushes.White;
                    button.Background = Brushes.White;
                    button.FontSize = 14;
                    WPButtons.Children.Add(button);
                }
            }
            else
            {
                for (int i = 0; i < prod1.Count / pageSize; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += RefreshBtn_Click;
                    button.Margin = new Thickness(0, 5, 0, 5);
                    button.Width = 25;
                    button.Height = 25;
                    button.BorderBrush = Brushes.White;
                    button.Background = Brushes.White;
                    button.FontSize = 14;
                    WPButtons.Children.Add(button);
                }
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            pageNumber = Convert.ToInt32(button.Content) - 1;
            RefreshPagination();
        }

        private void PagLeft_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber == 0)
                return;
            pageNumber--;
            RefreshPagination();
        }

        private void PagRight_Click(object sender, RoutedEventArgs e)
        {
            if (prod1.Count % pageSize == 0)
            {
                if (pageNumber == (prod1.Count / pageSize) - 1)
                    return;
            }
            else
            {

                if (pageNumber == (prod1.Count / pageSize))
                    return;
            }
            pageNumber++;
            RefreshPagination();
        }
    }
}
