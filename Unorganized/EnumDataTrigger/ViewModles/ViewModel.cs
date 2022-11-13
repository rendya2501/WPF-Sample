using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EnumDataTrigger.Enum;

namespace EnumDataTrigger.ViewModles
{
    public class ViewModel : BindableBase
    {
        public State State
        {
            get { return _State; }
            set { SetProperty(ref _State, value); }
        }
        private State _State;

        public DelegateCommand ButtonCommand => new DelegateCommand(
            () => State = (State == State.Error) ? State.Normal : ++State
        );

        void aaa()
        {
            int i = 0;
            i += i + 1;
            ++i;
            i++;
            i = i > 3 ? 1 : ++i;
        }
    }

    /// <summary>
    /// INotifyPropertyChangedのヘルパークラス
    /// </summary>
    public class BindableBase : INotifyPropertyChanged
    {
        /// <summary>
        /// INotifyPropertyChangedインターフェース実装イベント
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 値のセットと通知を行う
        /// </summary>
        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
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
}
