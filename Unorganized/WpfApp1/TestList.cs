using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class TestList
    {
        public ObservableCollection<Test> TestListData { get; }

        public TestList()
        {
            TestListData = new ObservableCollection<Test>
            {
                new Test {Subj = "国語", Points = 90, Name = "田中 一郎", ClassName = "A"},
                new Test {Subj = "数学", Points = 50, Name = "鈴木 二郎", ClassName = "A"},
                new Test {Subj = "英語", Points = 90, Name = "佐藤 三郎", ClassName = "B"}
            };
        }
    }
}
