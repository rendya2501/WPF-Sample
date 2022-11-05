using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MVVM_ToolKit_dotnetFramework.ViewModels
{
    public partial class ViewModel : ObservableObject
    {
        public string _FirstName;
        public string FirstName
        {
            get => _FirstName;
            set => SetProperty(ref _FirstName, value, nameof(FullName));
        }

        public string _LastName;
        public string LastName
        {
            get => _LastName;
            set => SetProperty(ref _LastName, value, nameof(FullName));
        }

        public bool _IsBusy;
        public bool IsBusy
        {
            get => _IsBusy;
            set => SetProperty(ref _IsBusy, value, nameof(IsNotBusy));
        }

        public string FullName => $"{FirstName},{LastName}";

        public bool IsNotBusy => !IsBusy;

        public RelayCommand TapCommand => new RelayCommand(async () =>
        {
            IsBusy = true;
            await Task.Delay(200);
            Trace.WriteLine(FullName);
            IsBusy = false;
        });
    }
}
