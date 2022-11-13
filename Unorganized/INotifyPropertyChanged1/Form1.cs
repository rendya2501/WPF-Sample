using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace INotifyPropertyChanged1
{
    /// <summary>
    /// �f�[�^�o�C���f�B���O�T���v��1
    /// https://docs.microsoft.com/ja-jp/dotnet/api/system.componentmodel.inotifypropertychanged?view=netframework-4.8
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// �v�f��ύX���邽�߂̃{�^��
        /// </summary>
        private readonly Button changeItemBtn = new Button();
        /// <summary>
        /// �f�[�^�\���p�O���b�h�r���[
        /// </summary>
        private readonly DataGridView customersDataGridView = new DataGridView();
        /// <summary>
        /// �O���b�h�r���[�̃f�[�^
        /// </summary>
        private readonly BindingSource customersBindingSource = new BindingSource();

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            // ChangeItem�{�^���̐ݒ�
            this.changeItemBtn.Text = "Change Item";
            this.changeItemBtn.Dock = DockStyle.Bottom;
            this.changeItemBtn.Click += new EventHandler(ChangeItemBtn_Click);
            this.Controls.Add(this.changeItemBtn);
            // DataGritView�̐ݒ�
            this.customersDataGridView.Dock = DockStyle.Top;
            this.Controls.Add(this.customersDataGridView);

            this.Size = new Size(400, 200);
        }

        /// <summary>
        /// FormLoad�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // �Ƃ肠����3���قǃf�[�^�𐶐�����B
            // �l��DemoCustomer�N���X�̃R���X�g���N�^�Ŏ����ŃZ�b�g�����
            BindingList<DemoCustomer> customerList = new BindingList<DemoCustomer>
            {
                DemoCustomer.CreateNewCustomer(),
                DemoCustomer.CreateNewCustomer(),
                DemoCustomer.CreateNewCustomer(),
            };
            // �f�[�^�\�[�X�I�u�W�F�N�g�ɑ��
            this.customersBindingSource.DataSource = customerList;
            // �O���b�h�r���[�ɕR�Â�
            this.customersDataGridView.DataSource = this.customersBindingSource;
        }


        /// <summary>
        /// �N���b�N�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeItemBtn_Click(object sender, EventArgs e)
        {
            // �f�[�^�\�[�X����l���擾
            BindingList<DemoCustomer> customersList =
                this.customersBindingSource.DataSource as BindingList<DemoCustomer>;
            // �ŏ��̃f�[�^������������
            // �����炩��������̃f�[�^�����������������Ȃ̂ɁA�����瑤�ɔ��f�����̂��ύX�̒ʒm�ɓ�����̂��낤���B
            // ���ʂȂ�DetaSouece��������������O���b�h�r���[��DataSource�ɍđ���̕K�v������̂ɁA�������Ă��Ȃ��B
            // �������Ȃ��Ă��l�����f����Ă���B
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
        /// �C���^�[�t�F�[�X�̎�������
        /// �v���p�e�B���ύX���ꂽ�Ƃ��ɔ�������C�x���g
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        private DemoCustomer()
        {
            customerNameValue = "Customer";
            phoneNumberValue = "(312)555-0100";
        }

        /// <summary>
        /// public factory method�Ƃ����Ă���B
        /// Singleton�p�^�[���ł͂Ȃ����ǃC���X�^���X�𐶐����ĕԂ����\�b�h
        /// </summary>
        /// <returns></returns>
        public static DemoCustomer CreateNewCustomer() => new DemoCustomer();

        public Guid ID { get => this.idValue; }
        public string CustomerName
        {
            get => this.customerNameValue;
            set
            {
                // �l�������Ȃ牽�����Ȃ�
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
