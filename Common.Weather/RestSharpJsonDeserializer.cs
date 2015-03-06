
namespace Gamoya.Common.Weather
{
    internal class RestSharpJsonDeserializer : RestSharp.Deserializers.IDeserializer
    {
        string RestSharp.Deserializers.IDeserializer.DateFormat { get; set; }

        string RestSharp.Deserializers.IDeserializer.Namespace { get; set; }

        string RestSharp.Deserializers.IDeserializer.RootElement { get; set; }

        T RestSharp.Deserializers.IDeserializer.Deserialize<T>(RestSharp.IRestResponse response)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
