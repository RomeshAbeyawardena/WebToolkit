using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebToolkit.Contracts
{
    public interface IJSonSettings
    {
        JsonLoadSettings LoadSettings { get; }
        JsonSerializer Serializer { get; }
    }
}