using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.SignalR
{
    public class AppHub : Hub, IAppHub
    {
        //public const string HubName = "employeeListModificationHub";

        private readonly IHubContext<AppHub> _hubContext;

        public AppHub(IHubContext<AppHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        public void Initialize(string method)
        {
            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentException("message", nameof(method));
            }
        }

        public Task NotifyAll(string method, object[] args, CancellationToken cancellationToken)
        {
            args = args ?? new object[0];
            
            return _hubContext.Clients.All.SendCoreAsync(method, args, cancellationToken);
        }
    }
}