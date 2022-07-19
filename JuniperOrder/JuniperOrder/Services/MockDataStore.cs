using JuniperOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuniperOrder.Services
{
   public class MockDataStore : IDataStore<order>
   {
      readonly List<order> items;

      public MockDataStore()
      {
         items = new List<order>()
            {
                new order { Id = Guid.NewGuid().ToString(), Text = "Order one", Description="Description for order one." },
                new order { Id = Guid.NewGuid().ToString(), Text = "Order two", Description="Description for order two." },
                new order { Id = Guid.NewGuid().ToString(), Text = "Order three", Description="Description for order three." }
            };
      }

      public async Task<bool> AddItemAsync(order item)
      {
         items.Add(item);

         return await Task.FromResult(true);
      }

      public async Task<bool> UpdateItemAsync(order item)
      {
         var oldItem = items.Where((order arg) => arg.Id == item.Id).FirstOrDefault();
         items.Remove(oldItem);
         items.Add(item);

         return await Task.FromResult(true);
      }

      public async Task<bool> DeleteItemAsync(string id)
      {
         var oldItem = items.Where((order arg) => arg.Id == id).FirstOrDefault();
         items.Remove(oldItem);

         return await Task.FromResult(true);
      }

      public async Task<order> GetItemAsync(string id)
      {
         return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
      }

      public async Task<IEnumerable<order>> GetItemsAsync(bool forceRefresh = false)
      {
         return await Task.FromResult(items);
      }
   }
}