namespace VirtualSchool.Common.Network
{
	public class RawDataEventArgs
	{
		public RawDataEventArgs(byte[] bytes)
		{
			Bytes = bytes;
		}

		public byte[] Bytes { get; }
	}
}
