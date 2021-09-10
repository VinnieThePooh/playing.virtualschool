using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace VirtualSchool.Common.Network
{
	public class UdpBroadcastingModule: IDisposable
	{
		private readonly UdpClient _client;
		private CancellationTokenSource _cts;
		private IPEndPoint _listeningEndPoint;

		public event EventHandler<RawDataEventArgs> DataReceived;

		public UdpBroadcastingModule(int listeningPort)
		{
			_client = new UdpClient(listeningPort);
			_listeningEndPoint = new IPEndPoint(IPAddress.Any, listeningPort);
			_client.EnableBroadcast = true;
		}

		public bool IsListening => _cts != null;

		public Task StartListening()
		{
			if (_cts != null)
				 throw new InvalidOperationException("Udp client already listening");

			_cts = new CancellationTokenSource();

			return  Task.Run(() => Listen(_cts.Token), _cts.Token);
		}

		public Task StopListening()
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
			while (!token.IsCancellationRequested)
			{
				var bytes = _client.Receive(ref _listeningEndPoint);
				OnDataReceived(bytes);
			}
		}

		protected virtual void OnDataReceived(byte[] bytes)
		{
			DataReceived?.Invoke(this, new RawDataEventArgs(bytes));
		}

		#endregion
	}
}
