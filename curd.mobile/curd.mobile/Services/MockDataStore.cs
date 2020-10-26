using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using curd.mobile.Models;
using curd.mobile.Models.DTO;
using Newtonsoft.Json;
using Refit;

namespace curd.mobile.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        private HttpClient client = new HttpClient();
        private static string URL = "http://samana-test-api.azurewebsites.net";

        IRequestApi request;

        readonly List<Item> items;

        public MockDataStore()
        {
            
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            ProductDTO dto = new ProductDTO(item);
            request = RestService.For<IRequestApi>(URL);
            var res = await request.SubmitPost(dto);
            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateItemAsync(Item item)
        {
            ProductDTO dto = new ProductDTO(item);
            request = RestService.For<IRequestApi>(URL);
            var res = await request.UpdatePost(item.Id, dto);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(long id)
        {
            string endpoit = "/v1/products/{1}";
            string route = string.Format(endpoit, URL,id);
            try
            {
                using (var client = new HttpClient { BaseAddress = new Uri(URL) })
                {
                    try
                    {
                        using (var response = client.DeleteAsync(route).Result)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                Console.WriteLine("Item Deleted");
                            }
                        }

                    }               
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(long id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {            
            string endpoit = "/v1/GetAll";
            var result = await client.GetAsync(String.Concat(URL,endpoit));
            var json = result.Content.ReadAsStringAsync().Result;
            List<Item> products = Item.FromJson(json);
            return await Task.FromResult(products);
        }
    }
}