//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Net.Sockets;
//using System.Net.NetworkInformation;
//using System.Net;
//using PT_App.TESTEN;

//namespace PT_App.Server
//{
//    public class Socketserver
//    {
//        private const int Port = 5000; // portnummer, serveren lytter på

//        public async Task StartServerAsync()
//        {
//            var listener = new TcpListener(IPAddress.Any, Port);
//            listener.Start();
//            Console.WriteLine($"Server listening on port {Port}...");

//            while (true)
//            {
//                var client = await listener.AcceptTcpClientAsync(); // Accepter klient
//                _ = HandleClientAsync(client); // Håndter klient i baggrunden
//            }
//        }
//        private async Task HandleClientAsync(TcpClient client)
//        {
//            Console.WriteLine("Client connected...");
//            using var stream = client.GetStream();
//            var buffer = new byte[1024];
//            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length); // Læs data fra klient
//            var receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead); // Konverter til string

//            Console.WriteLine($"Data received: {receivedData}");

//            // Processer data
//            await ProcessReceivedData(receivedData);

//            client.Close();
//            Console.WriteLine("Client disconnected...");
//        }
//        private async Task ProcessReceivedData(string data)
//        {
//            try
//            {
//                // Split data baseret på komma (CSV-format)
//                var parts = data.Split(',');
//                if (parts.Length != 4)
//                {
//                    Console.WriteLine("Invalid data format. Expected: CPR,FEV1,FCV,Ratio");
//                    return;
//                }

//                var cprNumber = parts[0].Trim();
//                var fev1 = double.Parse(parts[1].Trim());
//                var fcv = double.Parse(parts[2].Trim());
//                var ratio = double.Parse(parts[3].Trim());

//                // Kald metoden for at gemme data i databasen
//                await SaveLungFunctionValues(cprNumber, fev1, fcv, ratio);

//                Console.WriteLine("Data saved to database.");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error processing data: {ex.Message}");
//            }
//        }
//        public async Task SaveLungFunctionValues(string cprNumber, double fev1ProcessValue, double fcvProcessValue, double ratio)
//        {
//            var maaling = new PatientMålinger()
//            {
//                CPR = cprNumber,
//                Dato = DateTime.Now.ToString(),
//                FCV = fcvProcessValue.ToString(),
//                FEV1 = fev1ProcessValue.ToString(),
//                Ratio = ratio.ToString()
//            };
//            await App.Database.UpdateMålingerAsync(maaling);

//        }
//    }
//}

