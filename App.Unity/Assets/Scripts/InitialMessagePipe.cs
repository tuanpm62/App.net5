using MagicOnion;
using MessagePipe;
using UnityEngine;

class InitialMessagePipe
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void RegisterMessagePipe()
    {
        var builder = new BuiltinContainerBuilder();

        builder.AddMessagePipe(/* configure option */);

        builder.AddMessageBroker<ChannelDispose>();
        builder.AddMessageBroker<ReInitServer>();

        // create provider and set to Global(to enable diagnostics window and global fucntion)
        var provider = builder.BuildServiceProvider();
        GlobalMessagePipe.SetProvider(provider);
    }
}

public class MessageChannel
{
    public GrpcChannelx channel;

    public MessageChannel(GrpcChannelx channel)
    {
        this.channel = channel;
    }
}

public class ChannelDispose : MessageChannel
{
    public ChannelDispose(GrpcChannelx channel) : base(channel) { }
}

public class ReInitServer : MessageChannel
{
    public ReInitServer(GrpcChannelx channel) : base(channel) { }
}