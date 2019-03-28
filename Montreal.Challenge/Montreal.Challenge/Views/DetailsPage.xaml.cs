using Montreal.Challenge.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Montreal.Challenge.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailsPage : BaseView
	{
		public DetailsPage ()
		{
			InitializeComponent ();
		}
	}
}