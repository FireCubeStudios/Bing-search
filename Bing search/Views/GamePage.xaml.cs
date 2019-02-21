using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Bing_search.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        public GamePage()
        {
            this.InitializeComponent();
        }
        private void OnNewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs e)
        {

        
                GameView.Navigate(e.Uri);

                e.Handled = true;
            
          

        }

        private void chessButton_Click(object sender, RoutedEventArgs e)
        {
            FindName("GameView");
            UnloadObject(pivot);
            GameView.Source = new Uri("https://www.bing.com/fun/g/chess");
        }
        private void twozerofoureightButton_Click(object sender, RoutedEventArgs e)
        {
            FindName("GameView");
            FindName("HomeView");
            UnloadObject(pivot);
            GameView.Source = new Uri("https://www.bing.com/fun/g/2048");
        }
        private void jigsawButton_Click(object sender, RoutedEventArgs e)
        {
            FindName("GameView");
            FindName("HomeView");
            UnloadObject(pivot);
            GameView.Source = new Uri("https://www.bing.com/fun/g/jigsaw");
        }
        private void sudoku_Click(object sender, RoutedEventArgs e)
        {
            FindName("GameView");
            FindName("HomeView");
            UnloadObject(pivot);
            GameView.Source = new Uri("https://www.bing.com/fun/g/sudoku");
        }
        private void slidingTiles_Click(object sender, RoutedEventArgs e)
        {
            FindName("GameView");
                FindName("HomeView");
            UnloadObject(pivot);
            GameView.Source = new Uri("https://www.bing.com/fun/g/slidingtiles");
        }
        private void matchingCards_Click(object sender, RoutedEventArgs e)
        {
            FindName("GameView");
            FindName("HomeView");
            UnloadObject(pivot);
            GameView.Source = new Uri("https://www.bing.com/fun/g/matchingcards");
        }
        private void crosswordButton_Click(object sender, RoutedEventArgs e)
        {
            FindName("GameView");
            FindName("HomeView");
            UnloadObject(pivot);
            GameView.Source = new Uri("https://www.bing.com/fun/g/crossword");
        }
        private void nqButton_Click(object sender, RoutedEventArgs e)
        {
            FindName("GameView");
            FindName("HomeView");
            UnloadObject(pivot);
            GameView.Source = new Uri("https://www.bing.com/fun/g/quiz?type=newsquiz");
        }
        private void pioButton_Click(object sender, RoutedEventArgs e)
        {
            FindName("GameView");
            FindName("HomeView");
            UnloadObject(pivot);
            GameView.Source = new Uri("https://www.bing.com/fun/g/quiz?type=putinorderquiz");
        }
        private void SurpriseButton_Click(object sender, RoutedEventArgs e)
        {
            FindName("GameView");
            FindName("HomeView");
            UnloadObject(pivot);
            GameView.Source = new Uri("https://www.bing.com/fun/g/quiz?type=rewardsquiz");
        }
        private void celebquiz_Click(object sender, RoutedEventArgs e)
        {
            FindName("GameView");
            FindName("HomeView");
            UnloadObject(pivot);
            GameView.Source = new Uri("https://www.bing.com/fun/g/quiz?type=celebrityquiz");
        }
        private void geo_Click(object sender, RoutedEventArgs e)
        {
            FindName("GameView");
            FindName("HomeView");
            UnloadObject(pivot);
            GameView.Source = new Uri("https://www.bing.com/fun/g/quiz?type=geographyquiz");
        }
        private void HomePageQuiz_Click(object sender, RoutedEventArgs e)
        {
            FindName("GameView");
            FindName("HomeView");
            UnloadObject(pivot);
            GameView.Source = new Uri("https://www.bing.com/fun/g/quiz?type=homepagequiz");
        }

        private void GameView_NavigationStart(object sender, RoutedEventArgs e)
        {

            UnloadObject(GameView);
            UnloadObject(HomeView);
            FindName("pivot");
       
        }
    }
}
