using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkedClient : MonoBehaviour
{

    int connectionID;
    int maxConnections = 1000;
    int reliableChannelID;
    int unreliableChannelID;
    int hostID;
    int socketPort = 5491; //change back to 5491
    byte error;
    bool isConnected = false;
    int ourClientID;

    GameObject gameSystemManager;
    public Button ButtonPressed;

    public GameObject O;
    public GameObject X;

    public Text premadeText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            if(go.GetComponent<GameSystemManager>()!= null)
            {
                gameSystemManager = go;
            }
        }

            Connect();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S))
        //    //float boxX = float.Parse(csv[3]);
        //    //float boxY = float.Parse(csv[4]);

        //    //Center.GetComponent<ButtonScript>().drawShape(0, 0);

        UpdateNetworkConnection();
    }

    private void UpdateNetworkConnection()
    {
        if (isConnected)
        {
            int recHostID;
            int recConnectionID;
            int recChannelID;
            byte[] recBuffer = new byte[1024];
            int bufferSize = 1024;
            int dataSize;
            NetworkEventType recNetworkEvent = NetworkTransport.Receive(out recHostID, out recConnectionID, out recChannelID, recBuffer, bufferSize, out dataSize, out error);

            switch (recNetworkEvent)
            {
                case NetworkEventType.ConnectEvent:
                    Debug.Log("connected.  " + recConnectionID);
                    ourClientID = recConnectionID;
                    break;
                case NetworkEventType.DataEvent:
                    string msg = Encoding.Unicode.GetString(recBuffer, 0, dataSize);
                    ProcessRecievedMsg(msg, recConnectionID);
                    //Debug.Log("got msg = " + msg);
                    break;
                case NetworkEventType.DisconnectEvent:
                    isConnected = false;
                    Debug.Log("disconnected.  " + recConnectionID);
                    break;
            }
        }
    }

    private void Connect()
    {

        if (!isConnected)
        {
            Debug.Log("Attempting to create connection");

            NetworkTransport.Init();

            ConnectionConfig config = new ConnectionConfig();
            reliableChannelID = config.AddChannel(QosType.Reliable);
            unreliableChannelID = config.AddChannel(QosType.Unreliable);
            HostTopology topology = new HostTopology(config, maxConnections);
            hostID = NetworkTransport.AddHost(topology, 0);
            Debug.Log("Socket open.  Host ID = " + hostID);

            connectionID = NetworkTransport.Connect(hostID, "192.168.0.160", socketPort, 0, out error); // server is local on network ->change back to 192.168.0.160

            if (error == 0)
            {
                isConnected = true;

                Debug.Log("Connected, id = " + connectionID);

            }
        }
    }

    public void Disconnect()
    {
        NetworkTransport.Disconnect(hostID, connectionID, out error);
    }

    public void SendMessageToHost(string msg)
    {
        byte[] buffer = Encoding.Unicode.GetBytes(msg);
        NetworkTransport.Send(hostID, connectionID, reliableChannelID, buffer, msg.Length * sizeof(char), out error);
    }

    private void ProcessRecievedMsg(string msg, int id)
    {
        Debug.Log("msg recieved = " + msg + ".  connection id = " + id);
        string[] csv = msg.Split(',');
        int signifier = int.Parse(csv[0]);

        if (signifier == ServerToClientSignifiers.AccountCreationComplete)
        {
            gameSystemManager.GetComponent<GameSystemManager>().changeState(GameStates.MainMenu);
        }
        else if(signifier == ServerToClientSignifiers.LoginComplete)
        {
            gameSystemManager.GetComponent<GameSystemManager>().changeState(GameStates.MainMenu);
        }
        else if (signifier == ServerToClientSignifiers.GameStart)
        {
            gameSystemManager.GetComponent<GameSystemManager>().changeState(GameStates.Game);
         

        }
        else if (signifier == ServerToClientSignifiers.OpponentPlay)
        {
            Debug.Log("opponent play");
        }
        

        else if (signifier == ClientToServerSignifiers.InGame)
        {
            int GameSignifier = int.Parse(csv[1]);

            if(GameSignifier == GameSignifiers.PlayerMoved)
            {
                float boxX = float.Parse(csv[2]);
                float boxY = float.Parse(csv[3]);

                string buttonName = (csv[4]);
                int playerID = int.Parse(csv[5]);
                Debug.Log(buttonName);

                ButtonPressed = GameObject.Find(buttonName).GetComponent<Button>();

                if(playerID == 1)
                {
                    ButtonPressed.GetComponent<ButtonScript>().drawShape(O, boxX, boxY);
                }
                else
                {
                    ButtonPressed.GetComponent<ButtonScript>().drawShape(X, boxX, boxY);
                }
                
            }

            if(GameSignifier == ChatSignifiers.PremadeMessage)
            {
                string premadeMessage = csv[2];
                //Debug.Log(premadeMessage);

                premadeText.text = premadeMessage;
            }

        }


    }

    public bool IsConnected()
    {
        return isConnected;
    }
}

public static class ClientToServerSignifiers
{
    public const int CreateAccount = 1;
    public const int Login = 2;

    public const int JoinGameRoomQueue = 3;
    public const int InGame = 4;
}

public static class GameSignifiers
{
    public const int PlayerMoved = 1;
}

public static class ChatSignifiers
{
    public const int PremadeMessage = 2;
    public const int Message = 3;
}

public static class ServerToClientSignifiers
{
    public const int LoginComplete = 1;
    public const int LoginFailed = 2;

    public const int AccountCreationComplete = 3;
    public const int AccountCreationFailed = 4;

    public const int OpponentPlay = 5;
    public const int GameStart = 6;



}