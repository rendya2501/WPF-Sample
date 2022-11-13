using System;
using System.Collections.Generic;
using System.Text;

namespace INotifyPropertyChanged2
{
    public class Person : BindableBase
    {
        private string name;
        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }
        public Person()
        {
        }

        public void HogeChange()
        {
            this.Name = "hoge";
        }
    }
}
