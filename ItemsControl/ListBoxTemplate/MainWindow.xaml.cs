namespace ListBoxTemplate
{
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MyStrings = new ObservableCollection<string>();

            for (int i = 0; i < 15; i++)
            {
                MyStrings.Add("This is a long sample text to test a listbox scroller...");
            }
        }

        public ObservableCollection<string> MyStrings { get; }
    }
}
