using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PT_App.TESTEN;

namespace PT_App.Server
{
    public class RunClient
    {
        public SaveValues _saveValues = new SaveValues();
        private Socket client;

        public RunClient()
        {
            IPAddress ipAddress = IPAddress.Parse("172.20.10.2"); // rpi ip
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 2000); //same integers port som rpi kode
            client = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(ipEndPoint);
        }

        public async void Running(string cprNumber)
        {
            string startMessage = "start";
            byte[] startMessageBytes = Encoding.UTF8.GetBytes(startMessage);
            client.Send(startMessageBytes, SocketFlags.None);
            Console.WriteLine($"Asta sends string: {startMessage}");

          
                var buffer = new byte[1024];
                var received = client.Receive(buffer, SocketFlags.None);
                var respone = Encoding.UTF8.GetString(buffer, 0, received);

                Console.WriteLine($"Tim sender: {respone}");

                var dataParts = respone.Split('/');
                try
                {
                    // Parse FVC- og FEV1-værdier
                    double fvcValue = double.Parse(dataParts[0].Split(':')[1], CultureInfo.GetCultureInfo("da-DK"));
                    double fev1Value = double.Parse(dataParts[1].Split(':')[1], CultureInfo.GetCultureInfo("da-DK"));

                    Console.WriteLine($"Parsed FVC: {fvcValue}, FEV1: {fev1Value}");

                    double ratio = fev1Value / fvcValue;

                    // Gem værdierne ved at kalde SaveLungFunctionValues
                    await _saveValues.Save(cprNumber, fev1Value, fvcValue, ratio);
              
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Fejl ved parsing af data: {ex.Message}. Venter på næste besked...");
                }
                Thread.Sleep(1000);
        }
    }
}
