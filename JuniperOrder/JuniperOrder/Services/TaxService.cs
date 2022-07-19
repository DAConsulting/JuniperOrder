using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JuniperOrder.Models;

namespace JuniperOrder.Services 
{
   public class TaxService : ITaxCalc
   {
      private ITaxCalc _calc;

      //
      // Tax service accepting a generic tax calculation interface...
      //
      public TaxService(ITaxCalc theCalc)
      {
         _calc = theCalc;
      }

      //
      // calc tax, just delegating to the ITaxCalc interface...
      //
      public async Task<TaxCalcResult> CalcTax( order theOrder )
      {
         return await _calc.CalcTax( theOrder );
      }

      //
      // get rates for a zip code, delegating to the ITaxCalc interface...
      //
      public async Task<TaxRates> GetRates(string theZip)
      {
         return await _calc.GetRates(theZip);
      }
   }
}
