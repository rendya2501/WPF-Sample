using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace INotifyPropertyChanged1
{
    /// <summary>
    /// データバインディングサンプル1
    /// https://docs.microsoft.com/ja-jp/dotnet/api/system.componentmodel.inotifypropertychanged?view=netframework-4.8
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// 要素を変更するためのボタン
        /// </summary>
        private readonly Button changeItemBtn = new Button();
        /// <summary>
        /// データ表示用グリッドビュー
        /// </summary>
        private readonly DataGridView customersDataGridView = new DataGridView();
        /// <summary>
        /// グリッドビューのデータ
        /// </summary>
        private readonly BindingSource customersBindingSource = new BindingSource();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            // ChangeItemボタンの設定
            this.changeItemBtn.Text = "Change Item";
            this.changeItemBtn.Dock = DockStyle.Bottom;
            this.changeItemBtn.Click += new EventHandler(ChangeItemBtn_Click);
            this.Controls.Add(this.changeItemBtn);
            // DataGritViewの設定
            this.customersDataGridView.Dock = DockStyle.Top;
            this.Controls.Add(this.customersDataGridView);

            this.Size = new Size(400, 200);
        }

        /// <summary>
        /// FormLoadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // とりあえず3件ほどデータを生成する。
            // 値はDemoCustomerクラスのコンストラクタで自動でセットされる
            BindingList<DemoCustomer> customerList = new BindingList<DemoCustomer>
            {
                DemoCustomer.CreateNewCustomer(),
                DemoCustomer.CreateNewCustomer(),
                DemoCustomer.CreateNewCustomer(),
            };
            // データソースオブジェクトに代入
            this.customersBindingSource.DataSource = customerList;
            // グリッドビューに紐づけ
            this.customersDataGridView.DataSource = this.customersBindingSource;
        }


        /// <summary>
        /// クリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeItemBtn_Click(object sender, EventArgs e)
        {
            // データソースから値を取得
            BindingList<DemoCustomer> customersList =
                this.customersBindingSource.DataSource as BindingList<DemoCustomer>;
            // 最初のデータを書き換える
            // こちらから向こうのデータを書き換えただけなのに、こちら側に反映されるのが変更の通知に当たるのだろうか。
            // 普通ならDetaSoueceを書き換えたらグリッドビューのDataSourceに再代入の必要があるのに、そうしていない。
            // そうしなくても値が反映されている。
            customersList[0].CustomerName = "Tailspin Toys";
            customersList[0].PhoneNumber = "(708)555-0150";
        }
    }

    internal class DemoCustomer : INotifyPropertyChanged
    {
        private Guid idValue = Guid.NewGuid();
        private string customerNameValue = string.Empty;
        private string phoneNumberValue = string.Empty;
        /// <summary>
        /// インターフェースの自動実装
        /// プロパティが変更されたときに発生するイベント
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        /// <summary>
        /// コンストラクタ
        /// </summary>
        private DemoCustomer()
        {
            customerNameValue = "Customer";
            phoneNumberValue = "(312)555-0100";
        }

        /// <summary>
        /// public factory methodといっている。
        /// Singletonパターンではないけどインスタンスを生成して返すメソッド
        /// </summary>
        /// <returns></returns>
        public static DemoCustomer CreateNewCustomer() => new DemoCustomer();

        public Guid ID { get => this.idValue; }
        public string CustomerName
        {
            get => this.customerNameValue;
            set
            {
                // 値が同じなら何もしない
                if (value == this.customerNameValue) return;
                this.customerNameValue = value;
                this.NotifyPropertyChanged();
            }
        }
        public string PhoneNumber
        {
            get => this.phoneNumberValue;
            set
            {
                if (value != this.phoneNumberValue) return;
                this.phoneNumberValue = value;
                this.NotifyPropertyChanged();
            }
        }
    }
}
