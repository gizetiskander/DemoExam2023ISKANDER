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
    /// Логика взаимодействия для AddAgentWindow.xaml
    /// </summary>
    public partial class AddAgentWindow : Window
    {

        OpenFileDialog ofdImage = new OpenFileDialog();
        public static ToiletPaperShop_dbEntities dBEntities = new ToiletPaperShop_dbEntities();
        public AddAgentWindow()
        {
            InitializeComponent();
            MaterialCB.ItemsSource = dBEntities.Material.ToList();
            ProductTypeCB.ItemsSource = dBEntities.TypeProd.ToList();
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

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ArticleTB.Text == "" || NameTB.Text == "" || MinCostTB.Text == "")
            {
                MessageBox.Show("Введите данные!");
            }
            else
            {
                Product product = new Product();
                product.Id_Prod = Convert.ToInt32(ArticleTB.Text);
                product.Name = NameTB.Text;
                product.MinCostForAgent = Convert.ToInt32(MinCostTB.Text);
                product.Picture = File.ReadAllBytes(ofdImage.FileName);
                var prodType = ((TypeProd)ProductTypeCB.SelectedItem).Id;
                product.Id_Type = prodType;
                var material = ((Material)MaterialCB.SelectedItem).id;
                product.Id_Material = material;
                product.Count = Convert.ToInt32(CountTB.Text);
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
                }


            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            AgentListWindow agentListWindow = new AgentListWindow();
            agentListWindow.Show();
            this.Close();
        }
    }
}
