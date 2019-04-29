using UnityEngine;
using System.Collections;
public class Menu : MonoBehaviour
{

    public string ip = "169.254.17.167";
    public int port = 25000;

    void OnGUI()
    {
        //if the player is NOT connected
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            //this is temporary for input of the ip address
            //find out your ip address and assign it here during gameplay
            ip = GUI.TextField(new Rect(200, 100, 100, 25), ip);
            port = int.Parse(GUI.TextField(new Rect(200, 125, 100, 25), "" + port));

            //if the player wants to connect to a server
            if (GUI.Button(new Rect(100, 100, 100, 25), "Start Client"))
            {
                //this is where we actually connect to the server
                Network.Connect(ip, port);
            }

            //if the player wants to start a server
            if (GUI.Button(new Rect(100, 125, 100, 25), "Create Server"))
            {
                Network.InitializeServer(10, port, false);
            }
        }//end of "if the player is NOT connected"

        else //if the player IS connected
        {
            if (Network.peerType == NetworkPeerType.Client)
            {
                //letting the player know that they are a client to a server
                GUI.Label(new Rect(100, 100, 100, 25), "Client");

                //if the player wants to disconnect
                if (GUI.Button(new Rect(100, 125, 100, 25), "Logout"))
                    Network.Disconnect(200);//the 200 is in milliseconds for the disconnect

            }

            if (Network.peerType == NetworkPeerType.Server)
            {
                //letting the player know that they are a server
                GUI.Label(new Rect(100, 100, 100, 25), "Server");

                //this shows how many people are connected to your server
                GUI.Label(new Rect(100, 125, 100, 25), "Connections: " + Network.connections.Length);

                //if the player wants to disconnect
                if (GUI.Button(new Rect(100, 150, 100, 25), "Logout"))
                    Network.Disconnect(200); //the 200 is in milliseconds for the disconnect

            }
        }//end of "if the player IS connected"

    }
}