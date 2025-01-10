using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Net.Sockets;
using System.Net;

namespace WebApplication1.Controllers
{
    [Route("Prueba")]
    [ApiController]
    public class PruebaController : Controller
    {

        [HttpGet]
        [Route("ping")]
        public string PingHost(string host)
        {
            using (Ping ping = new Ping())
            {
                PingReply reply = ping.Send(host);
                if (reply.Status == IPStatus.Success)
                {
                    return $"Ping to {host} succeeded in {reply.RoundtripTime} ms.";
                }
                else
                {
                    return $"Ping to {host} failed. Error message: {reply.Status.ToString()}";
                }
            }
        }

        [HttpGet]
        [Route("ping2")]
        public string PingHost2(string host)
        {
            //string ipAddress = "192.168.1.1"; // dirección IP a la que se hará ping
            int pingCount = 5; // cantidad de veces que se hará ping

            List<long> pingTimes = new List<long>();
            Ping pingSender = new Ping();

            for (int i = 0; i < pingCount; i++)
            {
                PingReply reply = pingSender.Send(host);
                if (reply.Status == IPStatus.Success)
                {
                    pingTimes.Add(reply.RoundtripTime);
                }
            }

            if (pingTimes.Count > 0)
            {
                    return $"Ping to {host} succeeded in {pingTimes.Average()} ms.";
            }
            else
            {
                    return $"Ping to {host} failed. Error message: {pingTimes.Average()}";
            }
        }

        [HttpGet]
        [Route("ping3")]
        public string PingHost3(string host)
        {
            int port = 80; // puerto a utilizar

            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(host, port);
                    return $"Ping to {host} succeeded.";
            }
            catch (Exception ex)
            {
                    return $"Ping to {host} failed. Error message: {ex.Message}";
            }
            finally
            {
                tcpClient.Close();
            }
        }

        [HttpGet]
        [Route("ping4")]
        public string PingHost4(string host)
        {
            string ipAddress = "192.168.1.1"; // dirección IP a la que se hará ping

            WebClient client = new WebClient();
            try
            {
                byte[] response = client.DownloadData("http://" + host);
                return $"Ping to {host} succeeded.";
            }
            catch (WebException ex)
            {
                    return $"Ping to {host} failed. Error message: {ex.Message}";
            }
        }











    }
}
