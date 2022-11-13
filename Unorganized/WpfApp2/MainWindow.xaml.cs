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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // このウィンドウに対するバインドで、個別のコントロールに対してバインドすることも可能な模様。
            // というか、普通そうするか。
            //this.Test1.DataContext = ~~;
            //this.Test2.DataContext = ~~;
            // こんな具合に
            this.DataContext = new MainViewModel();

            // しかし、万君が言っていたことはそのままだったな。
            // Viewに対するViewModelを1つ定義してバインドすればよい。
            // 複数のバインドってのは、プロパティが複数あるだけで、意味合い的にはそうなるわな。
            // しかし、前のサンプルではどうしてうまくいかなかったのか。
            // それは、ViewとViewModelを1対1でバインドしてViewModel側でプロパティを定義するような形にしなかったからだろう。
            // コードビハインドで適当なデータを定義して、それをDataContextに入れただけだから、コンボボックスとテキストに対して個別の値をバインドさせることができなかったのだ。
            // 反面教師としていいサンプルなので残しておこう。
        }

        /// <summary>
        /// DataContextの内容を調べるためのサンプル
        /// バインドしたデータ型にキャストするとバインドした中身を取得できる。
        /// https://techlive.tokyo/archives/2216
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // バインドした値を2wayにして本当に変わっているのか確かめるためのサンプル
            // ちゃんと変わっていた。割と感動。
            MainViewModel text = this.DataContext as MainViewModel;
            MessageBox.Show(text.BindText2);
        }
    }
}
