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

    public partial class AgentListWindow : Window
    {
        public static MaterialDBEntities dBEntities = new MaterialDBEntities();
        public AgentListWindow()
        {
            InitializeComponent();
        }
    }
}
