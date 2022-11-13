using System.Windows;

namespace INotifyPropertyChanged2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Person person;
        public MainWindow()
        {
            InitializeComponent();
            this.person = new Person();
            // DataContextにモデルであるPrtsonクラスをバインド
            this.DataContext = this.person;
            // PersonクラスのNameプロパティの値を"Hello"に書き換える
            person.Name = "Hello";
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.person.HogeChange();
        }
    }
}
