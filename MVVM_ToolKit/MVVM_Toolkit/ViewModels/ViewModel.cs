using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MVVM_Toolkit.ViewModels
{
    public partial class ViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        public string? firstName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        public string? lastName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        public bool isBusy;

        public string FullName => $"{FirstName},{LastName}";

        public bool IsNotBusy => !IsBusy;

        [RelayCommand]
        public async void Tap()
        {
            IsBusy = true;
            await Task.Delay(200);
            Trace.WriteLine(FullName);
            IsBusy = false;
        }
    }
}