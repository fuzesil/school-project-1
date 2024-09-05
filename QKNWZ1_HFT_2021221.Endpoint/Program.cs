using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

[assembly: System.CLSCompliant(false)]
namespace QKNWZ1_HFT_2021221.Endpoint
{
	/// <summary>
	/// The class that holds <see cref="Endpoint"/>'s entry of execution.
	/// </summary>
	public static class Program
	{
		/// <summary>
		/// The entry point of execution in <see cref="Program"/>.
		/// </summary>
		/// <param name="args">Input parameters taken from the arguments this method is called with.</param>
		public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

		/// <summary>
		/// Creates a configured web host.
		/// </summary>
		/// <param name="args"></param>
		/// <returns>The builder of the configured web host.</returns>
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
	}
}
