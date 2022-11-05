using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace MultiSelectComboBoxTest1
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

    public class ViewModel : ViewModelBase
    {
        public ObservableCollection<string> Data { get; } =
            new ObservableCollection<string>
            {
                "北海道",
                "青森県",
                "岩手県",
                "秋田県"
            };

        public List<(string, string)> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                NotifyPropertyChanged("Items");
            }
        }
        private List<(string key , string value)> _items =
            new List<(string key, string value)>()
            {
                ( "Chennai", "MAS" ),
                ( "Trichy", "TPJ" ),
                ( "Bangalore", "SBC" ),
                ( "Coimbatore", "CBE")
            };

        public List<(string key, string value)> SelectedItems
        {
            get
            {
                return _selectedItems;
            }
            set
            {
                _selectedItems = value;
                NotifyPropertyChanged("SelectedItems");
            }
        }
        private List<(string key, string value)> _selectedItems =
            new List<(string key, string value)>()
            {
                ( "Chennai", "MAS" ),
                ( "Trichy", "TPJ")
            };

        private void Submit()
        {
            foreach (var s in SelectedItems)
            {
                MessageBox.Show(s.Item1);
            }
        }

        public ViewModel() { }
    }


    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
