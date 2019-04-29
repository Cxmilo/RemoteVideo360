using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Networking;

public class NetManager : NetworkManager
{
    public int currentConnections;
    public int maxConnection;
    public int server_port = 5000;
    public string server_ip;
    private List<string> conectionsIp = new List<string>();

    //multicast
    private int startup_port = 5100;

    //no funciona con equipos wlan

    private IPAddress group_address = IPAddress.Parse("224.0.0.224");
    private UdpClient udp_client;
    private IPEndPoint remote_end;

    private static NetManager _instance;

    public static NetManager Instance { get { return _instance; } }

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

        base.maxConnections = maxConnection;

        var config = new ConnectionConfig();
        config.AddChannel(QosType.ReliableSequenced);
        config.AddChannel(QosType.Unreliable);
        NetworkServer.Configure(config, 20);

    }

    public void StartGameServer()
    {
        NetManager.Instance.networkPort = server_port;
        NetManager.Instance.networkAddress = Network.player.ipAddress;
        NetManager.Instance.StartServer();

        Debug.Log("Current connections" + NetworkServer.connections);
        Debug.Log("max connections " + NetManager.Instance.maxConnections);
    }

    public void StartGameClient()
    {
        server_ip = "192.168.0.25";
        GameManager.instance.EnableVR();
        StartCoroutine(MakeConnection());
    }

    private IEnumerator MakeConnection()
    {
        //continues after we get server Addres

        while (server_ip == "")
            yield return null;

        while (!IsClientConnected())
        {
            if (server_ip != "")
            {
                // DebugConsole.Log( UnityEngine.Random.Range(0,0.1f) + "-" + "Conecting:" + server_ip + ":" + server_port);
                // the unity 3d way to connect to a server

                NetManager.Instance.networkPort = server_port;
                NetManager.Instance.networkAddress = server_ip;
                NetManager.Instance.StartClient();

                /*NetworkConnectionError error = Network.Connect(server_ip, server_port);
                DebugConsole.Log(UnityEngine.Random.Range(1, 10) + error.ToString());*/
            }

            yield return new WaitForSeconds(10);
        }
    }

    private void Update()
    {
        for (int i = 0; i < NetworkServer.connections.Count; i++)
        {
            if (NetworkServer.connections[i] != null)
            {
                if (!conectionsIp.Contains(NetworkServer.connections[i].address))
                {
                    conectionsIp.Add(NetworkServer.connections[i].address);
                }
            }
        }

        currentConnections = conectionsIp.Count;
    }

    public override void OnClientDisconnect(UnityEngine.Networking.NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        Debug.Log(NetworkServer.connections);
        // server_ip = "";
        StartGameClient();
    }

    public override void OnClientConnect(UnityEngine.Networking.NetworkConnection conn)
    {
        base.OnClientConnect(conn);
    }

    public override void OnClientError(UnityEngine.Networking.NetworkConnection conn, int errorCode)
    {
        base.OnClientError(conn, errorCode);
        StartGameClient();
    }

    public void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        StartGameClient();
    }
}