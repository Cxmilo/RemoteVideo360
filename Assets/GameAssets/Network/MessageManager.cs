using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{


    public string lastMessage;
    public Text messageRecived;

    private static MessageManager _instance;
    public static MessageManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        
    }

    // Use this for initialization



    public void ReciveMessage(string message)
    {
        messageRecived.text = message;
    }


    public void SendMessageToClients()
    {
        messageSystem.Instance.RpcSentMessageToClients("besitos" + Random.Range(0f,1f));
    }

    public void SendMessageToServer()
    {
        messageSystem.Instance.CmdSendMessageToServer("cachetada" + Random.Range(0f, 1f));
    }

}
