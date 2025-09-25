using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App;
using Grocery.App.ViewModels;
using Grocery.App.Views;
using Grocery.Core.Services;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.App.ViewModels
{
    public partial class CaptchaViewModel : BaseViewModel
    {
        private TaskCompletionSource<bool>? _tcs;

        private const double Step = 0.85;
        private const double Tolerance = 1;

        [ObservableProperty]
        private double shapeX;

        [ObservableProperty]
        private double shapeY;

        [ObservableProperty]
        private string timeLeftString= string.Empty;

        private int timeLeft = 0;
        public CaptchaViewModel()
        {
            SetShape();
        }

        private void SetShape()
        {
            // Randomize start position
            Random rnd = new();
            // Randomly choose horizontal or vertical edge
            bool spawnHorizontal = rnd.Next(0, 2) == 0;

            if (spawnHorizontal)
            {
                // Top or bottom edge
                ShapeY = rnd.Next(70, 100) * (rnd.Next(0, 2) == 0 ? 1 : -1);
                ShapeX = rnd.Next(-80, 80); // anywhere along X
            }
            else
            {
                // Left or right edge
                ShapeX = rnd.Next(70, 100) * (rnd.Next(0, 2) == 0 ? 1 : -1);
                ShapeY = rnd.Next(-80, 80); // anywhere along Y
            }
        }

        public void SetCompletionSource(TaskCompletionSource<bool> tcs)
        {
            _tcs = tcs;
            StartTimer(15);
        }

        private System.Timers.Timer? _timer;

        private void StartTimer(int durationInSeconds)
        {
            timeLeft = durationInSeconds;
            UpdateTimeLeft(timeLeft);
            _timer = new System.Timers.Timer(1000); // 1 second intervals
            _timer.Elapsed += (s, e) =>
            {
                timeLeft--;
                UpdateTimeLeft(timeLeft);
                if (timeLeft <= 0)
                {
                    _timer.Stop();
                    _tcs?.TrySetResult(false); // Time's up, fail the captcha
                }
            };
            _timer.Start();
        }

        private void UpdateTimeLeft(int seconds)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TimeLeftString = $"00:{seconds:D2}";
                if (seconds <= 0)
                {
                    Reset();
                }
            });
        }

        private void Reset()
        {
            SetShape();
            StartTimer(15);
        }

        [RelayCommand]
        private void MoveUp() => ShapeY -= Step;

        [RelayCommand]
        private void MoveDown() => ShapeY += Step;

        [RelayCommand]
        private void MoveLeft() => ShapeX -= Step;

        [RelayCommand]
        private void MoveRight() => ShapeX += Step;

        [RelayCommand]
        private async Task Verify()
        {
            bool isSuccess = Math.Abs(ShapeX) <= Tolerance && Math.Abs(ShapeY) <= Tolerance;

            if (isSuccess)
                await App.Current.MainPage.DisplayAlert("✅ Success", "You are human!", "OK");
            else
                await App.Current.MainPage.DisplayAlert("❌ Try Again", "Shape is not centered yet.", "OK");

            
            _tcs?.TrySetResult(isSuccess);
        }
    }
}
