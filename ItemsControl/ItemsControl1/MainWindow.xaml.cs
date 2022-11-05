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

namespace ItemsControl1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        class MainViewModel
        {
            public List<Store> Mall { get; set; } = Enumerable.Range(1, 20).Select(x => new Store()
            {
                Name = "お店" + x,
                Prefecture = "東京都",
                FavoriteCount = x * 10,

            }).ToList();
        }

        class Store
        {
            public string Name { get; set; }
            public string Prefecture { get; set; }
            public int FavoriteCount { get; set; }
        }
    }
}
