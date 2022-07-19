using JuniperOrder.Models;
using JuniperOrder.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JuniperOrder.Views
{
   public partial class NewItemPage : ContentPage
   {
      public order Item { get; set; }
      private NewItemViewModel _view = new NewItemViewModel();

      public NewItemPage()
      {
         InitializeComponent();
         BindingContext = _view;
      }

      private async void CancelClicked(object sender, EventArgs e)
      {
         await Shell.Current.GoToAsync("..");
      }

      private async void SaveClicked(object sender, EventArgs e)
      {
         if ( await _view.OnSave() )
         { 
            await Shell.Current.GoToAsync("..");
         }
         else
         {
            await DisplayAlert("WHOA!", "Unable to save order. Assure all information has been provided and try again", "OK");
         }
      }
   }
}