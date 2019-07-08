using Newtonsoft.Json.Linq;
using WebToolkit.Contracts.Builders;

namespace WebToolkit.Common.Extensions
{
    public static class DictionaryBuilderExtensions
    {
        public static JObject ToJObject<TKey, TValue>(this IDictionaryBuilder<TKey, TValue> dictionaryBuilder)
        {
            var jObject = new JObject();
            foreach (var item in dictionaryBuilder)
            {
                jObject.Add(item.Key.ToString(), JToken.FromObject(item.Value));
            }

            return jObject;
        }
    }
}