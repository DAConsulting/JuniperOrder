using System;
using System.Collections.Generic;
using System.Text;

namespace JuniperOrder.Services
{
   public static class TaxCalcFactory
   {
      public static ITaxCalc GetCalculator(string theTaxCalcName = null)
      {
         ITaxCalc result = null;

         // simple idea here is we can retrieve a tax calculator
         // that uses the given "name" (i.e., from configuraiton) to get
         // the appropriate tax calculator and return its interface
         // a null or empty value could return a default.
         // for now... we just return a TaxJarTaxCalc

         //
         // look up, load or select the calc based on the name...
         //
         result = new TaxJarTaxCalc();

         return result;
      }
   }
}
