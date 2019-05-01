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
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.PageDown) || Input.GetKeyDown(KeyCode.PageUp))
        {
            RpcStartCountDown();
            GameManager.instance.PlayVideo();
            Debug.Log("Message Sent");
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            RpcStopVideo();
            GameManager.instance.StopPresentation();
            Debug.Log("Message Sent");
        }
    }
}