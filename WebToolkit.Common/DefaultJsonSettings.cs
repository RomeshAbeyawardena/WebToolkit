using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    public class DefaultJSonSettings : IJSonSettings
    {
        public DefaultJSonSettings(MvcJsonOptions mvcJsonOptions)
         : this(new JsonLoadSettings(), JsonSerializer.CreateDefault(mvcJsonOptions.SerializerSettings))
        {
        }

        public DefaultJSonSettings(JsonLoadSettings jsonLoadSettings, JsonSerializer jsonSerializer)
         : this(jSonSettings => { jSonSettings.LoadSettings = jsonLoadSettings; jSonSettings.Serializer = jsonSerializer; })
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