using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TestWeb_Api.Controllers;

namespace Check
{
    class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string checkUrl = "https://www.mypokupashkin.ru/Reg/check_signin";
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var json = JsonConvert.SerializeObject(new AuthModel()
            {
                Name = "TOTPAREN",
                Password = "111",
                PhoneNumber = "89093516222"
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine(json);
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync(checkUrl, content);
                string result = response.StatusCode.ToString();
                Console.WriteLine("Result:" + result.Replace("  ", ""));
            }
        }
    }
}
