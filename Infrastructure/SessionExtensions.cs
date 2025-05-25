using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace FabricFrame.Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetJson<T>(this ISession session, string key)
        {
            var jsonData = session.GetString(key);
            return jsonData == null ? default : JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
