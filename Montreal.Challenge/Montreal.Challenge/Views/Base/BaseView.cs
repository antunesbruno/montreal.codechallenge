using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Montreal.Challenge.Views.Base
{
    /// <summary>
    /// Base class for any view
    /// </summary>
    public class BaseView : ContentPage
    {
        public BaseView()
        {
            // hides the navigation bar
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
