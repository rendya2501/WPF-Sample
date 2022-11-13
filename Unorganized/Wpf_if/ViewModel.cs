using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Wpf_if
{
    public class ViewModel : BindableBase
    {
        /// <summary>
        /// カウント数
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }
        private string _Name;

        public bool Flag1 { get; set; } = true;
        public bool Flag2 { get; set; } = false;
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
