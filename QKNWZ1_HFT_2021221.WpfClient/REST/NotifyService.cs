using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace QKNWZ1_HFT_2021221.WpfClient
{
	internal class NotifyService
	{
		private readonly HubConnection conn;

		public NotifyService(string url)
		{
			this.conn = new HubConnectionBuilder()
				.WithUrl(url)
				.Build();

			this.conn.Closed += async (error) =>
			{
				await Task.Delay(new Random().Next(0, 5) * 1000);
				await this.conn.StartAsync();
			};
		}

		public void AddHandler<T>(string methodname, Action<T> value) => this.conn.On<T>(methodname, value);

		public async void Init() => await this.conn.StartAsync();

	}
}
