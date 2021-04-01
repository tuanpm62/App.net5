using System.Threading.Tasks;
using UnityEngine;

public class GamingComponent : MonoBehaviour
{
    GamingHubClient gamingHubClient;
    GameObject player;
    [SerializeField] Vector3 position = Vector3.one;
    [SerializeField] Quaternion rotation = Quaternion.identity;

    async Task Start()
    {
        gamingHubClient = new GamingHubClient();
        player = await gamingHubClient.ConnectAsync(ChannelManager.Instance.grpcChannel, "gameRoom", "testPlayer");
    }

    void Update()
    {
        if (player != null)
        {
            gamingHubClient.MoveAsync(position, rotation);
        }
    }

    void OnDestroy()
    {
        gamingHubClient.LeaveAsync();
        gamingHubClient.DisposeAsync();
    }
}
