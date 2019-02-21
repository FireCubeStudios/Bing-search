using Bing_search.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Bing_search.UserControls
{
    public sealed partial class WebControl : UserControl
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                if (value)
                {
                    IsShowingFailedMessage = false;
                }

                Set(ref _isLoading, value);
                IsLoadingVisibility = value ? Visibility.Visible : Visibility.Collapsed;
                if (IsLoadingVisibility == Visibility.Visible)
                {
                    FindName("Loading");
                }
                else
                {
                    UnloadObject(Loading);
                }
            }
        }

        private Visibility _isLoadingVisibility;

        public Visibility IsLoadingVisibility
        {
            get { return _isLoadingVisibility; }
            set { Set(ref _isLoadingVisibility, value); }
        }

        private bool _isShowingFailedMessage;
        public bool IsBackEnabled
        {
            get { return webView.CanGoBack; }
        }

        public bool IsForwardEnabled
        {
            get { return webView.CanGoForward; }
        }
        public bool IsShowingFailedMessage
        {
            get
            {
                return _isShowingFailedMessage;
            }

            set
            {
                if (value)
                {
                    IsLoading = false;
                }

                Set(ref _isShowingFailedMessage, value);
                FailedMesageVisibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Visibility FailedMesageVisibility
        {
            get { return _failedMesageVisibility; }
            set { Set(ref _failedMesageVisibility, value); }
        }

        public object ThemeResource { get; private set; }

        private Visibility _failedMesageVisibility;
        public WebControl()
        {
            this.InitializeComponent();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

            FindName("webView");

            //this.Frame.Navigate(typeof(SearchPage));
            try
            {
                webView.Source = new Uri(args.QueryText);
                IsLoading = true;
                FindName("Loading");
                SearchBox.Text = args.QueryText.ToString();
                IsLoading = true;
                FindName("Re");
                Refresh.IsEnabled = true;
                Back.IsEnabled = true;
                FindName("CopyUrl");
                FindName("DefaultBrowser");
                FindName("Favorites");
                FindName("Fullscreen");
            }
            catch
            {
                try
                {

                    webView.Source = new Uri("http://" + args.QueryText);
                    IsLoading = true;
                    FindName("Loading");
                    SearchBox.Text = args.QueryText.ToString();
                    IsLoading = true;
                    FindName("Re");
                    Refresh.IsEnabled = true;
                    Back.IsEnabled = true;
                    FindName("CopyUrl");
                    FindName("DefaultBrowser");
                    FindName("Favorites");
                    FindName("Fullscreen");

                }
                catch
                {




                    // Use args.QueryText to determine what to do.
                    //this.Frame.Navigate(typeof(SearchPage));
                    Uri Args = new Uri("https://www.bing.com/search?q=" + args.QueryText);
                    webView.Source = new Uri(Args.ToString());
                    IsLoading = true;
                    FindName("Re");
                    Refresh.IsEnabled = true;
                    Back.IsEnabled = true;
                    FindName("CopyUrl");
                    FindName("DefaultBrowser");
                    FindName("Favorites");
                    FindName("Fullscreen");

                    // Use args.QueryText to determine what to do.

                }
            }

        }


        private void OnNewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs e)
        {

            SearchBox.Text = e.Uri.ToString();
            WebView web = sender;
            web.Navigate(e.Uri);
            IsLoading = true;
            e.Handled = true;

        }
        private void OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            SearchBox.Text = webView.Source.ToString();
            IsLoading = false;
            OnPropertyChanged(nameof(IsBackEnabled));
            OnPropertyChanged(nameof(IsForwardEnabled));
        }

        private void OnNavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            // Use `e.WebErrorStatus` to vary the displayed message based on the error reason
            IsShowingFailedMessage = true;
            IsLoading = false;
        }






        private async void DefaultBrowser_Click(object sender, RoutedEventArgs e)
        {
            var success = await Launcher.LaunchUriAsync(webView.Source);
            if (success)
            { }
            else
            { }

        }

        private void CopyUrl_Click(object sender, RoutedEventArgs e)
        {
            var CopyDataPackage = new DataPackage();
            CopyDataPackage.SetText(webView.Source.ToString());
            Clipboard.SetContent(CopyDataPackage);
        }

        private async void Feedback_Click(object sender, RoutedEventArgs e)
        {
            var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
            await launcher.LaunchAsync();
        }

        private void Fullscreen_Click(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode)
            {
                view.ExitFullScreenMode();
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
                SearchBar.Visibility = Visibility.Visible;
                // The SizeChanged event will be raised when the exit from full-screen mode is complete.
            }
            else
            {
                if (view.TryEnterFullScreenMode())
                {


                    ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
                    SearchBar.Visibility = Visibility.Collapsed;
                    // The SizeChanged event will be raised when the entry to full-screen mode is complete.
                }
            }

        }

        private void ShareItem_Click(object sender, RoutedEventArgs e)
        {
            ShowUIButton_Click(sender, e);
        }
        private DataTransferManager dataTransferManager;



        /* protected override void OnNavigatedTo(NavigationEventArgs e)

         {

             // Register the current page as a share source.

             this.dataTransferManager = DataTransferManager.GetForCurrentView();

             this.dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.OnDataRequested);



             // Request to be notified when the user chooses a share target app.

             this.dataTransferManager.TargetApplicationChosen += OnTargetApplicationChosen;

         }



         protected override void OnNavigatedFrom(NavigationEventArgs e)

         {

             // Unregister our event handlers.

             this.dataTransferManager.DataRequested -= new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.OnDataRequested);

             this.dataTransferManager.TargetApplicationChosen -= OnTargetApplicationChosen;

         }*/



        // When share is invoked (by the user or programatically) the event handler we registered will be called to populate the datapackage with the

        // data to be shared.
        private bool GetShareContent(DataRequest request)

        {

            bool succeeded = false;

            DataPackage requestData = request.Data;

            requestData.Properties.Title = "Shared Link";

            requestData.Properties.Description = "Check out this link"; // The description is optional.

            requestData.Properties.ContentSourceApplicationLink = ApplicationLink;
            requestData.SetWebLink(webView.Source);
            succeeded = true;


            // It's recommended to use both SetBitmap and SetStorageItems for sharing a single image

            // since the target app may only support one or the other.





            /* else

             {

                 request.FailWithDisplayText("Something went wrong please try again.");

             }*/

            return succeeded;

        }
        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs e)

        {

            // Register to be notified if the share operation completes.

            e.Request.Data.ShareCompleted += OnShareCompleted;



            // Call the scenario specific function to populate the datapackage with the data to be shared.

            if (GetShareContent(e.Request))

            {

                // Out of the datapackage properties, the title is required. If the scenario completed successfully, we need

                // to make sure the title is valid since the sample scenario gets the title from the user.

                if (String.IsNullOrEmpty(e.Request.Data.Properties.Title))

                {

                    e.Request.FailWithDisplayText("Something wrong happened!");

                }

            }

        }



        private void OnTargetApplicationChosen(DataTransferManager sender, TargetApplicationChosenEventArgs e)

        {

            // this.NotifyUser($"User chose {e.ApplicationName}", NotifyType.StatusMessage);

        }



        private void OnShareCompleted(DataPackage sender, ShareCompletedEventArgs e)

        {

            string shareCompletedStatus = "Shared successfully. ";



            // Typically, this information is not displayed to the user because the

            // user already knows which share target was selected.

            if (!String.IsNullOrEmpty(e.ShareTarget.AppUserModelId))

            {

                // The user picked an app.

                shareCompletedStatus += $"Target: App \"{e.ShareTarget.AppUserModelId}\"";

            }

            else if (e.ShareTarget.ShareProvider != null)

            {

                // The user picked a ShareProvider.

                shareCompletedStatus += $"Target: Share Provider \"{e.ShareTarget.ShareProvider.Title}\"";

            }



            //share completed

        }
        // Assume webView is defined in XAML
        // webView.ContainsFullScreenElementChanged += webView_ContainsFullScreenElementChanged;

        private void webView_ContainsFullScreenElementChanged(WebView sender, object args)
        {
            var applicationView = ApplicationView.GetForCurrentView();

            if (sender.ContainsFullScreenElement)
            {
                applicationView.TryEnterFullScreenMode();
            }
            else if (applicationView.IsFullScreenMode)
            {
                applicationView.ExitFullScreenMode();
            }
        }


        private void ShowUIButton_Click(object sender, RoutedEventArgs e)

        {

            // If the user clicks the share button, invoke the share flow programatically.
            // Shareconfirm.Hide();
            DataTransferManager.ShowShareUI();

        }



        // This function is implemented by each scenario to share the content specific to that scenario (text, link, image, etc.).

        //protected abstract bool GetShareContent(DataRequest request);



        private Uri ApplicationLink

        {

            get

            {

                return GetApplicationLink(GetType().Name);

            }

        }



        public static Uri GetApplicationLink(string sharePageName)

        {

            return new Uri("ms-sdk-sharesourcecs:navigate?page=" + sharePageName);

        }


        private void webView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            IsLoading = true;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (webView.CanGoBack == true)
            {
                Forward.IsEnabled = true;
                webView.GoBack();
            }
            else
            {
                UnloadObject(webView);
                Back.IsEnabled = false;
            }

        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            if (webView.CanGoForward == true)
            {
                webView.GoForward();
                if (webView.CanGoForward == false)
                {
                    Forward.IsEnabled = false;
                }
            }
            else
            {
                UnloadObject(webView);
                Forward.IsEnabled = false;
            }

        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (webView.Source != null)
            {
                webView.Refresh();
                //webview.source.host
            }
            else
            {
                UnloadObject(webView);
                Refresh.IsEnabled = false;
            }
        }




    }
    public partial class WebControl : BingPage
    {

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            FindName("SettingsPanel");
        }
        private void MyFancyPanel_BackdropTapped(object sender, EventArgs e)

        {
            var Setting = FindName("SettingsPanel") as DependencyObject;
            UnloadObject(Setting);


        }

    }
}
