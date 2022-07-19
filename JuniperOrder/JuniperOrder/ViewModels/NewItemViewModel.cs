using JuniperOrder.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JuniperOrder.ViewModels
{
   public class NewItemViewModel : BaseViewModel
   {
      public NewItemViewModel()
      {
      }

      private bool Validate()
      {
         return !String.IsNullOrWhiteSpace(Text)
            && !String.IsNullOrWhiteSpace(Description)
            && !String.IsNullOrWhiteSpace(from_country)
            && !String.IsNullOrWhiteSpace(from_zip)
            && !String.IsNullOrWhiteSpace(from_state)
            && !String.IsNullOrWhiteSpace(from_city)
            && !String.IsNullOrWhiteSpace(from_street)
            && !String.IsNullOrWhiteSpace(to_country)
            && !String.IsNullOrWhiteSpace(to_zip)
            && !String.IsNullOrWhiteSpace(to_state)
            && !String.IsNullOrWhiteSpace(to_city)
            && !String.IsNullOrWhiteSpace(to_street)
            && amount > 0;
      }

      public string Text {  get; set;}
      public string Description {  get; set; }
      public string from_country {  get; set; }
      public string from_zip { get; set; }
      public string from_state { get; set; }
      public string from_city { get; set; }
      public string from_street { get; set; }
      public string to_country { get; set; }
      public string to_zip { get; set; }
      public string to_state { get; set; }
      public string to_city { get; set; }
      public string to_street { get; set; }
      public double amount { get; set; }
      public double shipping { get; set; }

      public Command SaveCommand { get; }
      public Command CancelCommand { get; }

      public async void OnCancel()
      {
         // This will pop the current page off the navigation stack
        
      }

      public async Task<bool> OnSave()
      {
         bool result = Validate();

         if ( result )
         { 
            order newItem = new order()
            {
               Id = Guid.NewGuid().ToString(),
               Text = this.Text,
               Description = this.Description,
               from_country = this.from_country,
               from_zip = this.from_zip,
               from_state = this.from_state,
               from_city = this.from_city,
               from_street = this.from_street,
               to_country = this.to_country,
               to_zip = this.to_zip,
               to_state = this.to_state,
               to_city = this.to_city,
               to_street = this.to_street,
               amount = this.amount,
               shipping = this.shipping
            };

            await DataStore.AddItemAsync(newItem);
         }

         return result;

      }
   }
}
