using Grpc.Core;
using MagicOnion.Unity;
using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;

class InitialSettings
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void RegisterResolvers()
    {
        // NOTE: Currently, CompositeResolver doesn't work on Unity IL2CPP build. Use StaticCompositeResolver instead of it.
        StaticCompositeResolver.Instance.Register(
            MagicOnion.Resolvers.MagicOnionResolver.Instance,
            MessagePack.Resolvers.GeneratedResolver.Instance,
            MessagePack.Unity.Extension.UnityBlitResolver.Instance,
            MessagePack.Unity.UnityResolver.Instance,
            BuiltinResolver.Instance,
            PrimitiveObjectResolver.Instance
        );

        MessagePackSerializer.DefaultOptions = MessagePackSerializer.DefaultOptions
            .WithResolver(StaticCompositeResolver.Instance);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnRuntimeInitialize()
    {
        // If you want configure KEEP_ALIVE interval, then....
        // * set same value for `grpc.keepalive_time_ms` and `grpc.http2.min_time_between_pings_ms`
        // * keep `grpc.http2.min_ping_interval_without_data_ms < grpc.http2.min_time_between_pings_ms`
        // Initialize gRPC channel provider when the application is loaded.
        GrpcChannelProviderHost.Initialize(new DefaultGrpcChannelProvider(new[]
        {
                // send keepalive ping every 10 second, default is 2 hours
                new ChannelOption("grpc.keepalive_time_ms", 10000),
                // keepalive ping time out after 5 seconds, default is 20 seoncds
                new ChannelOption("grpc.keepalive_timeout_ms", 5000),
                // allow grpc pings from client every 10 seconds
                new ChannelOption("grpc.http2.min_time_between_pings_ms", 10000),
                // allow unlimited amount of keepalive pings without data
                new ChannelOption("grpc.http2.max_pings_without_data", 0),
                // allow keepalive pings when there's no gRPC calls
                new ChannelOption("grpc.keepalive_permit_without_calls", 1),
                // allow grpc pings from client without data every 5 seconds
                new ChannelOption("grpc.http2.min_ping_interval_without_data_ms", 5000),
            }));
    }
}