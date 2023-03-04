using DemoExam2023.db;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// <summary>
    /// Логика взаимодействия для InsertAgentWindow.xaml
    /// </summary>
    public partial class InsertAgentWindow : Window
    {
        public static ToiletPaperShop_dbEntities dBEntities = new ToiletPaperShop_dbEntities();
        Product product;

        OpenFileDialog ofdImage = new OpenFileDialog();
        public InsertAgentWindow(Product product)
        {
            InitializeComponent();
            this.product = product;
            MaterialCB.ItemsSource = dBEntities.Material.ToList();
            ProductTypeCB.ItemsSource = dBEntities.TypeProd.ToList();
            TextBoxFill();
        }

        /// <summary>
        ///  Этот метод запоняет данными контейнеры
        /// </summary>
        
        void TextBoxFill()
        {
            ArticleTB.Text = Convert.ToString(product.Id_Prod);
            NameTB.Text = product.Name;
            CountTB.Text = Convert.ToString(product.Count);
            MinCostTB.Text = Convert.ToString(product.MinCostForAgent);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(ArticleTB.Text == "" || NameTB.Text == "" || MinCostTB.Text == "")
            {
                MessageBox.Show("Введите данные!");
            }
            else
            {
                Product productIns = dBEntities.Product.FirstOrDefault();
                productIns = product;
                productIns.Id_Prod = Convert.ToInt32(ArticleTB.Text);
                productIns.Name = NameTB.Text;
                productIns.MinCostForAgent = Convert.ToInt32(MinCostTB.Text);
                productIns.Picture = File.ReadAllBytes(ofdImage.FileName);
                var prodType = ((TypeProd)ProductTypeCB.SelectedItem).Id;
                productIns.Id_Type = prodType;
                var material = ((Material)MaterialCB.SelectedItem).id;
                productIns.Id_Material = material;
                productIns.Count = Convert.ToInt32(CountTB.Text);
                try
                {
                    dBEntities.Product.Add(product);
                    dBEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Не правильные данные!");
                }
                finally
                {
                    MessageBox.Show("Выполнено!");
                    AgentListWindow agentWindow = new AgentListWindow();
                    agentWindow.AgentLst.ItemsSource = dBEntities.Product.ToList();
                }


            }
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            DelWindow delWindow = new DelWindow();
            delWindow.Show();
            this.Close();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            AgentListWindow agentWindow = new AgentListWindow();
            agentWindow.Show();
            this.Close();
        }

        private void ImgBtn_Click(object sender, RoutedEventArgs e)
        {
            ofdImage.Filter = "Image files|*.bmp;*.jpg;*.png|All files|*.*";
            ofdImage.FilterIndex = 1;
            if (ofdImage.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(ofdImage.FileName);
                image.EndInit();
                playim.Source = image;
            }
        }

        private void DelImgBtn_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image.Freeze();
            playim.Source = image;
        }
    }
}
