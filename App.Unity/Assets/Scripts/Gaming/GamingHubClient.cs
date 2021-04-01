using System.Collections.Generic;
using System.Threading.Tasks;
using App.Shared.Hubs;
using App.Shared.MessagePackObjects;
using MagicOnion;
using MagicOnion.Client;
using UnityEngine;

public class GamingHubClient : IGamingHubReceiver
{
    Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    IGamingHub client;

    public async Task<GameObject> ConnectAsync(GrpcChannelx grpcChannel, string roomName, string playerName)
    {
        client = await StreamingHubClient.ConnectAsync<IGamingHub, IGamingHubReceiver>(grpcChannel, this);

        var roomPlayers = await client.JoinAsync(roomName, playerName, Vector3.zero, Quaternion.identity);
        foreach (var player in roomPlayers)
        {
            if (player.Name.Equals(playerName)) break;
            (this as IGamingHubReceiver).OnJoin(player);
        }

        return players[playerName];
    }

    // methods send to server.

    public Task LeaveAsync()
    {
        return client.LeaveAsync();
    }

    public Task MoveAsync(Vector3 position, Quaternion rotation)
    {
        return client.MoveAsync(position, rotation);
    }

    // dispose client-connection before channel.ShutDownAsync is important!
    public Task DisposeAsync()
    {
        return client.DisposeAsync();
    }

    // You can watch connection state, use this for retry etc.
    public Task WaitForDisconnect()
    {
        return client.WaitForDisconnect();
    }

    // Receivers of message from server.

    void IGamingHubReceiver.OnJoin(Player player)
    {
        Debug.Log("Join Player:" + player.Name);

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = player.Name;
        cube.transform.SetPositionAndRotation(player.Position, player.Rotation);
        players[player.Name] = cube;
    }

    void IGamingHubReceiver.OnLeave(Player player)
    {
        Debug.Log("Leave Player:" + player.Name);

        if (players.TryGetValue(player.Name, out var cube))
        {
            GameObject.Destroy(cube);
        }
    }

    void IGamingHubReceiver.OnMove(Player player)
    {
        Debug.Log("Move Player:" + player.Name);

        if (players.TryGetValue(player.Name, out var cube))
        {
            cube.transform.SetPositionAndRotation(player.Position, player.Rotation);
        }
    }
}