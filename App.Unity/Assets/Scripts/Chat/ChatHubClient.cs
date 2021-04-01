using System;
using System.Threading;
using System.Threading.Tasks;
using App.Shared.Hubs;
using App.Shared.MessagePackObjects;
using Cysharp.Threading.Tasks;
using MagicOnion;
using MagicOnion.Client;
using MessagePipe;
using Microsoft.Extensions.Logging;

public class ChatHubClient : IChatHubReceiver
{
    static readonly ILogger<ChatHubClient> logger = LogManager.GetLogger<ChatHubClient>();
    readonly ChatComponent chatComponent;

    IChatHub client;
    private bool isJoin;
    IDisposable disposable;

    public ChatHubClient(ChatComponent chatComponent)
    {
        this.chatComponent = chatComponent;
    }

    public async Task ConnectAsync(GrpcChannelx grpcChannel, CancellationToken cancellationToken = default)
    {
        client = await StreamingHubClient.ConnectAsync<IChatHub, IChatHubReceiver>(grpcChannel, this, cancellationToken: cancellationToken);
        RegisterDisconnectEvent();

        var bag = DisposableBag.CreateBuilder(); // composite disposable for manage subscription

        GlobalMessagePipe.GetAsyncSubscriber<ChannelDispose>()
        .Subscribe(DisposeClient, cd => cd.channel.Equals(grpcChannel))
        .AddTo(bag);
        GlobalMessagePipe.GetAsyncSubscriber<ReInitServer>()
        .Subscribe(ReInitialize, cd => cd.channel.Equals(grpcChannel))
        .AddTo(bag);

        disposable = bag.Build();
    }

    // dispose client-connection before channel.ShutDownAsync is important!
    public Task DisposeAsync()
    {
        disposable.Dispose(); // unsubscribe event, all subscription **must** Dispose when completed
        return client.DisposeAsync();
    }

    // You can watch connection state, use this for retry etc.
    public Task WaitForDisconnect()
    {
        return client.WaitForDisconnect();
    }

    private async void RegisterDisconnectEvent()
    {
        try
        {
            // you can wait disconnected event
            await client.WaitForDisconnect();
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
        }
        finally
        {
            // try-to-reconnect? logging event? close? etc...
            logger.LogInformation($"disconnected server.");

            if (chatComponent.isSelfDisConnected)
            {
                // there is no particular meaning
                await Task.Delay(2000);

                // reconnect
                await chatComponent.ReconnectServerAsync();
            }
        }
    }

    private async UniTask DisposeClient(ChannelDispose message, CancellationToken cancellationToken)
    {
        // Clean up Hub
        if (client != null) await client.DisposeAsync();
    }

    private async UniTask ReInitialize(ReInitServer message, CancellationToken cancellationToken)
    {
        if (client != null)
        {
            var streamClient = client;
            if (streamClient == Interlocked.CompareExchange(ref client, null, streamClient))
            {
                await streamClient.DisposeAsync();
                client = null;
            }
        }

        if (client == null)
        {
            chatComponent.InitializeAsync();
        }
    }

    // methods send to server.

    #region Client -> Server (Streaming)
    public async Task JoinAsync()
    {
        if (isJoin) return;
        var request = new JoinRequest { RoomName = "SampleRoom", UserName = chatComponent.Input.text };
        await client.JoinAsync(request);

        isJoin = true;
        chatComponent.SetJoinedUi();
    }

    public async Task LeaveAsync()
    {
        if (!isJoin) return;
        await client.LeaveAsync();

        isJoin = false;
        chatComponent.InitializeUi();
    }

    public async void JoinOrLeave()
    {
        if (isJoin)
        {
            await LeaveAsync();
        }
        else
        {
            await JoinAsync();
        }
    }

    public async void SendMessage()
    {
        if (!isJoin) return;

        await client.SendMessageAsync(chatComponent.Input.text);

        chatComponent.Input.text = string.Empty;
    }

    public async void GenerateException()
    {
        // hub
        if (!isJoin) return;
        await client.GenerateException("client exception(streaminghub)!");
    }
    #endregion

    // Receivers of message from server.

    #region Server -> Client (Streaming)
    public void OnJoin(string name)
    {
        chatComponent.ChatText.text += $"\n<color=grey>{name} entered the room.</color>";
    }

    public void OnLeave(string name)
    {
        chatComponent.ChatText.text += $"\n<color=grey>{name} left the room.</color>";
    }

    public void OnSendMessage(MessageResponse message)
    {
        chatComponent.ChatText.text += $"\n{message.UserName}：{message.Message}";
    }
    #endregion
}