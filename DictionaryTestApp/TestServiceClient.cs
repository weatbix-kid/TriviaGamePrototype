using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DictionaryTestApp
{
    class TestServiceClient
    {
        internal async static Task<RootObject> GetRootObject(string prWord)
        {
            using (HttpClient lcHttpClient = new HttpClient()) {

                lcHttpClient.BaseAddress = new Uri("https://od-api.oxforddictionaries.com:443/api/v1/entries/en/");

                foreach (var item in APIKeys.KeyDetails)
                    lcHttpClient.DefaultRequestHeaders.Add(item.Key, item.Value);

                return JsonConvert.DeserializeObject<RootObject> (await lcHttpClient.GetStringAsync(
                    lcHttpClient.BaseAddress + prWord.ToLower()
                    ));
            }
        }
    }
}
