using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace QKNWZ1_HFT_2021221.JavascriptClient
{
	public class Program
	{
		public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
	}
}
