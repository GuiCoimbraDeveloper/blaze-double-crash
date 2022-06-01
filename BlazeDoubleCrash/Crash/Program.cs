using Crash.Entity;
using System;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;

namespace Crash
{
    class Program
    {
        private static readonly string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ODQ1NzgwNiwiYmxvY2tzIjpbXSwiaWF0IjoxNjUwMDUwMjkwLCJleHAiOjE2NTUyMzQyOTB9.x5tzGNmryxckvvOMvnEwKsw0r1R4us7w6NZRSiv-MA0";
        private static readonly int timeoutSendingAliveSocket = 5000;
        private static readonly string api = "wss://api-v2.blaze.com/replication/?EIO=3&transport=websocket";

        static void Main(string[] args)
        {
            try
            {
                situacao=0;
                // db= new Database();

                var uws = new Uri(api);

                var exitEvent = new ManualResetEvent(false);
                var factory = new Func<ClientWebSocket>(() => new ClientWebSocket
                {
                    Options = {
                            KeepAliveInterval = TimeSpan.FromSeconds(5),
                    }
                });

                using var client = new WebsocketClient(uws, factory);

                client.ReconnectTimeout = TimeSpan.FromSeconds(15);
                client.ErrorReconnectTimeout= TimeSpan.FromSeconds(15);

                client.ReconnectionHappened.Subscribe(info =>
                {
                    Console.WriteLine($"Reconnection happened, type: {info.Type}, url: {client.Url}");
                    client.Start().Wait();
                });
                client.DisconnectionHappened.Subscribe(info =>
                   Console.WriteLine($"Disconnection happened, type: {info.Type} exceção: {info.Exception?.Message}"));


                client.MessageReceived.Subscribe(async msg => await OnReceived(msg));
                client.Start().Wait();

                _= new Timer(_ => client.Send("2"), null, 0, timeoutSendingAliveSocket);

                OnOpen(client);
                exitEvent.WaitOne();
            }
            catch (Exception er)
            {
                Console.WriteLine("ERROR: " + er.Message);
            }
            finally
            {
                //db.FecharConexao();
            }
        }

        static int situacao = 0;
        protected static async Task OnReceived(ResponseMessage msg)
        {

            if (msg.Text.Contains(@"""id"":"))
            {
                var aux2 = msg.Text.Split(@"[""data"",")[1];
                var aux = aux2.Substring(0, aux2.Length-1);
                var json = JsonSerializer.Deserialize<ReceivedMessage>(aux);

                switch (json.Id)
                {
                    case "crash.bet":
                        Console.WriteLine("bet");
                        //Console.WriteLine(json.ToString());
                        break;
                    case "crash.update":

                        if (json.Payload.status.Equals("waiting"))
                        {
                            Console.WriteLine("update waiting");
                        }
                        if (json.Payload.status.Equals("graphing"))
                        {
                            Console.WriteLine("update graphing");
                        }
                        if (json.Payload.status.Equals("complete"))
                        {
                            Console.WriteLine("update complete");
                        }
                        break;
                    case "crash.tick":
                        if (json.Payload.crash_point == null)
                        {
                            if (json.Payload.status.Equals("graphing") && situacao ==0)
                            {
                                Console.WriteLine("tick crash point graphing");
                                Console.WriteLine(json.ToString());
                                situacao =1;
                            }
                            if (!json.Payload.status.Equals("graphing") && situacao ==1)
                            {
                                Console.WriteLine("tick crash point waiting");
                                Console.WriteLine(json.ToString());
                                situacao =2;
                            }
                        }

                        if (json.Payload.crash_point != null && situacao ==2)
                        {
                            Console.WriteLine("tick crash point complete");
                            Console.WriteLine(json.ToString());
                            situacao =0;
                        }
                        break;
                    default:
                        Console.WriteLine("Tratar Condição: "+json.ToString());
                        break;
                }
            }
        }

        protected static void OnOpen(WebsocketClient client)
        {
            client.Send("2");
            client.Send(@"423[""cmd"",{""id"":""subscribe"",""payload"":{""room"":""crash_v2""}}]");
            client.Send(@"423[""cmd"",{""id"":""authenticate"",""payload"":{""token"":"""+token+@"""}}]");
            client.Send(@"422[""cmd"",{""id"":""authenticate"",""payload"":{""token"":"""+token+@"""}}]");
            client.Send(@"420[""cmd"",{""id"":""authenticate"",""payload"":{""token"":"""+token+@"""}}]");
        }
    }
}
