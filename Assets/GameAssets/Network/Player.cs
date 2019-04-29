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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.PageDown) || Input.GetKeyDown(KeyCode.PageUp))
        {
            RpcStartCountDown();
            GameManager.instance.PlayVideo();
            Debug.Log("Message Sent");
        }
    }
}