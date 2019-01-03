using System.Threading.Tasks;

namespace HTP.Labs.SchoolStaff.Utils.Asynchronism
{
    public delegate Task EventHandlerAsync(object sender, EventArgsForAsyncHandler e);

    public delegate Task EventHandlerAsync<in TEventArgs>(object sender, TEventArgs e) where TEventArgs : EventArgsForAsyncHandler;
}
