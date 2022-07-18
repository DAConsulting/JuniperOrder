using JuniperOrder.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace JuniperOrder.Views
{
   public partial class ItemDetailPage : ContentPage
   {
      public ItemDetailPage()
      {
         InitializeComponent();
         BindingContext = new ItemDetailViewModel();
      }
   }
}