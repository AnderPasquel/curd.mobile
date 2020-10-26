using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Org.Json;

namespace curd.mobile.Models
{
    public partial class Product
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("creation")]
        public DateTime Creation { get; set; }

        [JsonProperty("modification")]
        public DateTime Modification { get; set; }
    }

    public partial class Product
    {
        public static List<Product> FromJson(string request)
        {
            List<Product> products = new List<Product>();
            JSONArray jsonArray = new JSONArray(request);           
            for (int i = 0; i < jsonArray.Length(); i++)
            {
                JSONObject explrObject = jsonArray.GetJSONObject(i);
                Product dto = new Product(explrObject);
                products.Add(dto);
            }
            return products;
        }
        public Product(JSONObject obj) 
        {
            Id = obj.GetLong("id");
            Name = obj.GetString("name");
            Price = obj.GetDouble("price");
            Creation = DateTime.ParseExact(obj.GetString("creation"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Modification = DateTime.ParseExact(obj.GetString("modification"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
    }

 
}
