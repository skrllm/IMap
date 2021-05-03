using IMAP.Views;
using Xamarin.Forms;

namespace IMAP.View
{
    class WelcomeScreeen : ContentPage
    {
        private Image SplashImage;

        public WelcomeScreeen()
        {
            NavigationPage.SetHasNavigationBar(this,false);

            var sub = new AbsoluteLayout();
            SplashImage = new Image
            {
                Source = "AppLogo.png",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            AbsoluteLayout.SetLayoutFlags(SplashImage,AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(SplashImage,new Rectangle(0.5,0.5,AbsoluteLayout.AutoSize,AbsoluteLayout.AutoSize));

            sub.Children.Add(SplashImage);
            
            this.BackgroundColor = Color.White;
            this.Content = sub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await SplashImage.ScaleTo(1, 2000);
            await SplashImage.ScaleTo(0.9, 1500, Easing.Linear);
            await SplashImage.ScaleTo(150, 1200, Easing.Linear);

            Application.Current.MainPage = new MainPage();
        }
    }
}
