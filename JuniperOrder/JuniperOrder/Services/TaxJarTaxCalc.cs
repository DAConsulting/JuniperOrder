using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JuniperOrder.Models;
using Newtonsoft.Json;

namespace JuniperOrder.Services
{
   //
   // TaxJar tax calculator exposing "generic" interface...
   //
   public class TaxJarTaxCalc : ITaxCalc
   {
      private string _calcTaxUrl = "https://api.taxjar.com/v2/taxes";
      private string _ratesUrl = "https://api.taxjar.com/v2/rates/";
      private string _key = "5da2f821eee4035db4771edab942a4cc";

      public async Task<TaxCalcResult> CalcTax(order theOrder)
      {
         TaxCalcResult tax = null;

         try
         {
            var uri = new Uri(string.Format(_calcTaxUrl, string.Empty));

            var json = JsonConvert.SerializeObject(theOrder);

            string result = await HttpService.Post(uri,json,_key);

            System.Diagnostics.Debug.WriteLine(result);

            tax = JsonConvert.DeserializeObject<TaxCalcResult>(result);
         }
         catch (Exception e)
         {
            System.Diagnostics.Debug.WriteLine(e.Message);// TODO
         }

         return tax;
      }

      public async Task<TaxRates> GetRates(string theZip)
      {
         TaxRates rates = null;

         try
         {
            var uri = new Uri(Path.Combine(_ratesUrl, theZip));

            string result = await HttpService.Get(uri, _key);

            System.Diagnostics.Debug.WriteLine(result);

            rates = JsonConvert.DeserializeObject<TaxRates>(result);
         }
         catch (Exception e)
         {
            System.Diagnostics.Debug.WriteLine(e.Message);// TODO
         }

         return rates;
      }
   }
}
