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
        public DefaultJSonSettings(MvcJsonOptions mvcJSonOptions)
         : this(new JsonLoadSettings(), JsonSerializer.CreateDefault(mvcJSonOptions.SerializerSettings))
        {
        }

        public DefaultJSonSettings(JsonLoadSettings jSonLoadSettings, JsonSerializer jSonSerializer)
         : this(jSonSettings => { jSonSettings.LoadSettings = jSonLoadSettings; jSonSettings.Serializer = jSonSerializer; })
        {
        }

        public DefaultJSonSettings(Action<IJSonSettings> jSonSettings)
        {
            jSonSettings(this);
        }

        public JsonLoadSettings LoadSettings { get; set; }
        public JsonSerializer Serializer { get; set; }
    }
}