using System;
using System.Net.Http;
using System.Text;

namespace HexClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CreateNewGame();

        }

        static void CreateNewGame()
        {
            string myJson = "{'Username': 'myusername','Password':'pass'}";
            //var client = new HttpClient();
            //var response = client.GetAsync("https://www.uol.com.br");
            //"https://localhost:44309/api/game");            
            using (var client = new HttpClient())
            {
                var response = client.PostAsync("https://localhost:44309/api/game", new StringContent("")).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;

                    Console.WriteLine(responseString);
                }
            }

        }

        static void GetGame()
        {
            //string myJson = "{'Username': 'myusername','Password':'pass'}";
            //var client = new HttpClient();
            //var response = client.GetAsync("https://www.uol.com.br");
            //"https://localhost:44309/api/game");            
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("https://localhost:44309/api/game").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;

                    Console.WriteLine(responseString);
                }
            }

        }
    }
}
