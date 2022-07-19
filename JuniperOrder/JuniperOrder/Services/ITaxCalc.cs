using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JuniperOrder.Models;

namespace JuniperOrder.Services
{
   //
   // Generic tax interface...
   //
   public interface ITaxCalc
   {
      Task<TaxCalcResult> CalcTax( order theOrder );
      Task<TaxRates> GetRates(string theZip);
   }
}
