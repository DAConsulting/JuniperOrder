using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JuniperOrder.Services
{
   public static class HttpService
   {

      // isolate this in case we need some special configuration...
      private static HttpClient Client
      {
         get
         {
            HttpClient result = new HttpClient()
            {
               MaxResponseContentBufferSize = 1024 * 1024 * 50
            };

            result.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
            result.DefaultRequestHeaders.Add("Keep-Alive", "3600");
            result.Timeout = TimeSpan.FromSeconds(10);
            return result;
         }
      }

      // 
      // Simple HTTP GET
      //
      public static async Task<string> Get(Uri uri, string key)
      {
         string result;
         HttpResponseMessage response = null;

         try
         {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);

            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", key);

            response = await Client.SendAsync(message);

            result = await response.Content.ReadAsStringAsync();
         }
         catch (Exception ex)
         {
            System.Diagnostics.Debug.WriteLine($"HTTP GET FAILED: {ex.Message}");
            throw;
         }

         return result;
      }

      //
      // HTTP POST with prepared content object...
      //
      public static async Task<HttpResponseMessage> Post(Uri uri, HttpContent content, string key)
      {
         HttpResponseMessage result = null;

         try
         {
            HttpClient cli = Client;

            cli.DefaultRequestHeaders.Authorization =
               new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", key);

            result = await cli.PostAsync(uri, content);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
         }
         catch (Exception ex)
         {
            System.Diagnostics.Debug.WriteLine($"HTTP POST FAILED: {ex.Message}");
            throw;
         }

         return result;
      }

      //
      // HTTP POST with JSON payload...
      //
      public static async Task<string> Post(Uri uri, string json, string key)
      {
         HttpResponseMessage response = null;
         string result = null;

         try
         {
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            response = await Post(uri,content,key);

            result = await response.Content.ReadAsStringAsync();
         }
         catch (Exception ex)
         {
            System.Diagnostics.Debug.WriteLine($"HTTP POST FAILED: {ex.Message}");
            throw;
         }

         return result;
      }


   }
}
