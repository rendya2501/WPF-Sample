using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ItemsControl3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }


    public sealed class Person
    {
        public int Seq { get; }
        public string Name { get; }
        public string BirthDay { get; }
        public int Age { get; }
        public Person(int seq, string name, string birthDay, int age)
        {
            this.Seq = seq;
            this.Name = name;
            this.BirthDay = birthDay;
            this.Age = age;
        }
    }

    public class MainWindowViewModel
    {
        public ReadOnlyObservableCollection<Person> Persons { get; set; } =
            new ReadOnlyObservableCollection<Person>(
                new ObservableCollection<Person>
                {
                    new Person(1,  "北野久子"  , "1973/11/25" , 48),
                    new Person(2,  "松崎和馬"  , "1993/11/15" , 28),
                    new Person(3,  "水谷義哉"  , "1962/07/21" , 59),
                    new Person(4,  "長野佳奈"  , "2001/03/13" , 21),
                    new Person(5,  "藤島凪"    , "1974/04/18" , 47),
                    new Person(6,  "荻野亜実"  , "1968/03/02" , 54),
                    new Person(7,  "岩本葵愛"  , "1997/12/06" , 24),
                    new Person(8,  "小山田大貴", "1991/10/10" , 30),
                    new Person(9,  "宮原美明"  , "1977/10/26" , 44),
                    new Person(10, "田端利之"  , "1974/08/16" , 47),
                    new Person(11, "大下靖"    , "1968/09/14" , 53),
                    new Person(12, "那須瑞紀"  , "2000/11/15" , 21),
                    new Person(13, "門脇忠吉"  , "1978/03/25" , 44),
                    new Person(14, "冨永和奏"  , "1988/03/14" , 34),
                    new Person(15, "島本彰三"  , "1988/09/07" , 33),
                    new Person(16, "首藤和男"  , "1995/10/28" , 26),
                    new Person(17, "氏家覚"    , "1972/05/28" , 49),
                    new Person(18, "古川幸次郎", "1977/08/06" , 44),
                    new Person(19, "安藤浩俊"  , "1965/10/16" , 56),
                    new Person(20, "佐伯誠治"  , "1985/03/21" , 37),
                }
            );

        public List<Deposit> DepositTypeList { get; set; } =
            new List<Deposit>()
            {
                new Deposit(){DepositTypeName="test1",Amount=100 },
                new Deposit(){DepositTypeName="test2",Amount=200 },
                new Deposit(){DepositTypeName="test3",Amount=300 },
                new Deposit(){DepositTypeName="test4",Amount=400 },
                new Deposit(){DepositTypeName="test5",Amount=500 },
                new Deposit(){DepositTypeName="test6",Amount=600 },
            };
    }

    public sealed class Deposit
    {
        public string DepositTypeName { get; set; }
        public decimal Amount { get; set; }
    }

}
