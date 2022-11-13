using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    class PrefList
    {
        public ObservableCollection<string> Data { get; }
        public PrefList()
        {
            Data = new ObservableCollection<string>();
            Data.Add("北海道");
            Data.Add("青森県");
            Data.Add("岩手県");
            Data.Add("秋田県");
        }
    }
}
