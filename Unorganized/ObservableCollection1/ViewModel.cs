using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ObservableCollection1
{
    public class ViewModel
    {
        public ObservableCollection<Person> People { get; set; } =
            new ObservableCollection<Person>(
                Enumerable.Range(1, 10).Select(x => new Person { Name = "tanaka" + x, Age = x })
            );
        /// <summary>
        /// コレクションに要素を追加する
        /// </summary>
        public DelegateCommand AddItem =>
            new DelegateCommand(
                () => People.Add(new Person { Name = "追加したtanaka", Age = People.Count + 1 }
            )
        );
        /// <summary>
        /// コレクションの要素を削除する。
        /// </summary>
        public DelegateCommand RemoveItem =>
            new DelegateCommand(
                () => People.RemoveAt(People.Count - 1
            )
        );

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ViewModel()
        {
            People.CollectionChanged += People_CollectionChanged;
        }

        /// <summary>
        /// ObservableCollectionのAddやRemoveされた時実行される処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void People_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Action for this event: {0}", e.Action);

            switch (e.Action)
            {
                // They added something. 
                case NotifyCollectionChangedAction.Add:
                    // Now show the NEW items that were inserted.
                    Console.WriteLine("Here are the NEW items:");
                    foreach (Person p in e.NewItems)
                    {
                        Console.WriteLine(p.ToString());
                    }
                    break;
                // They removed something. 
                case NotifyCollectionChangedAction.Remove:
                    Console.WriteLine("Here are the OLD items:");
                    foreach (Person p in e.OldItems)
                    {
                        Console.WriteLine(p.ToString());
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Reset:
                default:
                    break;
            }

            Console.WriteLine();
        }
    }

    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string? Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }
        private string? _Name;

        public int Age
        {
            get => _Age;
            set => SetProperty(ref _Age, value);
        }
        private int _Age;
    }
}
