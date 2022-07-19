using JuniperOrder.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using JuniperOrder.Services;

namespace JuniperOrder.ViewModels
{
   public class NewItemViewModel : BaseViewModel
   {
      public NewItemViewModel()
      {
         //
         // inject a default calculator into the tax service
         //
         taxService = new TaxService( TaxCalcFactory.GetCalculator("default") );

         //
         // add some line items so we don't have to create
         // an editor for this example...
         //
         line_items.Add( new line_item_view() {
            id = "Stuff A",
            quantity = 1,
            unit_price = 9.99,
            discount = 0,
            product_tax_code = "20010"
         });
         line_items.Add(new line_item_view()
         {
            id = "Stuff B",
            quantity = 1,
            unit_price = 17.59,
            discount = 0,
            product_tax_code = "20010"
         });
         line_items.Add(new line_item_view()
         {
            id = "Stuff C",
            quantity = 1,
            unit_price = 12.39,
            discount = 0,
            product_tax_code = "20010"
         });

         //
         // simple update for a sane display...
         //
         updateAmount();
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

      private string _text = "Fancy Project";
      public string Text { get => _text; set { SetProperty<string>(ref _text,value); } }

      private string _desc = "Cures what ailes you!";
      public string Description {  get => _desc; set { SetProperty<string>(ref _desc,value); } }

      private string _from_country = "US";
      public string from_country {  get => _from_country; set { SetProperty<string>(ref _from_country, value); } }

      private string _from_zip = "92093";
      public string from_zip { get => _from_zip; set { SetProperty<string>(ref _from_zip, value); } }

      private string _from_state = "CA";
      public string from_state { get => _from_state; set { SetProperty<string>(ref _from_state, value); } }

      private string _from_city = "La Jolla";
      public string from_city { get => _from_city; set { SetProperty<string>(ref _from_city, value); } }

      private string _from_street = "9500 Gilman Drive";
      public string from_street { get => _from_street; set { SetProperty<string>(ref _from_street, value); } }

      private string _to_country = "US";
      public string to_country { get => _to_country; set { SetProperty<string>(ref _to_country, value); } }

      private string _to_zip = "90002";
      public string to_zip { get => _to_zip; set { SetProperty<string>(ref _to_zip, value); } }

      private string _to_state = "CA";
      public string to_state { get => _to_state; set { SetProperty<string>(ref _to_state, value); } }

      private string _to_city = "Los Angeles";
      public string to_city { get => _to_city; set { SetProperty<string>(ref _to_city, value); } }

      private string _to_street = "1335 E 103rd St";
      public string to_street { get => _to_street; set { SetProperty<string>(ref _to_street, value); } }

      private double _amount = 0;
      public double amount { get => _amount; set { SetProperty<double>(ref _amount,value); } }

      private double _shipping = 0;
      public double shipping { get => _shipping; set { SetProperty<double>(ref _shipping, value); } }

      private double _tax = 0;
      public double tax {  get => _tax; set { SetProperty<double>(ref _tax,value); } }

      //
      // our line items...
      //
      private List<line_item_view> _line_items = new List<line_item_view>();
      public List<line_item_view> line_items => _line_items;

      //
      // instance of the tax service...
      //
      private TaxService taxService;

      private void updateAmount()
      {
         foreach( line_item_view v in _line_items )
         {
            amount += ((v.unit_price * v.quantity) * 1.00 - v.discount);
         }
      }

      public async Task<TaxCalcResult> CalcTax()
      {
         order aOrder = collectView();

         TaxCalcResult result  = await taxService.CalcTax(aOrder);

         tax = result.tax.amount_to_collect;
         amount = result.tax.order_total_amount;

         return result;
      }

      public async Task<TaxRates> GetRates()
      {
         return await taxService.GetRates(this.from_zip);
      }


      //
      // collect view into model
      //
      private order collectView()
      {
         order result = new order()
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

         result.line_items = new List<line_item>();

         foreach( line_item_view v in this.line_items )
         {
            result.line_items.Add( new line_item()
            {
               id = v.id,
               product_tax_code = v.product_tax_code,
               quantity = v.quantity,
               unit_price = v.unit_price,
               discount = v.discount
            });
         }

         return result;

      }

      //
      // validate, popuplate model and save...
      //
      public async Task<bool> Save()
      {
         bool result = Validate();

         if ( result )
         { 
            order newItem = collectView();
            await DataStore.AddItemAsync(newItem);
         }

         return result;

      }
   }
}
