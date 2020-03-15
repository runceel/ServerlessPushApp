using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ServerlessPushApp.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("何かキーを押すとサーバーに接続します。");
            Console.ReadKey();
            var hubConnection = new HubConnectionBuilder()
                .WithUrl(new Uri("https://freeokazuki.azurewebsites.net/api?code=ZmOaAaEWOcy0eoQ13J1hLBLGPyVPFNnFSUbxMYZCsxzN8xmeYeZsmw=="))
                .WithAutomaticReconnect()
                .Build();
            hubConnection.On<Data[]>("alert", x =>
            {
                Console.WriteLine("異常値がありました");
                foreach (var d in x)
                {
                    Console.WriteLine($"  {d.SensorId}: {d.Amount}");
                }
            });
            await hubConnection.StartAsync();
            Console.WriteLine("接続しました。何かキーを押すとプログラムが終了します。");
            Console.ReadKey();
        }
    }

    public class Data
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("sensorId")]
        public string SensorId { get; set; }
    }
}
