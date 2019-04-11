using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestApp
{

    class Program
    {
        public async static Task<string> asyncPostStreamAsync(string content, string url)
        {
            StringContent stringContent = new StringContent(content, UnicodeEncoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(
                url,
                stringContent);
            return response.Content.ReadAsStringAsync().Result;
        }

        public static List<List<object>> GetLocations(string url)
        {
            string payload = "{\"Command\": \"Get Locations\"}";
            string address = "http://" + url;
            string response = asyncPostStreamAsync(payload, address).Result;
            string json = JsonConvert.DeserializeObject<string>(response);
            List<List<object>> output = JsonConvert.DeserializeObject<List<List<object>>>(json);
            return output;
        }

        public static bool NewDepartment(List<List<object>> input, string url)
        {
            List<object> command = new List<object>();
            command.Add("{\"Command\": \"New Department\"}");
            input.Add(command);
            string payload  = JsonConvert.SerializeObject(input);
            string address = "http://" + url;
            string response = asyncPostStreamAsync(payload, address).Result;
            return true;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Sending Test Data!");
            string address = "IP Address Here";

            List<List<object>> test = new List<List<object>>();
            bool output = NewDepartment(test, address);
            Console.WriteLine(output);
            //List<List<object>> test = JsonConvert.DeserializeObject<List<List<object>>>(json);
            foreach (var item in test)
            {
                foreach (var items in item)
                {
                    Console.WriteLine(items);
                }
            }
            Console.ReadKey();
        }
    }
}
