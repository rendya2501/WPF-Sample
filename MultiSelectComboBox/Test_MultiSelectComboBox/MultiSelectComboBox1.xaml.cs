using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MultiSelectComboBoxTest1
{
    /// <summary>
    /// Interaction logic for MultiSelectComboBox.xaml
    /// https://www.codeproject.com/Articles/563862/Multi-Select-ComboBox-in-WPF
    /// </summary>
    public partial class MultiSelectComboBox1 : UserControl
    {
        #region 依存プロパティ
        #region ItemsSource
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IList),
                typeof(MultiSelectComboBox1),
                new FrameworkPropertyMetadata(
                    null,
                    new PropertyChangedCallback(OnItemsSourceChanged)
                )
            );
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        #endregion

        #region SelectedItems
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(
                "SelectedItems",
                typeof(IList),
                typeof(MultiSelectComboBox1),
                new FrameworkPropertyMetadata(
                    null,
                    new PropertyChangedCallback(OnSelectedItemsChanged)
                )
            );
        public IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }
        #endregion

        #region DisplayMemberr
        /// <summary>
        /// DisplayMemberProperty依存関係プロパティ登録
        /// </summary>
        public static readonly DependencyProperty DisplayMemberProperty =
            DependencyProperty.RegisterAttached(
                "DisplayMember",
                typeof(string),
                typeof(MultiSelectComboBox1),
                new PropertyMetadata(null)
            );
        /// <summary>
        /// チェックボックスのテキストに表示するプロパティ名
        /// </summary>
        public string DisplayMember
        {
            get { return (string)GetValue(DisplayMemberProperty); }
            set { SetValue(DisplayMemberProperty, value); }
        }
        #endregion

        #region MaxHeaderItemsProperty
        /// <summary>
        /// MaxHeaderItemsProperty依存関係プロパティ登録
        /// </summary>
        public static readonly DependencyProperty MaxHeaderItemsProperty =
            DependencyProperty.Register(
                "MaxHeaderItems",
                typeof(int),
                typeof(MultiSelectComboBox1),
                new PropertyMetadata(0)
            );
        /// <summary>
        /// コントロールヘッダーに表示する項目の最大数を取得または設定します。
        /// </summary>
        public int MaxHeaderItems
        {
            get { return (int)GetValue(MaxHeaderItemsProperty); }
            set { SetValue(MaxHeaderItemsProperty, value); }
        }
        #endregion

        #region HeaderFormatProperty
        /// <summary>
        /// MaxHeaderItemsProperty依存関係プロパティ登録
        /// </summary>
        public static readonly DependencyProperty HeaderFormatProperty =
            DependencyProperty.Register(
                "HeaderFormat",
                typeof(string),
                typeof(MultiSelectComboBox1),
                new PropertyMetadata("items selected")
            );
        /// <summary>
        /// コントロール内の選択項目が maxHeaderItems より多いときに、ヘッダーコンテンツの作成に使用される書式文字列を取得または設定します。
        /// </summary>
        public string HeaderFormat
        {
            get { return (string)GetValue(HeaderFormatProperty); }
            set { SetValue(HeaderFormatProperty, value); }
        }
        #endregion

        #region SelectAllContent
        public static readonly DependencyProperty SelectAllContentProperty = DependencyProperty.Register(
            "SelectAllContent",
            typeof(string),
            typeof(MultiSelectComboBox1),
            new UIPropertyMetadata("Select All")
        );
        public string SelectAllContent
        {
            get { return (string)GetValue(SelectAllContentProperty); }
            set { SetValue(SelectAllContentProperty, value); }
        }
        #endregion

        #region Text
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(MultiSelectComboBox1),
                new UIPropertyMetadata(string.Empty)
            );
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region DefaultText
        public static readonly DependencyProperty DefaultTextProperty =
            DependencyProperty.Register(
                "DefaultText",
                typeof(string),
                typeof(MultiSelectComboBox1),
                new UIPropertyMetadata(string.Empty)
            );
        public string DefaultText
        {
            get { return (string)GetValue(DefaultTextProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }
        #endregion
        #endregion

        #region NodeList
        /// <summary>
        /// ノードリスト
        /// </summary>
        /// <remarks>
        /// チェックボックスの実体
        /// </remarks>
        private ObservableCollection<Node> NodeList { get; set; } = new ObservableCollection<Node>();
        #endregion


        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MultiSelectComboBox1()
        {
            InitializeComponent();
        }
        #endregion


        #region イベント
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiSelectComboBox1 control = (MultiSelectComboBox1)d;
            control.DisplayInControl();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiSelectComboBox1 control = (MultiSelectComboBox1)d;
            control.SelectNodes();
            control.SetText();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox clickedBox = (CheckBox)sender;

            // 全選択ボタンを押下した場合
            if (clickedBox.Content != null && clickedBox.Content.ToString() == "All")
            {
                if (clickedBox.IsChecked.HasValue && clickedBox.IsChecked.Value)
                {
                    foreach (var node in NodeList)
                    {
                        node.IsSelected = true;
                    }
                }
                else
                {
                    foreach (var node in NodeList)
                    {
                        node.IsSelected = false;
                    }
                }
            }
            // 通常アイテムをクリックした場合
            else
            {
                // 選択状態の通常アイテム数を取得
                var selectedCount = NodeList.Count(s => s.IsSelected && s.Title != "All");
                // 全選択ノードを取得
                var node = NodeList.FirstOrDefault(i => i.Title == "All");
                if (node != null)
                {
                    // 全ての通常アイテムを選択していたら全選択ボタンを選択状態にする。
                    node.IsSelected = selectedCount == NodeList.Count - 1;

                    // 通常アイテムを1つ以上、全選択未満の場合、全選択ボタンを第3状態とする

                    // 通常アイテムを1つも選択していない場合
                }
            }
            SetSelectedItems();
            SetText();
        }
        #endregion


        #region 内部処理
        /// <summary>
        /// ノードを初期化します。
        /// </summary>
        private void DisplayInControl()
        {
            NodeList.Clear();

            // ALLノードの生成
            if (ItemsSource.Count > 0)
            {
                NodeList.Add(new Node("All"));
            }
            // ItemsSourceをノードに変換
            foreach (var item in ItemsSource)
            {
                NodeList.Add(new Node(item.ToString()));
                var aa = this.MultiSelectCombo;
            }

            MultiSelectCombo.ItemsSource = NodeList;
        }

        /// <summary>
        /// ノードを選択状態にします
        /// </summary>
        private void SelectNodes()
        {
            if (SelectedItems == null)
            {
                return;
            }

            foreach (var item in SelectedItems)
            {
                var node = NodeList.FirstOrDefault(i => i.Title == item.ToString());
                if (node != null)
                {
                    node.IsSelected = true;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetSelectedItems()
        {
            SelectedItems.Clear();

            foreach (var node in NodeList)
            {
                // 選択されていないノードまたは全選択ノードの場合は関係ないのでスキップ
                if (!node.IsSelected || node.Title == "All")
                {
                    continue;
                }
                if (ItemsSource.Count <= 0)
                {
                    continue;
                }

                SelectedItems.Add(ItemsSource.OfType<object>().ToList().FirstOrDefault(i => i.ToString() == node.Title));
            }
        }

        /// <summary>
        /// テキストをセットします
        /// </summary>
        private void SetText()
        {
            if (SelectedItems != null)
            {
                StringBuilder displayText = new StringBuilder();
                foreach (Node s in NodeList)
                {
                    if (s.IsSelected == true && s.Title == "All")
                    {
                        displayText = new StringBuilder().Append("All");
                        break;
                    }
                    else if (s.IsSelected == true && s.Title != "All")
                    {
                        displayText.Append(s.Title);
                        displayText.Append(',');
                    }
                }

                // 最大数が設定されているときだけ文言を変更する
                if (MaxHeaderItems <= 0)
                {
                    Text = displayText.ToString().TrimEnd(new char[] { ',' });
                }
                else
                {
                    // 選択件数が設定した最大表示件数を超えた場合、設定した文言に変更する。
                    // そうでなければ、選択したアイテム名を羅列して表示する
                    Text = SelectedItems.Count > MaxHeaderItems
                        ? $"{SelectedItems.Count}{HeaderFormat}"
                        : displayText.ToString().TrimEnd(new char[] { ',' });
                }
            }

            // 何も選択されていない場合、デフォルトのテキストを表示する
            if (string.IsNullOrEmpty(Text))
            {
                Text = DefaultText;
            }
        }
        #endregion

        #region 内部クラス
        /// <summary>
        /// ノードクラス
        /// </summary>
        /// <remarks>
        /// チェックボックスとして羅列される実体です。
        /// </remarks>
        private class Node : INotifyPropertyChanged
        {
            #region Properties
            /// <summary>
            /// 表示されるテキスト
            /// </summary>
            public string Title
            {
                get
                {
                    return _title;
                }
                set
                {
                    _title = value;
                    NotifyPropertyChanged(nameof(Title));
                }
            }
            private string _title;

            /// <summary>
            /// 選択状態
            /// </summary>
            public bool IsSelected
            {
                get
                {
                    return _isSelected;
                }
                set
                {
                    _isSelected = value;
                    NotifyPropertyChanged(nameof(IsSelected));
                }
            }
            private bool _isSelected;
            #endregion

            /// <summary>
            /// PropertyChangedイベント
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            /// NotifyPropertyChanged
            /// </summary>
            /// <param name="propertyName"></param>
            protected void NotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="title"></param>
            public Node(string title) => Title = title;
        }
        #endregion
    }
}
