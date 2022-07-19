using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JuniperOrder.Models;

namespace JuniperOrder.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ShowRates : ContentPage
   {
      public ShowRates(TaxRates theRates)
      {
         InitializeComponent();

         BindingContext = Rates = theRates;
      }

      public TaxRates Rates { set;get;}

      private void QuitClicked(object sender, EventArgs e)
      {
         Shell.Current.GoToAsync("..");
      }
   }
}