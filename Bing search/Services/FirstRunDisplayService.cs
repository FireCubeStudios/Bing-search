using System;
using System.Threading.Tasks;

using Bing_search.Views;

using Microsoft.Toolkit.Uwp.Helpers;

namespace Bing_search.Services
{
    public static class FirstRunDisplayService
    {
        private static bool shown = false;

        internal static async Task ShowIfAppropriateAsync()
        {
            if (SystemInformation.IsFirstRun && !shown)
            {
                shown = true;
                var dialog = new FirstRunDialog();
                await dialog.ShowAsync();
            }
        }
    }
}
