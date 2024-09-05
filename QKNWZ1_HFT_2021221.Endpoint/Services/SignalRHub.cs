using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace QKNWZ1_HFT_2021221.Endpoint
{
	public class SignalRHub : Hub
	{
		public override Task OnConnectedAsync()
		{
			this.Clients.Caller.SendAsync("Connected", this.Context.ConnectionId);
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			this.Clients.Caller.SendAsync("Disconnected", this.Context.ConnectionId);
			return base.OnDisconnectedAsync(exception);
		}
	}
}
