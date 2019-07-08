using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebToolkit.Contracts
{
    public interface IJSonSettings
    {
        JsonLoadSettings LoadSettings { get; set; }
        JsonSerializer Serializer { get; set; }
    }
}