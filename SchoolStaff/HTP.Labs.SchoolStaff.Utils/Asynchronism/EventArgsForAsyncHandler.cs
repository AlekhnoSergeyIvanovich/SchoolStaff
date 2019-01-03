using System;
using System.Threading;

namespace HTP.Labs.SchoolStaff.Utils.Asynchronism
{
    public class EventArgsForAsyncHandler : EventArgs
    {
        public static new readonly EventArgsForAsyncHandler Empty = new EventArgsForAsyncHandler();

        public CancellationToken CancellationToken { get; private set; }

        public EventArgsForAsyncHandler() : this(CancellationToken.None)
        {
        }

        public EventArgsForAsyncHandler(CancellationToken cancellationToken) => CancellationToken = cancellationToken;
    }
}
