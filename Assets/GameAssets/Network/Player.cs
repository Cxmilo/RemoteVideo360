using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    [ClientRpc]
    public void RpcStartCountDown()
    {
        Debug.Log("Message Recived");
        GameManager.instance.PlayVideo();
    }

    [ClientRpc]
    public void RpcStopVideo()
    {
        Debug.Log("Message Recived");
        GameManager.instance.StopPresentation();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            RpcStartCountDown();
            GameManager.instance.PlayVideo();
            Debug.Log("Message Sent");
        }

        if(Input.GetKeyDown(KeyCode.PageUp))
        {
            RpcStopVideo();
            GameManager.instance.StopPresentation();
            Debug.Log("Message Sent");
        }
    }
}