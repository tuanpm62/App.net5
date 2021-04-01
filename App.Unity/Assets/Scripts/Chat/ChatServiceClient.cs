using App.Shared.Services;
using MagicOnion;
using MagicOnion.Client;

public class ChatServiceClient
{
    readonly ChatComponent chatComponent;

    IChatService client;

    public ChatServiceClient(ChatComponent chatComponent)
    {
        this.chatComponent = chatComponent;
    }

    public void Create(GrpcChannelx grpcChannel)
    {
        client = MagicOnionClient.Create<IChatService>(grpcChannel);
    }

    #region Client -> Server (Unary)
    public async void SendReport()
    {
        await client.SendReportAsync(chatComponent.ReportInput.text);

        chatComponent.ReportInput.text = string.Empty;
    }

    public async void UnaryGenerateException()
    {
        // unary
        await client.GenerateException("client exception(unary)！");
    }
    #endregion
}