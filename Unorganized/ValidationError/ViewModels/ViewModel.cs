using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ValidationError.ViewModels
{
    public class ViewModel : BindableBase, IDataErrorInfo
    {

        private int _number = 0;
        [Required(ErrorMessage = "Required")]
        [Range(1, 100, ErrorMessage = "1から100で入力してください。")]
        public int Number
        {
            get { return _number; }
            set { SetProperty(ref _number, value); }
        }

        private string _name = string.Empty;
        [CustomValidation(typeof(ViewModel), "ValidateName")]
        [MinLength(4)]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public static ValidationResult ValidateName(string test, ValidationContext context)
        {
            if (string.IsNullOrEmpty(test)) return ValidationResult.Success;

            if (test.StartsWith("test"))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("test で始まってほしい");
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                object v = GetValue(columnName);
                ICollection<ValidationResult> results = new List<ValidationResult>();
                Validator.TryValidateProperty(v, new ValidationContext(this) { MemberName = columnName }, results);
                foreach (var item in results)
                {
                    return item.ErrorMessage;
                }
                return null;
            }
        }

        public object GetValue(string propName)
        {
            return GetValue(propName, this);
        }

        public object GetValue(string propName, object model)
        {
            if (model == null) throw new ArgumentNullException("model");

            System.Reflection.PropertyInfo prop = model.GetType().GetProperty(propName,
                System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            return prop.GetValue(model);
        }

        public ViewModel() { }
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
