using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace Wpf_RadioButton_Implement_on_ListBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //var tmp = NetworkInterface.GetAllNetworkInterfaces();
            //this.DataContext = tmp;

            var tmp2 = new List<SimpleList>
            {
                new SimpleList() { CD = 0, Name = "共通" },
                new SimpleList() { CD = 10, Name = "フロント" },
                new SimpleList() { CD = 20, Name = "スタート" },
                new SimpleList() { CD = 40, Name = "ショップ" },
                new SimpleList() { CD = 50, Name = "ハウス売店" },
                new SimpleList() { CD = 70, Name = "その他" }
            };
            this.DataContext = tmp2;
            InitializeComponent();
        }


        /// <summary>
        /// xaml表示テスト用
        /// </summary>
        private class SimpleList
        {
            /// <summary>
            /// コード
            /// </summary>
            [Display(Name = "コード")]
            public int CD { get; set; }
            /// <summary>
            /// 名称
            /// </summary>
            [Display(Name = "名称")]

            public string Name { get; set; }
        }
    }
}
