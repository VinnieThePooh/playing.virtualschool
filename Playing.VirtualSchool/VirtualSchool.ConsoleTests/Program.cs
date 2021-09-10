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
			Console.ReadLine();
		}



		static void TestBroadcasting()
		{
			var listenerEndpoint = new IPEndPoint(IPAddress.Broadcast, NetworkDefaults.UdpBroadcastingPort);

			int outcomingPort = 2000;
			var udpClient = new UdpClient(outcomingPort);
			udpClient.EnableBroadcast = true;

			var timer = new System.Timers.Timer(1000);
			timer.Elapsed += (o, e) =>
			{
				var bytes = BitConverter.GetBytes((int) NetworkCommand.WhoIsIt);
				udpClient.Send(bytes, bytes.Length, listenerEndpoint);
				Console.WriteLine($"Sent command: {NetworkCommand.WhoIsIt}.");
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
