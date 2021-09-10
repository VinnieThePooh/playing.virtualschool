using VirtualSchool.Common.Network;

namespace VirtualSchool.ClientApp.Network
{
	public class UdpClientModule: UdpBroadcastingModule
	{
		public UdpClientModule(int clientId, int listeningPort) : base(listeningPort)
		{
			ClientId = clientId;
		}

		public int ClientId { get; }
	}
}
