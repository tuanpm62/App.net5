using System.Threading.Tasks;
using App.Shared.MessagePackObjects;
using MagicOnion;
using UnityEngine;

namespace App.Shared.Hubs
{
    // Client -> Server definition
    // implements `IStreamingHub<TSelf, TReceiver>`  and share this type between server and client.
    public interface IGamingHub : IStreamingHub<IGamingHub, IGamingHubReceiver>
    {
        // return type shuold be `Task` or `Task<T>`, parameters are free.
        Task<Player[]> JoinAsync(string roomName, string userName, Vector3 position, Quaternion rotation);
        Task LeaveAsync();
        Task MoveAsync(Vector3 position, Quaternion rotation);
    }
}