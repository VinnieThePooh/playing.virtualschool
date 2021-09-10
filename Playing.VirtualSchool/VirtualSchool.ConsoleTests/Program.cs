using System;
using System.Net;
using System.Net.Sockets;
using VirtualSchool.Common.Models;
using VirtualSchool.Common.Utilities;

namespace VirtualSchool.ConsoleTests
{
	class Program
	{
		static void Main(string[] args)
		{
			TestBroadcasting();
		}



		static void TestBroadcasting()
		{
			var listenerEndpoint = new IPEndPoint(IPAddress.Broadcast, NetworkDefaults.UdpBroadcastingPort);

			var udpClient = new UdpClient(listenerEndpoint);
			udpClient.EnableBroadcast = true;

			var timer = new System.Timers.Timer(500);
			timer.Elapsed += (o, e) =>
			{
				var bytes = BitConverter.GetBytes((int) NetworkCommand.WhoIsIt);
				udpClient.Send(bytes, bytes.Length, listenerEndpoint);
				Console.Write($"Sent command: {NetworkCommand.WhoIsIt}.");
			};

			Console.WriteLine( "Press enter to begin broadcasting");
			Console.ReadLine();

			timer.Start();

			Console.WriteLine("Started broadcasting");


			Console.WriteLine("Press enter to stop broadcasting");
			Console.ReadLine();
			timer.Stop();
			Console.WriteLine("Stopped broadcasting");
		}
	}
}
