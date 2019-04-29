using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;

public class messageSystem : NetworkBehaviour {

    private static messageSystem _instance;
    public static messageSystem Instance { get { return _instance; } }

    [SyncVar]
    public float sycVar;
    
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

	[Command]
	public void CmdSendMessageToServer (string words)
	{
		RpcSentMessageToClients(words);
        
	}


	[ClientRpc]
	public void RpcSentMessageToClients(string words)
	{
        MessageManager.Instance.ReciveMessage(words);
	}

    [ClientRpc]
    public void RpcShowHoldScreen()
    {
        //GameManager.instance.ShowHoldScreen();
    }

    [ClientRpc]
    public void RpcShowMainScreen()
    {
        //GameManager.instance.ShowMainMenu();
    }


}
