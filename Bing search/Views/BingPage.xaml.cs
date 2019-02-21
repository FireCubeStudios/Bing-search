using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Bing_search.Views
{
    public sealed partial class BingPage : Page, INotifyPropertyChanged
    {
        public static string Args = "https://www.bing.com/";
        private TabView _tabs;
        public FrameworkElement control;

        public SolidColorBrush GetSolidColorBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
           // byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
           // byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
           // byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(a, 12, 132, 132));
            return myBrush;
        }

        public int tabnumber = 1;

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
                else {
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
        public BingPage()
        {
            InitializeComponent();
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
          Container.Height = coreTitleBar.Height;
            Window.Current.SetTitleBar(Container);
        }

        public void OnXamlRendered(FrameworkElement control)

        {

          /*  _tabs = control.FindChildByName("Tabs") as TabView;

            if (_tabs != null)

            {

                _tabs.TabDraggedOutside += Tabs_TabDraggedOutside;

              //  _tabs.TabClosing += Tabs_TabClosing;

            }




            if (control.FindDescendantByName("AddTabButtonUpper") is Button btn)

            {

                btn.Click += AddUpperTabClick;

            }*/

        }
        private void Tabs_TabDraggedOutside(object sender, TabDraggedOutsideEventArgs e)

        {

            // The sample app let's you drag items from a static TabView with TabViewItem's pre-defined.

            // In the case of databound scenarios e.Item should be your data item, and e.Tab should always be the TabViewItem.

            var str = e.Item.ToString();



            if (e.Tab != null)

            {

                str = e.Tab.Header.ToString();

            }



           //TabViewNotification.Show("Tore Tab '" + str + "' Outside of TabView.", 2000);

        }
        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn == true)
                {
                    News.Visibility = Visibility.Visible;
                    Msn.Visibility = Visibility.Visible;
                }
                else
                {
                    News.Visibility = Visibility.Collapsed;
                    Msn.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void ToggleSwitch_Toggle(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn == true)
                {
                    /*Translator.Visibility = Visibility.Collapsed;
                    Maps.Visibility = Visibility.Collapsed;
                    Shopping.Visibility = Visibility.Collapsed;*/
                    Outlook.Visibility = Visibility.Collapsed;
                    if (News.Visibility == Visibility.Visible)
                    {
                        News.Visibility = Visibility.Collapsed;
                        Msn.Visibility = Visibility.Collapsed;

                    }
                    else
                    {
                        News.Visibility = Visibility.Visible;
                        Msn.Visibility = Visibility.Visible;

                    }

                }
                else
                {
                    /*Translator.Visibility = Visibility.Visible;
                    Maps.Visibility = Visibility.Visible;
                    Shopping.Visibility = Visibility.Visible;*/
                    Outlook.Visibility = Visibility.Visible;
                    if (News.Visibility == Visibility.Collapsed)
                    {
                        News.Visibility = Visibility.Collapsed;
                        Msn.Visibility = Visibility.Collapsed;

                    }
                    else
                    {
                        News.Visibility = Visibility.Visible;
                        Msn.Visibility = Visibility.Visible;

                    }

                }
            }
        }
        private void AddUpperTabClick(object sender, RoutedEventArgs e)

        {
            
            string hex = ("0C");
            byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            var fill = GetSolidColorBrush("#0C8484").Color;


            _tabs = FindName("Tabs") as TabView;

            Windows.UI.Xaml.Media.AcrylicBrush CodeBrush = new Windows.UI.Xaml.Media.AcrylicBrush();
            CodeBrush.BackgroundSource = Windows.UI.Xaml.Media.AcrylicBackgroundSource.HostBackdrop;
            CodeBrush.FallbackColor = Windows.UI.Color.FromArgb(a, 12, 132, 132);
            CodeBrush.TintColor = Windows.UI.Color.FromArgb(a, 12, 132, 132);
            CodeBrush.TintOpacity = 0.7f;

            /* AppBarButton backWeb = new AppBarButton();
             backWeb.Icon = new SymbolIcon(Symbol.Back);
             backWeb.LabelPosition = CommandBarLabelPosition.Collapsed;
             backWeb.IsEnabled = false;
             backWeb.Click += Back_Click;
             backWeb.Name = "back" + tabnumber;

             AppBarButton ForwardWeb = new AppBarButton();
             ForwardWeb.Icon = new SymbolIcon(Symbol.Forward);
             ForwardWeb.LabelPosition = CommandBarLabelPosition.Collapsed;
             ForwardWeb.IsEnabled = false;
             ForwardWeb.Click += Forward_Click;
             ForwardWeb.Name = "Forward" + tabnumber;

             AppBarButton RefreshWeb = new AppBarButton();
             RefreshWeb.Icon = new SymbolIcon(Symbol.Refresh);
             RefreshWeb.LabelPosition = CommandBarLabelPosition.Collapsed;
             RefreshWeb.IsEnabled = false;
             RefreshWeb.Click += Refresh_Click;
             RefreshWeb.Name = "Refresh" + tabnumber;

             CommandBar S = new CommandBar();
             S.OverflowButtonVisibility = CommandBarOverflowButtonVisibility.Collapsed;
             S.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(a, 12, 132, 132));
             S.Background = CodeBrush;
             S.PrimaryCommands.Add(backWeb);
             S.PrimaryCommands.Add(ForwardWeb);
             S.PrimaryCommands.Add(RefreshWeb);


             AutoSuggestBox WebSearcher = new AutoSuggestBox();
             WebSearcher.QueryIcon = new SymbolIcon(Symbol.Find);
             WebSearcher.QuerySubmitted += AutoSuggestBox_QuerySubmitted;
             WebSearcher.UseSystemFocusVisuals = false;
             WebSearcher.Width = 900f;
             WebSearcher.Margin = new Thickness(5);
             WebSearcher.HorizontalAlignment = HorizontalAlignment.Right;
             WebSearcher.Name = "SearchBox" + tabnumber;

             StackPanel commandsHub = new StackPanel();
             commandsHub.Orientation = Orientation.Horizontal;
             commandsHub.Children.Add(S);

             commandsHub.Children.Add(WebSearcher);

             AppBarButton ControlSetting = new AppBarButton();
             ControlSetting.LabelPosition = CommandBarLabelPosition.Collapsed;
             ControlSetting.Icon = new SymbolIcon(Symbol.Setting);
             ControlSetting.Click += AppBarButton_Click;

             AppBarButton CommandSetting = new AppBarButton();
             CommandSetting.Label = "Settings";
             CommandSetting.Icon = new SymbolIcon(Symbol.Setting);
             CommandSetting.Click += AppBarButton_Click;

             AppBarButton refresher = new AppBarButton();
             refresher.Label = "Refresh";
             refresher.Icon = new SymbolIcon(Symbol.Refresh);
             refresher.Click += Refresh_Click;
             refresher.Name = "Re" + tabnumber;
             refresher.Visibility = Visibility.Collapsed;

             AppBarButton CopyLink = new AppBarButton();
             refresher.Label = "Copy URL";
             refresher.Icon = new SymbolIcon(Symbol.Copy);
             refresher.Click += CopyUrl_Click;
             refresher.Name = "CopyUrl" + tabnumber;
             refresher.Visibility = Visibility.Collapsed;


             AppBarSeparator Seperate1 = new AppBarSeparator();

             AppBarButton Globe = new AppBarButton();
             refresher.Label = "Open in default browser";
             refresher.Icon = new SymbolIcon(Symbol.Globe);
             refresher.Click += DefaultBrowser_Click;
             refresher.Name = "DefauktBrowser" + tabnumber;
             refresher.Visibility = Visibility.Collapsed;

             AppBarButton FavAdd = new AppBarButton();
             refresher.Label = "Add to Home screen";
             refresher.Icon = new SymbolIcon(Symbol.Add);
             refresher.Click += Refresh_Click;
             refresher.Name = "Favorites" + tabnumber;
             refresher.Visibility = Visibility.Collapsed;

             AppBarButton FullViewMode = new AppBarButton();
             refresher.Label = "Fullscreen mode";
             refresher.Icon = new SymbolIcon(Symbol.FullScreen);
             refresher.Click += Fullscreen_Click;
             refresher.Name = "Fullscreen" + tabnumber;
             refresher.Visibility = Visibility.Collapsed;

             AppBarButton BookMarker = new AppBarButton();
            BookMarker.Label = "Save as bookmark";
             BookMarker.Icon = new SymbolIcon(Symbol.Bookmarks);


             AppBarButton WebShare = new AppBarButton();
             ControlSetting.Label = "Share";
             ControlSetting.Icon = new SymbolIcon(Symbol.Share);
             ControlSetting.Click += ShareItem_Click;

             AppBarButton WebFeedback = new AppBarButton();
             ControlSetting.Label = "Feedback";
             ControlSetting.Icon = new SymbolIcon(Symbol.Comment);
             ControlSetting.Click += Feedback_Click;



             AppBarSeparator Seperate = new AppBarSeparator();



             CommandBar c = new CommandBar();
             c.OverflowButtonVisibility = CommandBarOverflowButtonVisibility.Visible;
             c.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(a, 12, 132, 132));
             //c.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(a, 12, 132, 132));
             c.Background = CodeBrush;
             c.Content = commandsHub;
             c.PrimaryCommands.Add(ControlSetting);
             c.SecondaryCommands.Add(refresher);
             c.SecondaryCommands.Add(Seperate1);
             c.SecondaryCommands.Add(BookMarker);
             c.SecondaryCommands.Add(WebShare);
             c.SecondaryCommands.Add(WebFeedback);
             c.SecondaryCommands.Add(CommandSetting);
             c.SecondaryCommands.Add(Seperate);
             c.SecondaryCommands.Add(CopyLink);
             c.SecondaryCommands.Add(Globe);
             c.SecondaryCommands.Add(FavAdd);
             c.SecondaryCommands.Add(FullViewMode);


             //put buttons in correct order




             Grid SearchGrid = new Grid();
             SearchGrid.Children.Add(c);
             SearchGrid.Name = "SearchBar" + tabnumber;


             WebView we = new WebView();
             we.Name = "webView" + tabnumber;
             we.NavigationCompleted += OnNavigationCompleted;
             we.NavigationStarting += webView_NavigationStarting;
             we.NewWindowRequested += OnNewWindowRequested;
             we.NavigationFailed += OnNavigationFailed;
             we.ContainsFullScreenElementChanged += webView_ContainsFullScreenElementChanged;

             ProgressRing L = new ProgressRing();
             L.Name = "Loading" + tabnumber;
             L.HorizontalAlignment = HorizontalAlignment.Center;
             L.VerticalAlignment = VerticalAlignment.Center;
             L.Visibility = Visibility.Collapsed;
             L.IsActive = _isLoading;


             Grid Gr = new Grid();
             Gr.Children.Add(we);
             Gr.Children.Add(SearchGrid);
             Gr.Children.Add(L);
             Gr.Name = "Grid" + tabnumber;*/
            var F = new UserControls.WebControl();

            _tabs.Items.Add(new TabViewItem()

            {

                Header = "Search " + tabnumber++,
               Content = F,
                Icon = new SymbolIcon(Symbol.Find),
                Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(a, 12, 132, 132)),
               
               
                



                   



            });
       



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





        PivotItem pivot = null;
        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pivot = (PivotItem)(sender as Pivot).SelectedItem;
            switch (pivot.Header.ToString())
            {
                case "Images":
                    this.UnloadObject(video);
                    this.UnloadObject(NewS);
                    this.UnloadObject(MSN);
                    this.UnloadObject(outlook);
                    this.UnloadObject(CopyUrl);
                    this.UnloadObject(DefaultBrowser);
                    this.UnloadObject(Favorites);
                    this.UnloadObject(Fullscreen);
                    FindName("Item");
                    break;
                case "Videos":
                    this.UnloadObject(Item);
                    this.UnloadObject(NewS);
                    this.UnloadObject(MSN);
                    this.UnloadObject(outlook);
                    this.UnloadObject(CopyUrl);
                    this.UnloadObject(DefaultBrowser);
                    this.UnloadObject(Favorites);
                    this.UnloadObject(Fullscreen);
                    FindName("video");
                    break;
                case "News":
                    this.UnloadObject(video);
                    this.UnloadObject(Item);
                    this.UnloadObject(MSN);
                    this.UnloadObject(outlook);
                    this.UnloadObject(CopyUrl);
                    this.UnloadObject(DefaultBrowser);
                    this.UnloadObject(Favorites);
                    this.UnloadObject(Fullscreen);
                    FindName("NewS");
                    break;
                case "MSN":
                    this.UnloadObject(video);
                    this.UnloadObject(Item);
                    this.UnloadObject(MSN);
                    this.UnloadObject(NewS);
                    this.UnloadObject(CopyUrl);
                    this.UnloadObject(DefaultBrowser);
                    this.UnloadObject(Favorites);
                    this.UnloadObject(Fullscreen);
                    FindName("MSN");
                    break;
                case "Outlook":
                    this.UnloadObject(video);
                    this.UnloadObject(Item);
                    this.UnloadObject(MSN);
                    this.UnloadObject(NewS);
                    this.UnloadObject(CopyUrl);
                    this.UnloadObject(DefaultBrowser);
                    this.UnloadObject(Favorites);
                    this.UnloadObject(Fullscreen);
                    FindName("outlook");
                    break;
            }

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



        protected override void OnNavigatedTo(NavigationEventArgs e)

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

        }



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

        private void Expander_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            
                FindName("G");
        }
     
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fill = GetSolidColorBrush("#0C8484").Color;
            _tabs = FindName("Tabs") as TabView;
            _tabs.Items.Add(new TabViewItem()

            {

                Header = "Privacy",

                Icon = new SymbolIcon(Symbol.Find),
                Foreground = new SolidColorBrush(fill),
                Name = "PrivacyTab",








            });
            TabViewItem psdf = FindName("PrivacyTab") as TabViewItem;
            WebView privacy = new WebView()
            {
                Source = new Uri("https://privacy.microsoft.com/en-US/privacystatement"),

            };
            psdf.Content = privacy;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var fill = GetSolidColorBrush("#0C8484").Color;
            _tabs = FindName("Tabs") as TabView;
            _tabs.Items.Add(new TabViewItem()

            {

                Header = "Legal",

                Icon = new SymbolIcon(Symbol.Find),
                Foreground = new SolidColorBrush(fill),
                Name = "legalTab",








            });
            TabViewItem lsdf = FindName("legalTab") as TabViewItem;
            WebView Legal = new WebView()
            {
                Source = new Uri("https://www.microsoft.com/en-us/servicesagreement/"),

            };
            lsdf.Content = Legal;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var fill = GetSolidColorBrush("#0C8484").Color;
            _tabs = FindName("Tabs") as TabView;
            _tabs.Items.Add(new TabViewItem()

            {

                Header = "Help",

                Icon = new SymbolIcon(Symbol.Find),
                Foreground = new SolidColorBrush(fill),
                Name = "LegalTab",








            });
            TabViewItem lsdf = FindName("LegalTab") as TabViewItem;
            WebView legal = new WebView()
            {
                Source = new Uri("http://help.bing.microsoft.com/#apex/18/en-US/n1999/-1/en-US"),

            };
            lsdf.Content = legal;
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var fill = GetSolidColorBrush("#0C8484").Color;
            _tabs = FindName("Tabs") as TabView;
            _tabs.Items.Add(new TabViewItem()

            {

                Header = "History",

                Icon = new SymbolIcon(Symbol.Find),
                Foreground = new SolidColorBrush(fill),
                Name = "HistoryTab",








            });
            TabViewItem lSdf = FindName("HistoryTab") as TabViewItem;
            WebView legal = new WebView()
            {
                Source = new Uri("https://www.bing.com/profile/history"),

            };
            lSdf.Content = legal;
        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            var fill = GetSolidColorBrush("#0C8484").Color;
            _tabs = FindName("Tabs") as TabView;
            _tabs.Items.Add(new TabViewItem()

            {

                Header = "Setting",

                Icon = new SymbolIcon(Symbol.Find),
                Foreground = new SolidColorBrush(fill),
                Name = "SettingTab",








            });
            TabViewItem Sdf = FindName("SettingTab") as TabViewItem;
            WebView setting = new WebView()
            {
                Source = new Uri("https://www.bing.com/account/general"),

            };
            Sdf.Content = setting;
        }

        private void MenuFlyoutItem_Click_2(object sender, RoutedEventArgs e)
        {
            var fill = GetSolidColorBrush("#0C8484").Color;
            _tabs = FindName("Tabs") as TabView;
            _tabs.Items.Add(new TabViewItem()

            {

                Header = "Rewards",

                Icon = new SymbolIcon(Symbol.Find),
                Foreground = new SolidColorBrush(fill),
                Name = "RewardTab",








            });
            TabViewItem reward = FindName("RewardTab") as TabViewItem;
            WebView legal = new WebView()
            {
                Source = new Uri("https://account.microsoft.com/rewards/"),

            };
            reward.Content = legal;
        }
        private void MyFancyPanel_BackdropTapped(object sender, EventArgs e)

        {

            UnloadObject(SettingsPanel);

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            FindName("SettingsPanel");
        }
        // Add "using" for WinUI controls.
        // using muxc = Microsoft.UI.Xaml.Controls;

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
           // throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
{
    ("home", typeof(SettingsPage)),
    ("saves", typeof(saveView)),
    ("places", typeof(PlacePage)),
    ("games", typeof(GamePage)),
    ("rewards", typeof(RewardsPage)),
};

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            // You can also add items in code.
          /*  NavView.MenuItems.Add(new NavigationViewItemSeparator());
            NavView.MenuItems.Add(new NavigationViewItem
            {
                Content = "My content",
                Icon = new SymbolIcon((Symbol)0xF1AD),
                Tag = "content"
            });
            _pages.Add(("content", typeof(SettingsPage)));

            // Add handler for ContentFrame navigation.
            ContentFrame.Navigated += On_Navigated;

            // NavView doesn't load any page by default, so load home page.
            NavView.SelectedItem = NavView.MenuItems[0];
            // If navigation occurs on SelectionChanged, this isn't needed.
            // Because we use ItemInvoked to navigate, we need to call Navigate
            // here to load the home page.
            NavView_Navigate("home", new EntranceNavigationTransitionInfo());

            // Add keyboard accelerators for backwards navigation.
            var goBack = new KeyboardAccelerator { Key = VirtualKey.GoBack };
            goBack.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(goBack);*/

            // ALT routes here
            var altLeft = new KeyboardAccelerator
            {
                Key = VirtualKey.Left,
                Modifiers = VirtualKeyModifiers.Menu
            };
            altLeft.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(altLeft);
        }

        private void NavView_ItemInvoked(NavigationView sender,
                                         NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

/* NavView_SelectionChanged is not used in this example, but is shown for completeness.
     You will typically handle either ItemInvoked or SelectionChanged to perform navigation,
     but not both.*/
private void NavView_SelectionChanged(NavigationView sender,
                                      NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected == true)
            {
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.SelectedItemContainer != null)
            {
                var navItemTag = args.SelectedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavView_Navigate(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            Type _page = null;
            if (navItemTag == "settings")
            {
                _page = typeof(SettingsPage);
            }
            else
            {
                var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                _page = item.Page;
            }
            // Get the page type before navigation so you can prevent duplicate
            // entries in the backstack.
            var preNavPageType = ContentFrame.CurrentSourcePageType;

            // Only navigate if the selected page isn't currently loaded.
            if (!(_page is null) && !Type.Equals(preNavPageType, _page))
            {
                ContentFrame.Navigate(_page, null, transitionInfo);
            }
        }

        private void NavView_BackRequested(NavigationView sender,
                                           NavigationViewBackRequestedEventArgs args)
        {
            On_BackRequested();
        }

        private void BackInvoked(KeyboardAccelerator sender,
                                 KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }

        private bool On_BackRequested()
        {
            if (!ContentFrame.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed.
            if (NavView.IsPaneOpen &&
                (NavView.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NavView.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;

            ContentFrame.GoBack();
            return true;
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(SettingsPage))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
                NavView.SelectedItem = (NavigationViewItem)NavView.SettingsItem;
                NavView.Header = "Settings";
            }
            else if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);

                NavView.SelectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag));

                NavView.Header =
                    ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
            }
        }

        private void webView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
          IsLoading = true;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if(webView.CanGoBack == true)
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

        private void TabViewItem_Closing(object sender, TabClosingEventArgs e)
        {
            UnloadObject(MainSearch);
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            FindName("MainSearch");
        }
        string blob = "www.google.com";
       /* private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            blob = webView.Source.ToString();
            UnloadObject(webView);
        }
        private void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {

            FindName("webView");
            webView.Source = new Uri(blob);
        }*/
    }
}
