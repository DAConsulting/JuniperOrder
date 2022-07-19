using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JuniperOrder.Services
{
   /// <summary>
   /// Mimics a DB of some sort
   /// </summary>
   /// <typeparam name="T">the class of the object being stored</typeparam>
   public interface IDataStore<T>
   {
      Task<bool> AddItemAsync(T item);
      Task<bool> UpdateItemAsync(T item);
      Task<bool> DeleteItemAsync(string id);
      Task<T> GetItemAsync(string id);
      Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
   }
}
