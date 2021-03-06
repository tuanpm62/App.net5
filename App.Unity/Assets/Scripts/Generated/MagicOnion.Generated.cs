// <auto-generated />
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace MagicOnion
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::MagicOnion;
    using global::MagicOnion.Client;

    public static partial class MagicOnionInitializer
    {
        static bool isRegistered = false;

        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Register()
        {
            if(isRegistered) return;
            isRegistered = true;

            MagicOnionClientRegistry<App.Shared.Services.IChatService>.Register((x, y, z) => new App.Shared.Services.ChatServiceClient(x, y, z));

            StreamingHubClientRegistry<App.Shared.Hubs.IChatHub, App.Shared.Hubs.IChatHubReceiver>.Register((a, _, b, c, d, e) => new App.Shared.Hubs.ChatHubClient(a, b, c, d, e));
            StreamingHubClientRegistry<App.Shared.Hubs.IGamingHub, App.Shared.Hubs.IGamingHubReceiver>.Register((a, _, b, c, d, e) => new App.Shared.Hubs.GamingHubClient(a, b, c, d, e));
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace MagicOnion.Resolvers
{
    using System;
    using MessagePack;

    public class MagicOnionResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new MagicOnionResolver();

        MagicOnionResolver()
        {

        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                var f = MagicOnionResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class MagicOnionResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static MagicOnionResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(6)
            {
                {typeof(global::App.Shared.MessagePackObjects.Player[]), 0 },
                {typeof(global::MagicOnion.DynamicArgumentTuple<global::System.Collections.Generic.List<int>, global::System.Collections.Generic.Dictionary<int, string>>), 1 },
                {typeof(global::MagicOnion.DynamicArgumentTuple<global::UnityEngine.Vector3, global::UnityEngine.Quaternion>), 2 },
                {typeof(global::MagicOnion.DynamicArgumentTuple<string, string, global::UnityEngine.Vector3, global::UnityEngine.Quaternion>), 3 },
                {typeof(global::System.Collections.Generic.Dictionary<int, string>), 4 },
                {typeof(global::System.Collections.Generic.List<int>), 5 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key))
            {
                return null;
            }

            switch (key)
            {
                case 0: return new global::MessagePack.Formatters.ArrayFormatter<global::App.Shared.MessagePackObjects.Player>();
                case 1: return new global::MagicOnion.DynamicArgumentTupleFormatter<global::System.Collections.Generic.List<int>, global::System.Collections.Generic.Dictionary<int, string>>(default(global::System.Collections.Generic.List<int>), default(global::System.Collections.Generic.Dictionary<int, string>));
                case 2: return new global::MagicOnion.DynamicArgumentTupleFormatter<global::UnityEngine.Vector3, global::UnityEngine.Quaternion>(default(global::UnityEngine.Vector3), default(global::UnityEngine.Quaternion));
                case 3: return new global::MagicOnion.DynamicArgumentTupleFormatter<string, string, global::UnityEngine.Vector3, global::UnityEngine.Quaternion>(default(string), default(string), default(global::UnityEngine.Vector3), default(global::UnityEngine.Quaternion));
                case 4: return new global::MessagePack.Formatters.DictionaryFormatter<int, string>();
                case 5: return new global::MessagePack.Formatters.ListFormatter<int>();
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace App.Shared.Services {
    using System;
    using MagicOnion;
    using MagicOnion.Client;
    using Grpc.Core;
    using MessagePack;

    [Ignore]
    public class ChatServiceClient : MagicOnionClientBase<global::App.Shared.Services.IChatService>, global::App.Shared.Services.IChatService
    {
        static readonly Method<byte[], byte[]> GenerateExceptionMethod;
        static readonly Func<RequestContext, ResponseContext> GenerateExceptionDelegate;
        static readonly Method<byte[], byte[]> SendReportAsyncMethod;
        static readonly Func<RequestContext, ResponseContext> SendReportAsyncDelegate;

        static ChatServiceClient()
        {
            GenerateExceptionMethod = new Method<byte[], byte[]>(MethodType.Unary, "IChatService", "GenerateException", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);
            GenerateExceptionDelegate = _GenerateException;
            SendReportAsyncMethod = new Method<byte[], byte[]>(MethodType.Unary, "IChatService", "SendReportAsync", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);
            SendReportAsyncDelegate = _SendReportAsync;
        }

        ChatServiceClient()
        {
        }

        public ChatServiceClient(CallInvoker callInvoker, MessagePackSerializerOptions serializerOptions, IClientFilter[] filters)
            : base(callInvoker, serializerOptions, filters)
        {
        }

        protected override MagicOnionClientBase<IChatService> Clone()
        {
            var clone = new ChatServiceClient();
            clone.host = this.host;
            clone.option = this.option;
            clone.callInvoker = this.callInvoker;
            clone.serializerOptions = this.serializerOptions;
            clone.filters = filters;
            return clone;
        }

        public new IChatService WithHeaders(Metadata headers)
        {
            return base.WithHeaders(headers);
        }

        public new IChatService WithCancellationToken(System.Threading.CancellationToken cancellationToken)
        {
            return base.WithCancellationToken(cancellationToken);
        }

        public new IChatService WithDeadline(System.DateTime deadline)
        {
            return base.WithDeadline(deadline);
        }

        public new IChatService WithHost(string host)
        {
            return base.WithHost(host);
        }

        public new IChatService WithOptions(CallOptions option)
        {
            return base.WithOptions(option);
        }
   
        static ResponseContext _GenerateException(RequestContext __context)
        {
            return CreateResponseContext<string, global::MessagePack.Nil>(__context, GenerateExceptionMethod);
        }

        public global::MagicOnion.UnaryResult<global::MessagePack.Nil> GenerateException(string message)
        {
            return InvokeAsync<string, global::MessagePack.Nil>("IChatService/GenerateException", message, GenerateExceptionDelegate);
        }
        static ResponseContext _SendReportAsync(RequestContext __context)
        {
            return CreateResponseContext<string, global::MessagePack.Nil>(__context, SendReportAsyncMethod);
        }

        public global::MagicOnion.UnaryResult<global::MessagePack.Nil> SendReportAsync(string message)
        {
            return InvokeAsync<string, global::MessagePack.Nil>("IChatService/SendReportAsync", message, SendReportAsyncDelegate);
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace App.Shared.Hubs {
    using Grpc.Core;
    using MagicOnion;
    using MagicOnion.Client;
    using MessagePack;
    using System;
    using System.Threading.Tasks;

    [Ignore]
    public class ChatHubClient : StreamingHubClientBase<global::App.Shared.Hubs.IChatHub, global::App.Shared.Hubs.IChatHubReceiver>, global::App.Shared.Hubs.IChatHub
    {
        static readonly Method<byte[], byte[]> method = new Method<byte[], byte[]>(MethodType.DuplexStreaming, "IChatHub", "Connect", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);

        protected override Method<byte[], byte[]> DuplexStreamingAsyncMethod { get { return method; } }

        readonly global::App.Shared.Hubs.IChatHub __fireAndForgetClient;

        public ChatHubClient(CallInvoker callInvoker, string host, CallOptions option, MessagePackSerializerOptions serializerOptions, IMagicOnionClientLogger logger)
            : base(callInvoker, host, option, serializerOptions, logger)
        {
            this.__fireAndForgetClient = new FireAndForgetClient(this);
        }
        
        public global::App.Shared.Hubs.IChatHub FireAndForget()
        {
            return __fireAndForgetClient;
        }

        protected override void OnBroadcastEvent(int methodId, ArraySegment<byte> data)
        {
            switch (methodId)
            {
                case -1297457280: // OnJoin
                {
                    var result = MessagePackSerializer.Deserialize<string>(data, serializerOptions);
                    receiver.OnJoin(result); break;
                }
                case 532410095: // OnLeave
                {
                    var result = MessagePackSerializer.Deserialize<string>(data, serializerOptions);
                    receiver.OnLeave(result); break;
                }
                case -552695459: // OnSendMessage
                {
                    var result = MessagePackSerializer.Deserialize<global::App.Shared.MessagePackObjects.MessageResponse>(data, serializerOptions);
                    receiver.OnSendMessage(result); break;
                }
                default:
                    break;
            }
        }

        protected override void OnResponseEvent(int methodId, object taskCompletionSource, ArraySegment<byte> data)
        {
            switch (methodId)
            {
                case -733403293: // JoinAsync
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case 1368362116: // LeaveAsync
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case -601690414: // SendMessageAsync
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case 517938971: // GenerateException
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case -852153394: // SampleMethod
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                default:
                    break;
            }
        }
   
        public global::System.Threading.Tasks.Task JoinAsync(global::App.Shared.MessagePackObjects.JoinRequest request)
        {
            return WriteMessageWithResponseAsync<global::App.Shared.MessagePackObjects.JoinRequest, Nil>(-733403293, request);
        }

        public global::System.Threading.Tasks.Task LeaveAsync()
        {
            return WriteMessageWithResponseAsync<Nil, Nil>(1368362116, Nil.Default);
        }

        public global::System.Threading.Tasks.Task SendMessageAsync(string message)
        {
            return WriteMessageWithResponseAsync<string, Nil>(-601690414, message);
        }

        public global::System.Threading.Tasks.Task GenerateException(string message)
        {
            return WriteMessageWithResponseAsync<string, Nil>(517938971, message);
        }

        public global::System.Threading.Tasks.Task SampleMethod(global::System.Collections.Generic.List<int> sampleList, global::System.Collections.Generic.Dictionary<int, string> sampleDictionary)
        {
            return WriteMessageWithResponseAsync<DynamicArgumentTuple<global::System.Collections.Generic.List<int>, global::System.Collections.Generic.Dictionary<int, string>>, Nil>(-852153394, new DynamicArgumentTuple<global::System.Collections.Generic.List<int>, global::System.Collections.Generic.Dictionary<int, string>>(sampleList, sampleDictionary));
        }


        class FireAndForgetClient : global::App.Shared.Hubs.IChatHub
        {
            readonly ChatHubClient __parent;

            public FireAndForgetClient(ChatHubClient parentClient)
            {
                this.__parent = parentClient;
            }

            public global::App.Shared.Hubs.IChatHub FireAndForget()
            {
                throw new NotSupportedException();
            }

            public Task DisposeAsync()
            {
                throw new NotSupportedException();
            }

            public Task WaitForDisconnect()
            {
                throw new NotSupportedException();
            }

            public global::System.Threading.Tasks.Task JoinAsync(global::App.Shared.MessagePackObjects.JoinRequest request)
            {
                return __parent.WriteMessageAsync<global::App.Shared.MessagePackObjects.JoinRequest>(-733403293, request);
            }

            public global::System.Threading.Tasks.Task LeaveAsync()
            {
                return __parent.WriteMessageAsync<Nil>(1368362116, Nil.Default);
            }

            public global::System.Threading.Tasks.Task SendMessageAsync(string message)
            {
                return __parent.WriteMessageAsync<string>(-601690414, message);
            }

            public global::System.Threading.Tasks.Task GenerateException(string message)
            {
                return __parent.WriteMessageAsync<string>(517938971, message);
            }

            public global::System.Threading.Tasks.Task SampleMethod(global::System.Collections.Generic.List<int> sampleList, global::System.Collections.Generic.Dictionary<int, string> sampleDictionary)
            {
                return __parent.WriteMessageAsync<DynamicArgumentTuple<global::System.Collections.Generic.List<int>, global::System.Collections.Generic.Dictionary<int, string>>>(-852153394, new DynamicArgumentTuple<global::System.Collections.Generic.List<int>, global::System.Collections.Generic.Dictionary<int, string>>(sampleList, sampleDictionary));
            }

        }
    }

    [Ignore]
    public class GamingHubClient : StreamingHubClientBase<global::App.Shared.Hubs.IGamingHub, global::App.Shared.Hubs.IGamingHubReceiver>, global::App.Shared.Hubs.IGamingHub
    {
        static readonly Method<byte[], byte[]> method = new Method<byte[], byte[]>(MethodType.DuplexStreaming, "IGamingHub", "Connect", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);

        protected override Method<byte[], byte[]> DuplexStreamingAsyncMethod { get { return method; } }

        readonly global::App.Shared.Hubs.IGamingHub __fireAndForgetClient;

        public GamingHubClient(CallInvoker callInvoker, string host, CallOptions option, MessagePackSerializerOptions serializerOptions, IMagicOnionClientLogger logger)
            : base(callInvoker, host, option, serializerOptions, logger)
        {
            this.__fireAndForgetClient = new FireAndForgetClient(this);
        }
        
        public global::App.Shared.Hubs.IGamingHub FireAndForget()
        {
            return __fireAndForgetClient;
        }

        protected override void OnBroadcastEvent(int methodId, ArraySegment<byte> data)
        {
            switch (methodId)
            {
                case -1297457280: // OnJoin
                {
                    var result = MessagePackSerializer.Deserialize<global::App.Shared.MessagePackObjects.Player>(data, serializerOptions);
                    receiver.OnJoin(result); break;
                }
                case 532410095: // OnLeave
                {
                    var result = MessagePackSerializer.Deserialize<global::App.Shared.MessagePackObjects.Player>(data, serializerOptions);
                    receiver.OnLeave(result); break;
                }
                case 1429874301: // OnMove
                {
                    var result = MessagePackSerializer.Deserialize<global::App.Shared.MessagePackObjects.Player>(data, serializerOptions);
                    receiver.OnMove(result); break;
                }
                default:
                    break;
            }
        }

        protected override void OnResponseEvent(int methodId, object taskCompletionSource, ArraySegment<byte> data)
        {
            switch (methodId)
            {
                case -733403293: // JoinAsync
                {
                    var result = MessagePackSerializer.Deserialize<global::App.Shared.MessagePackObjects.Player[]>(data, serializerOptions);
                    ((TaskCompletionSource<global::App.Shared.MessagePackObjects.Player[]>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case 1368362116: // LeaveAsync
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case -99261176: // MoveAsync
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                default:
                    break;
            }
        }
   
        public global::System.Threading.Tasks.Task<global::App.Shared.MessagePackObjects.Player[]> JoinAsync(string roomName, string userName, global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation)
        {
            return WriteMessageWithResponseAsync<DynamicArgumentTuple<string, string, global::UnityEngine.Vector3, global::UnityEngine.Quaternion>, global::App.Shared.MessagePackObjects.Player[]> (-733403293, new DynamicArgumentTuple<string, string, global::UnityEngine.Vector3, global::UnityEngine.Quaternion>(roomName, userName, position, rotation));
        }

        public global::System.Threading.Tasks.Task LeaveAsync()
        {
            return WriteMessageWithResponseAsync<Nil, Nil>(1368362116, Nil.Default);
        }

        public global::System.Threading.Tasks.Task MoveAsync(global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation)
        {
            return WriteMessageWithResponseAsync<DynamicArgumentTuple<global::UnityEngine.Vector3, global::UnityEngine.Quaternion>, Nil>(-99261176, new DynamicArgumentTuple<global::UnityEngine.Vector3, global::UnityEngine.Quaternion>(position, rotation));
        }


        class FireAndForgetClient : global::App.Shared.Hubs.IGamingHub
        {
            readonly GamingHubClient __parent;

            public FireAndForgetClient(GamingHubClient parentClient)
            {
                this.__parent = parentClient;
            }

            public global::App.Shared.Hubs.IGamingHub FireAndForget()
            {
                throw new NotSupportedException();
            }

            public Task DisposeAsync()
            {
                throw new NotSupportedException();
            }

            public Task WaitForDisconnect()
            {
                throw new NotSupportedException();
            }

            public global::System.Threading.Tasks.Task<global::App.Shared.MessagePackObjects.Player[]> JoinAsync(string roomName, string userName, global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation)
            {
                return __parent.WriteMessageAsyncFireAndForget<DynamicArgumentTuple<string, string, global::UnityEngine.Vector3, global::UnityEngine.Quaternion>, global::App.Shared.MessagePackObjects.Player[]> (-733403293, new DynamicArgumentTuple<string, string, global::UnityEngine.Vector3, global::UnityEngine.Quaternion>(roomName, userName, position, rotation));
            }

            public global::System.Threading.Tasks.Task LeaveAsync()
            {
                return __parent.WriteMessageAsync<Nil>(1368362116, Nil.Default);
            }

            public global::System.Threading.Tasks.Task MoveAsync(global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation)
            {
                return __parent.WriteMessageAsync<DynamicArgumentTuple<global::UnityEngine.Vector3, global::UnityEngine.Quaternion>>(-99261176, new DynamicArgumentTuple<global::UnityEngine.Vector3, global::UnityEngine.Quaternion>(position, rotation));
            }

        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
