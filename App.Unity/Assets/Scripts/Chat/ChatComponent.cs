using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ChatComponent : MonoBehaviour
{
    private CancellationTokenSource shutdownCancellation = new CancellationTokenSource();
    ChannelManager channelManager;
    ChatHubClient chatHubClient;
    ChatServiceClient chatServiceClient;

    internal bool isSelfDisConnected;

    public Text ChatText;

    public Button JoinOrLeaveButton;

    public Text JoinOrLeaveButtonText;

    public Button SendMessageButton;

    public InputField Input;

    public InputField ReportInput;

    public Button SendReportButton;

    public Button ReconnectButon;
    public Button DisconnectButon;
    public Button ExceptionButton;
    public Button UnaryExceptionButton;

    void Start()
    {
        InitializeAsync();
    }

    void OnDestroy()
    {
        shutdownCancellation.Cancel();
    }

    private async Task InitializeClientAsync()
    {
        channelManager = ChannelManager.Instance;

        while (!shutdownCancellation.IsCancellationRequested)
        {
            try
            {
                Debug.Log($"Connecting to the server...");
                await chatHubClient.ConnectAsync(channelManager.grpcChannel, shutdownCancellation.Token);
                Debug.Log($"Connection is established.");
                break;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            Debug.Log($"Failed to connect to the server. Retry after 5 seconds...");
            await Task.Delay(5 * 1000);
        }

        chatServiceClient.Create(channelManager.grpcChannel);
    }

    public async void InitializeAsync()
    {
        RegisterClient();
        AddListenerButtons();
        await InitializeClientAsync();
        InitializeUi();
    }

    private void RegisterClient()
    {
        chatHubClient = new ChatHubClient(this);
        chatServiceClient = new ChatServiceClient(this);
    }

    public void SetJoinedUi()
    {
        SendMessageButton.interactable = true;
        JoinOrLeaveButtonText.text = "Leave the room";
        Input.text = string.Empty;
        Input.placeholder.GetComponent<Text>().text = "Please enter a comment.";
        ExceptionButton.interactable = true;
    }

    public void InitializeUi()
    {
        SendMessageButton.interactable = false;
        ChatText.text = string.Empty;
        Input.text = string.Empty;
        Input.placeholder.GetComponent<Text>().text = "Please enter your name.";
        JoinOrLeaveButtonText.text = "Enter the room";
        ExceptionButton.interactable = false;
    }

    private void AddListenerButtons()
    {
        JoinOrLeaveButton.onClick.AddListener(chatHubClient.JoinOrLeave);
        SendMessageButton.onClick.AddListener(chatHubClient.SendMessage);
        SendReportButton.onClick.AddListener(chatServiceClient.SendReport);
        ReconnectButon.onClick.AddListener(ReconnectInitializedServer);
        DisconnectButon.onClick.AddListener(DisconnectServer);
        ExceptionButton.onClick.AddListener(chatHubClient.GenerateException);
        UnaryExceptionButton.onClick.AddListener(chatServiceClient.UnaryGenerateException);
    }

    private void RemoveListenerButtons()
    {
        JoinOrLeaveButton.onClick.RemoveListener(chatHubClient.JoinOrLeave);
        SendMessageButton.onClick.RemoveListener(chatHubClient.SendMessage);
        SendReportButton.onClick.RemoveListener(chatServiceClient.SendReport);
        ReconnectButon.onClick.RemoveListener(ReconnectInitializedServer);
        DisconnectButon.onClick.RemoveListener(DisconnectServer);
        ExceptionButton.onClick.RemoveListener(chatHubClient.GenerateException);
        UnaryExceptionButton.onClick.RemoveListener(chatServiceClient.UnaryGenerateException);
    }

    public async void DisconnectServer()
    {
        isSelfDisConnected = true;

        JoinOrLeaveButton.interactable = false;
        SendMessageButton.interactable = false;
        SendReportButton.interactable = false;
        DisconnectButon.interactable = false;
        ExceptionButton.interactable = false;
        UnaryExceptionButton.interactable = false;

        await chatHubClient.LeaveAsync();

        await chatHubClient.DisposeAsync();
    }

    public void ReconnectInitializedServer()
    {
        channelManager.ReconnectInitializedServerAsync();
        RemoveListenerButtons();
    }

    public async Task ReconnectServerAsync()
    {
        Debug.Log($"Reconnecting to the server...");
        await chatHubClient.ConnectAsync(channelManager.grpcChannel);
        Debug.Log("Reconnected.");

        JoinOrLeaveButton.interactable = true;
        SendMessageButton.interactable = false;
        SendReportButton.interactable = true;
        DisconnectButon.interactable = true;
        ExceptionButton.interactable = true;
        UnaryExceptionButton.interactable = true;

        isSelfDisConnected = false;
    }
}