using CommunityToolkit.Maui.Views;
using Grocery.App.ViewModels;
using Grocery.Core.Services;


namespace Grocery.App.Views;

public partial class CaptchaView : Popup
{
    public CaptchaView(CaptchaViewModel vm, TaskCompletionSource<bool> tcs)
    {
        InitializeComponent();
        BindingContext = vm;
        vm.SetCompletionSource(tcs);

    }

    private void OnCloseClicked(object sender, EventArgs e)
    {
        Close(); // dismiss popup
    }
}
