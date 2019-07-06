using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    public class DefaultJSonSettings : IJSonSettings
    {
        public DefaultJSonSettings(JsonLoadSettings jsonLoadSettings, JsonSerializer jsonSerializer)
        {

        }

        public DefaultJSonSettings(Action<IJSonSettings> jsonSettings)
        {
            jsonSettings(this);
        }

        public JsonLoadSettings LoadSettings { get; set; }
        public JsonSerializer Serializer { get; set; }
    }
}