using App.Shared.MessagePackObjects;

namespace App.Shared.Hubs
{
    // Server -> Client definition
    public interface IGamingHubReceiver
    {
        // return type shuold be `void` or `Task`, parameters are free.
        void OnJoin(Player player);
        void OnLeave(Player player);
        void OnMove(Player player);
    }
}