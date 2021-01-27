using Newtonsoft.Json;

namespace Exadel.HEH.Backend.DataAccess.Extensions
{
    public static class ModelExtensions
    {
        public static T DeepClone<T>(this T obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}