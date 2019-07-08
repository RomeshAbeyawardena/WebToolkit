using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    public class DefaultJSonSettings : IJSonSettings
    {
        private DefaultJSonSettings(MvcJsonOptions mvcJSonOptions)
         : this(new JsonLoadSettings(), JsonSerializer.CreateDefault(mvcJSonOptions.SerializerSettings))
        {
        }

        private DefaultJSonSettings(JsonLoadSettings jSonLoadSettings, JsonSerializer jSonSerializer)
         : this(jSonSettings => { jSonSettings.LoadSettings = jSonLoadSettings; jSonSettings.Serializer = jSonSerializer; })
        {
        }

        private DefaultJSonSettings(Action<IJSonSettings> jSonSettings)
        {
            jSonSettings(this);
        }

        public static IJSonSettings Create(Action<IJSonSettings> jSonSettings)
        {
            return new DefaultJSonSettings(jSonSettings);
        }

        public static IJSonSettings Create(JsonLoadSettings jSonLoadSettings, JsonSerializer jSonSerializer)
        {
            return new DefaultJSonSettings(jSonLoadSettings, jSonSerializer);
        }

        
        public static IJSonSettings Create(MvcJsonOptions mvcJSonOptions)
        {
            return new DefaultJSonSettings(mvcJSonOptions);
        }

        public JsonLoadSettings LoadSettings { get; set; }
        public JsonSerializer Serializer { get; set; }
    }
}