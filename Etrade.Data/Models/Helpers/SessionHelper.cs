using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Etrade.Data.Models.Helpers
{
    //Oturum  verilerini işlemk için yardımcı sınıf
    public static class SessionHelper
    {
        //Oturumdaki nesne sayısını takip eden özellik
        public static int Count { get; set; }
        //Bir nesneyi JSON formatında oturuma ekleyen genişletilmiş metot
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            //JSON formatına dönüştürülmüş nesneyi oturuma ekler
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        //Oturumda bir nesneyi JSON formatında almayı sağlayan genişletilmiş metot
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            //Belirtilen anahtar ile oturumda bir değer alır
            var value = session.GetString(key);

            //Eğer değer null ise varsayılan değer döndürülür
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
