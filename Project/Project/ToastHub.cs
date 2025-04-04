﻿using Microsoft.AspNetCore.SignalR;

namespace Project {
	public class ToastHub : Hub {
		public async Task SendMessage(string username, string message) {
			await Clients.All.SendAsync("ReceiveMessage", username, message);
		}

		public override Task OnConnectedAsync() {
			Clients.All.SendAsync("ShowConnected");
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception? exception) {
			return base.OnDisconnectedAsync(exception);
		}
	}
}
