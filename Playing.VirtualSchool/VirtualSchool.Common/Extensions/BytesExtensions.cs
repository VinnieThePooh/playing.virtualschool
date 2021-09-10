using System;
using System.Linq;
using VirtualSchool.Common.Models;

namespace VirtualSchool.Common.Extensions
{
	public static class BytesExtensions
	{
		public static NetworkCommand GetCommand(this byte[] bytes, bool inBigEndian = true)
		{
			if (bytes.Length != 4)
				return NetworkCommand.Unknown;

			var enumValues = Enum.GetValues<NetworkCommand>().OfType<int>();

			int integer;
			if (!inBigEndian)
				integer = BitConverter.ToInt32(bytes);
			else
				integer = bytes[0] << 24 | bytes[1] << 16 | bytes[2] << 8 | bytes[3];

			if (enumValues.Contains(integer))
				return (NetworkCommand) integer;

			return NetworkCommand.Unknown;
		}
	}
}
