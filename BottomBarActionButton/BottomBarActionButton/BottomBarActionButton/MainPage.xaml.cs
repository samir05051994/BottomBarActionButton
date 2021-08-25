using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BottomBarActionButton
{
    public partial class MainPage : ContentPage
    {
        public double ScreenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
        public MainPage()
        {
            InitializeComponent();
        }
        private async void TapGestureRecognizer1_Tapped(object sender, EventArgs e)
        {
            await ShowTab1();
        }
        private async void TapGestureRecognizer2_Tapped(object sender, EventArgs e)
        {
            await ShowTab2();
        }
        private void SwipeGestureRecognizer1_Swiped(object sender, SwipedEventArgs e)
        {
            Task.Run(async () => { await ShowTab2(); });
        }
        private void SwipeGestureRecognizer2_Swiped(object sender, SwipedEventArgs e)
        {
            Task.Run(async () => { await ShowTab1(); });
        }
        public async Task ShowTab1()
        {
            if (Tab1.IsVisible) return;
            Tab2.IsEnabled = false;
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                Tab1.IsVisible = true;
                Tab1.TranslationX = -ScreenWidth;
                await Task.WhenAll
                (
                    Tab1.TranslateTo(0, 0, 300, Easing.SinIn),
                    Tab2.TranslateTo(ScreenWidth, 0, 300, Easing.SinInOut)
                );
                Tab2.IsVisible = false;
            });
            Tab2.IsEnabled = true;
        }
        public async Task ShowTab2()
        {
            if (Tab2.IsVisible) return;
            Tab1.IsEnabled = false;
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                Tab2.IsVisible = true;
                Tab2.TranslationX = ScreenWidth;
                await Task.WhenAll
                (
                    Tab2.TranslateTo(0, 0, 300, Easing.SinIn),
                    Tab1.TranslateTo(-ScreenWidth, 0, 300, Easing.SinInOut)
                );
                Tab1.IsVisible = false;
            });
            Tab1.IsEnabled = true;
        }
    }
}
