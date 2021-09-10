using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace VirtualSchool.ClientApp.Network
{
	class ClientNetworkModule: IDisposable
	{
		private readonly UdpClient _client;
		private CancellationTokenSource _cts;
		private IPEndPoint _listeningEndPoint;

		public ClientNetworkModule(int clientId, int listeningPort)
		{
			_client = new UdpClient(new IPEndPoint(IPAddress.Broadcast, listeningPort));
			_client.EnableBroadcast = true;
			ClientId = clientId;
		}

		public int ClientId { get; }

		public Task StartListening()
		{
			if (_cts != null)
				 throw new InvalidOperationException("Udp client already listening");

			_cts = new CancellationTokenSource();

			return  Task.Run(() => Listen(_cts.Token), _cts.Token);
		}

		public Task TaskStopListening()
		{
			_cts?.Cancel();
			_cts = null;
			return Task.CompletedTask;
		}


		public void Dispose()
		{
			_client?.Dispose();
		}


		#region Implementation details

		private void Listen(CancellationToken token)
		{
			Debug.WriteLine($"Client {ClientId} began listenning");

			while (!token.IsCancellationRequested)
			{
				var bytes = _client.Receive(ref _listeningEndPoint);

			}

			Debug.WriteLine($"Client {ClientId} stopped listenning");
		}

		#endregion
	}
}
