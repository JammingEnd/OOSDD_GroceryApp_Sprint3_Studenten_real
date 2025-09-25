
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Grocery.Core.Services;
using System.Threading.Tasks;

namespace Grocery.App.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly GlobalViewModel _global;

        [ObservableProperty]
        private string email = "user3@mail.com";

        [ObservableProperty]
        private string password = "user3";

        [ObservableProperty]
        private string loginMessage;

        public LoginViewModel(IAuthService authService, GlobalViewModel global)
        { //_authService = App.Services.GetServices<IAuthService>().FirstOrDefault();
            _authService = authService;
            _global = global;
        }

        [RelayCommand]
        private async Task Login()
        {
            Client? authenticatedClient = _authService.Login(Email, Password);
            if (authenticatedClient != null)
            {
                LoginMessage = $"Welkom {authenticatedClient.Name}!";
                _global.Client = authenticatedClient;

            
                bool isHuman = await ShowCaptchaAsync();


                if (isHuman)
                    Application.Current.MainPage = new AppShell();


            }
            else
            {
                LoginMessage = "Ongeldige inloggegevens.";
            }
        }
        public async Task<bool> ShowCaptchaAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            var captchaVm = new CaptchaViewModel();
            var captchaView = new CaptchaView(captchaVm, tcs);
            await Application.Current.MainPage.ShowPopupAsync(captchaView);
            captchaView.Close();
            return await tcs.Task;
        }
    }


}
