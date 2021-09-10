using System;
using Microsoft.Extensions.Configuration;

namespace VirtualSchool.ClientApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var options = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json")
				.Build();


			
		}
	}
}
