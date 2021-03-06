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

    int playerTurn = 1;

    public GameObject O;
    public GameObject X;

    public Text textOutputP1;
    public Text textOutputP2;

    public Text playerTurnLabel;
    public Text playerIdentifierLabel;

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
            gameSystemManager.GetComponent<GameSystemManager>().changeState(GameStates.MainMenu); //sets the state to main menu after account creation
        }
        else if(signifier == ServerToClientSignifiers.LoginComplete)
        {
            gameSystemManager.GetComponent<GameSystemManager>().changeState(GameStates.MainMenu); //sets the state to login after logging in
        }
        else if (signifier == ServerToClientSignifiers.GameStart)
        {
            gameSystemManager.GetComponent<GameSystemManager>().changeState(GameStates.Game); //sets the state to the game state after gamestart is called

            int playerID = int.Parse(csv[1]);

            if (playerID == PlayerIDCheck.PlayerID1)
            {
                Debug.Log("You are player one");
                playerIdentifierLabel.text = "You are: P1 (O)"; //sets label to represent player ID
            }

            if (playerID == PlayerIDCheck.PlayerID2)
            {
                Debug.Log("You are player two");
                playerIdentifierLabel.text = "You are: P2 (X)";
            }

           if(playerID == PlayerIDCheck.ObserverID)
            {
                Debug.Log("You are an observer!");
                playerIdentifierLabel.text = "You are: Observer";
            }


        }

        else if (signifier == ServerToClientSignifiers.GameEnd)
        {
            GameObject[] allShapes = GameObject.FindGameObjectsWithTag("Shape");


            for (int i = 0; i < allShapes.Length; i++)
            {
                Destroy(allShapes[i]); // destroys all currently placed shapes to make room for the replay ones
            }

            GameObject[] allButtons = GameObject.FindGameObjectsWithTag("GameButton");

            for (int i = 0; i < allButtons.Length; i++)
            {
                allButtons[i].GetComponent<Button>().enabled = true; //re-enables all the buttons that were disabled
            }



        }
        else if (signifier == ServerToClientSignifiers.SendReplay)
        {
            gameSystemManager.GetComponent<GameSystemManager>().changeState(GameStates.Replay); //enter replay state



            Button replayButton;
            string msgReceived = csv[1];
            replayButton = GameObject.Find(msgReceived).GetComponent<Button>();

            


            if (playerTurn == PlayerIDCheck.PlayerID1) //this is how the replay system alternates which shape should be replayed
            {
                replayButton.GetComponent<ButtonScript>().drawShape(O, replayButton.transform.position.x, replayButton.transform.position.y);
                playerTurn = PlayerIDCheck.PlayerID2;
            }

            else if (playerTurn == PlayerIDCheck.PlayerID2)
            {
                replayButton.GetComponent<ButtonScript>().drawShape(X, replayButton.transform.position.x, replayButton.transform.position.y);
                playerTurn = PlayerIDCheck.PlayerID1;
            }

            






        }



        else if (signifier == ClientToServerSignifiers.InGame)
        {
            int GameSignifier = int.Parse(csv[1]);


            if (GameSignifier == GameSignifiers.PlayerMoved)
            {
                float boxX = float.Parse(csv[2]);
                float boxY = float.Parse(csv[3]);

                string buttonName = (csv[4]);
                int playerID = int.Parse(csv[5]);



                ButtonPressed = GameObject.Find(buttonName).GetComponent<Button>();
               

                if (playerID == PlayerIDCheck.PlayerID1)
                {
                    ButtonPressed.GetComponent<ButtonScript>().drawShape(O, boxX, boxY); //calls the draw shape function on the button gameobject that was sent by the server, player one is always O
                    playerTurnLabel.text = "Player 2's Turn (X)"; // sets the player turn label to the other player

                }
                else
                {
                    ButtonPressed.GetComponent<ButtonScript>().drawShape(X, boxX, boxY); //calls the draw shape function on the button gameobject that was sent by the server, player two is always X
                    playerTurnLabel.text = "Player 1's Turn (O)"; // sets the player turn label to the other player

                }

            }

            


            if (GameSignifier == ChatSignifiers.PremadeMessage)
            {
                string premadeMessage = csv[2];
                int playerID = int.Parse(csv[3]);

                if(playerID == PlayerIDCheck.PlayerID1)
                {
                    textOutputP1.text = "P1: "+ premadeMessage; // if the player that sent the message was P1, it changes the p1 text to the message sent
                }

               if(playerID == PlayerIDCheck.PlayerID2)
                {
                    textOutputP2.text = "P2: " + premadeMessage; // if the player that sent the message was P2, it changes the p2 text to the message sent
                }

                Debug.Log(premadeMessage);

                
            }
            
            else if(GameSignifier == ChatSignifiers.Message)
            {
                string message = csv[2];
                int playerID = int.Parse(csv[3]);

                if (playerID == PlayerIDCheck.PlayerID2)
                {
                    textOutputP2.text = "P2: " + message;
                }

                if (playerID == PlayerIDCheck.PlayerID1)
                {
                    textOutputP1.text = "P1: " + message;
                }
            }

            else if (GameSignifier == ClientToServerSignifiers.WinForX)
            {
                gameSystemManager.GetComponent<GameSystemManager>().changeState(GameStates.WinForX); // if X wins, set the state accordingly

            }

            else if (GameSignifier == ClientToServerSignifiers.WinForO)
            {
                gameSystemManager.GetComponent<GameSystemManager>().changeState(GameStates.WinForO); // if O wins, set the state accordingly

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

    public const int JoinReplay = 5;

    public const int WinForX = 6;
    public const int WinForO = 7;
    public const int Tie = 8;


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
    public const int GameEnd = 7;

    public const int SendReplay = 8;




}

public static class PlayerIDCheck
{
    public const int PlayerID1 = 1;
    public const int PlayerID2 = 2;
    public const int ObserverID = 3;
}