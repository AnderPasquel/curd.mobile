using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Org.Json;
namespace curd.mobile.Models
{
 
    public partial class Item
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

    public partial class Item
    {
        public Item() { }
        public static List<Item> FromJson(string request)
        {
            List<Item> products = new List<Item>();
            JSONArray jsonArray = new JSONArray(request);
            for (int i = 0; i < jsonArray.Length(); i++)
            {
                JSONObject explrObject = jsonArray.GetJSONObject(i);
                Item dto = new Item(explrObject);
                products.Add(dto);
            }
            return products;
        }
        public Item(JSONObject obj)
        {          
            Id = obj.GetLong("id");
            Name = obj.GetString("name");
            Price = obj.GetDouble("price");
            Creation = DateTime.Parse(obj.Get("creation").ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
            Modification = DateTime.Parse(obj.Get("modification").ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
        }
    } 

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
        {
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
        };
    }
    
}