using System.Threading;
using Grpc.Core;
using MagicOnion;
using MagicOnion.Unity;
using MessagePipe;
using Microsoft.Extensions.Logging;

public class ChannelManager : Singleton<ChannelManager>
{
    static readonly ILogger<ChannelManager> logger = LogManager.GetLogger<ChannelManager>();

    private GrpcChannelx channel;
    public GrpcChannelx grpcChannel => channel;

    protected override void Awake()
    {
        base.Awake();
        InitializeChannel();
    }

    protected override async void OnDestroy()
    {
        // Clean up Hub and channel
        var p = GlobalMessagePipe.GetAsyncPublisher<ChannelDispose>();
        await p.PublishAsync(new ChannelDispose(channel));

        if (channel != null) await channel.DisposeAsync();

        base.OnDestroy();
    }

    public void InitializeChannel()
    {
        // Initialize the channel
        channel = GrpcChannelx.ForTarget(new GrpcChannelTarget("localhost", 5000, ChannelCredentials.Insecure));
    }

    public async void ReconnectInitializedServerAsync()
    {
        var p = GlobalMessagePipe.GetAsyncPublisher<ReInitServer>();
        await p.PublishAsync(new ReInitServer(channel));

        if (channel != null)
        {
            var chan = channel;
            if (chan == Interlocked.CompareExchange(ref channel, null, chan))
            {
                await chan.DisposeAsync();
                channel = null;
            }
        }

        if (channel == null)
        {
            InitializeChannel();
        }
    }
}