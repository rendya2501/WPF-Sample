using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace INotifyPropertyChanged3
{
    internal class ViewModel : BindableBase, INotifyPropertyChanged
    {
        /// <summary>
        /// INotifyPropertyChanged自動実装イベント
        /// 変数の更新通知用
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 変数(カウント数)
        /// </summary>
        public int Count
        {
            get { return _Count; }
            set
            {
                _Count = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
            }

        }
        private int _Count;

        private string _Name;
        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }

        /// <summary>
        /// プレーンでのコマンド実装
        /// </summary>
        public ICommand PlaneCountDownCommand => new CountDownCommand(() => Count++);

        /// <summary>
        /// Prismでのコマンド実装
        /// </summary>
        public DelegateCommand DelegateCountDownCommand => new DelegateCommand(() => Count++);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ViewModel()
        {
            // コマンド登録
            // カウント数を増やす
            // 実務でもコンストラクタで記述していたが、プロパティの段階で代入することがわかった
            //PlaneCountDownCommand = new RelayCommand(() => Count++);
            //DelegateCountDownCommand = new DelegateCommand(() => Count++);
        }
    }

    /// <summary>
    /// INotifyPropertyChangedのバインドヘルパークラス
    /// https://blog.okazuki.jp/entry/2014/12/23/180413
    /// </summary>
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            // 同じ値なら処理しない
            if (Equals(field, value))
            {
                return false;
            }
            // 値を反映
            field = value;
            // プロパティ発火
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // 正常に終了したことを通知
            return true;
        }
    }

    /// <summary>
    /// MVVMのViewModelから呼び出されるコマンドの定義
    /// コマンド１つにつき１つのクラスを定義する
    /// </summary>
    public class CountDownCommand : ICommand
    {
        /// <summary>
        /// Command実行時に実行するアクション
        /// 引数を受け取りたい場合はこのActionをAction<object>などにする
        /// </summary>
        private Action _action;

        /// <summary>
        /// コンストラクタ
        /// Actionを登録
        /// </summary>
        /// <param name="action"></param>
        public CountDownCommand(Action action)
        {
            _action = action;
        }

        #region ICommandインターフェースの必須実装
        /// <summary>
        /// コマンドのルールとして必ず実装しておくイベントハンドラ
        /// 通常、このメソッドを丸ごとコピーすればOK
        /// RaiseCanExecuteChanged が呼び出されたときに生成されます。
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// コマンドの有効／無効を判定するメソッド
        /// コマンドのルールとして必ず実装しておくメソッド
        /// 有効／無効を制御する必要が無ければ、無条件にTrueを返しておく
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            //とりあえずActionがあれば実行可能
            return _action != null;
        }

        /// <summary>
        /// コマンドの動作を定義するメソッド
        /// コマンドのルールとして必ず実装しておくメソッド
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter)
        {
            //今回は引数を使わずActionを実行
            _action?.Invoke();
        }
        #endregion
    }


    /// <summary>
    /// CanExecuteも実装したタイプだけど、そこまで実装するならDelegateCommand使ったほうがいいよ。
    /// </summary>
    class RelayCommand : ICommand
    {
        /// <summary>
        /// Command実行時に実行するアクション、引数を受け取りたい場合はこのActionをAction<object>などにする
        /// </summary>
        private readonly Action? _execute;
        private readonly Func<bool>? _canExecute;

        /// <summary>
        /// コンストラクタ
        /// 常に実行可能な新しいコマンドを作成します。
        /// </summary>
        /// <param name="execute">実行ロジック</param>
        public RelayCommand(Action execute) : this(execute, null) { }

        /// <summary>
        /// コンストラクタ
        /// 新しいコマンドを作成します。
        /// </summary>
        /// <param name="execute">実行ロジック</param>
        /// <param name="canExecute">実行ステータス ロジック</param>
        public RelayCommand(Action execute, Func<bool>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public void RaiseCanExcuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// RaiseCanExecuteChanged が呼び出されたときに生成されます。
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// 現在の状態でこの <see cref="RelayCommand"/> が実行できるかどうかを判定します。
        /// </summary>
        /// <param name="parameter">
        /// コマンドによって使用されるデータ。コマンドが、データの引き渡しを必要としない場合、このオブジェクトを null に設定できます。
        /// </param>
        /// <returns>このコマンドが実行可能な場合は true、それ以外の場合は false。</returns>
        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute();

        /// <summary>
        /// 現在のコマンド ターゲットに対して <see cref="RelayCommand"/> を実行します。
        /// </summary>
        /// <param name="parameter">
        /// コマンドによって使用されるデータ。コマンドが、データの引き渡しを必要としない場合、このオブジェクトを null に設定できます。
        /// </param>
        public void Execute(object? parameter) => _execute?.Invoke();
    }
}
