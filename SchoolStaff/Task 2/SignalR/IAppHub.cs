using System.Threading;
using System.Threading.Tasks;

namespace Presentation.SignalR
{
    public interface IAppHub
    {
        void Initialize(string method);
        Task NotifyAll(string method, object[] args, CancellationToken cancellationToken);
    }
}