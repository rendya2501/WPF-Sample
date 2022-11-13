using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Model Model { get; } = new Model();

        public MainWindow()
        {
            InitializeComponent();

            this.dataGrid.ItemsSource = new List<Person>()
            {
                new Person { No = 1, Name = "Tanaka", BirthDay = new DateTime(2000, 1, 1) },
                new Person { No = 2, Name = "Yamada", BirthDay = new DateTime(1990, 5, 5) },
                new Person { No = 3, Name = "Sato", BirthDay = new DateTime(2001, 12, 31) },
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Delay(2000).GetAwaiter().GetResult();
            MessageBox.Show("owata1");
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await Task.Delay(2000);
            MessageBox.Show("owata2");
        }
    }

    public class Person
    {
        public int No { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
    }

    public class Model
    {
        public List<Goods> Goods { get; set; } = new List<Goods>();
        public Model()
        {
            for (int i = 0; i < 10; i++)
            {
                Goods.Add(new Goods()
                {
                    _Name = "商品" + i,
                    _Price = i * 1000,
                    _isAvailable = (i % 2 == 1) ? true : false,
                    _Vender = Vendor.取引先A,
                });
            }
        }
    }
    public class Goods
    {
        public string _Name { get; set; }
        public int _Price { get; set; }
        public bool _isAvailable { get; set; }
        public Vendor _Vender { get; set; }
    }
    public enum Vendor
    {
        取引先A,
        取引先B,
        取引先C,
        取引先D,
    }
}
