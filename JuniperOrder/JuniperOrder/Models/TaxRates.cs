using System;
using System.Collections.Generic;
using System.Text;

namespace JuniperOrder.Models
{
   //
   // Tax rates result classes.  These follow the model returned by TaxJar
   // for a real implementation, we would want to have a nuetral 
   // non-service specific result class... but for now... ;-)
   //
   public class Rate
   {
      public object city { get; set; }
      public string city_rate { get; set; }
      public string combined_district_rate { get; set; }
      public string combined_rate { get; set; }
      public string country { get; set; }
      public string country_rate { get; set; }
      public string county { get; set; }
      public string county_rate { get; set; }
      public bool freight_taxable { get; set; }
      public string state { get; set; }
      public string state_rate { get; set; }
      public string zip { get; set; }
   }

   public class TaxRates
   {
      public Rate rate { get; set; }
   }

}
