﻿using System;
using System.Collections.Generic;

namespace JuniperOrder.Models
{
   public class order
   {
      // window dressing...
      public string Id { get; set; }
      public string Text { get; set; }
      public string Description { get; set; }

      // order details...
      public string from_country { get; set; }
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
      public double tax {  get;set; }

      // nexus addresses
      public List<nexus_address> nexus_addresses => new List<nexus_address>();

      // line items
      private List<line_item> _line_items = new List<line_item>();
      public List<line_item> line_items
      {
         get => _line_items;
         set { _line_items = value; }
      }
   }
}