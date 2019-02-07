using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioLinQtoJSon
{
    class Program
    {
        static async void leeMuseosVisitas()
        {
            using (var Clientes = new HttpClient())
            {
                string url = @"https://museowebapp.azurewebsites.net/api/MuseosAPI";
                var uri = new Uri(url);

                var response = await Clientes.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JArray Array = JArray.Parse(content);
                    var museosVisitados = from x in Array
                                          where (string)x["visita"] == "S"
                                          select x;

                    foreach (JToken mus in museosVisitados)
                    {
                        Console.WriteLine(mus.ToString());
                    }
                }

            }
        }

        static void Main(string[] args)
        {
            //leeMuseos();
            leeMuseosVisitas();
            Console.ReadLine();
        }
    }
}
