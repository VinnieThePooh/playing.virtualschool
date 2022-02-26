using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Configuration;
using VirtualSchool.Common.Network;
using VirtualSchool.Common.Utilities;
using VirtualSchool.Common.Extensions;

namespace VirtualSchool.BroadcastListener
{
	class Program
	{
		static void Main(string[] args)
		{
			//var options = new ConfigurationBuilder()
			//	.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
			//	.AddJsonFile("appsettings.json")
			//	.Build();

			//var portStr = options.GetSection("Network:Udp:ListeningPort").Value;
			//var port = int.Parse(portStr);

			var port = NetworkDefaults.UdpBroadcastingPort;

			var listener = new UdpBroadcastingModule(port);
			listener.DataReceived += (o, e) =>
			{
				//todo: проверять на передачу данных в пределах одного сетевого стека
				var command = e.Bytes.GetCommand(true);
				Console.WriteLine($"Received command: {command}");
			};

			var task = listener.StartListening();
			Console.ReadLine();
		}
	}
}
