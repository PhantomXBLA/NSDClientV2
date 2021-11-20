using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject SubmitButton;
    GameObject UsernameInput;
    GameObject PasswordInput;

    GameObject LoginToggle;
    GameObject CreateAccountToggle;

    GameObject NetworkedClient;

    GameObject JoinGameRoomButton;
    GameObject ReplayButton;
    GameObject WaitingForPlayerText;

    GameObject Grid;
    GameObject LoginScreen;

    GameObject[] allButtons;

    bool inGame;

    void Start()
    {

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            if (go.name == "UsernameInput")
            {
                UsernameInput = go;
            }

            else if (go.name == "PasswordInput")
            {
                PasswordInput = go;
            }

            else if (go.name == "SubmitButton")
            {
                SubmitButton = go;
            }

            else if (go.name == "LoginToggle")
            {
                LoginToggle = go;
            }

            else if (go.name == "CreateAccountToggle")
            {
                CreateAccountToggle = go;
            }

            else if (go.name == "NetworkedClient")
            {
                NetworkedClient = go;
            }

            else if (go.name == "JoinGameRoomButton")
            {
                JoinGameRoomButton = go;
            }

            else if (go.name == "Grid")
            {
                Grid = go;
            }

            else if (go.name == "LoginScreen")
            {
                LoginScreen = go;
            }

            else if (go.name == "WaitingForPlayerText")
            {
                WaitingForPlayerText = go;
            }
            else if (go.name == "ReplayButton")
            {
                ReplayButton = go;
            }
        }

        SubmitButton.GetComponent<Button>().onClick.AddListener(OnSubmitButtonPressed);
        LoginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggleChanged);
        CreateAccountToggle.GetComponent<Toggle>().onValueChanged.AddListener(AccountToggleChanged);
        JoinGameRoomButton.GetComponent<Button>().onClick.AddListener(OnJoinGameRoomButtonPressed);
        ReplayButton.GetComponent<Button>().onClick.AddListener(OnReplayButtonPressed);



        changeState(GameStates.LoginMenu);








    }

    // Update is called once per frame
    void Update()
    {
        if (inGame == true)
        {
            if (allButtons[0].GetComponent<ButtonScript>().xHere == true && 
                allButtons[1].GetComponent<ButtonScript>().xHere == true &&
                allButtons[2].GetComponent<ButtonScript>().xHere == true)
            {
                Debug.Log("yeah buddy that's a win for x, lets go");
            }
        }

    }

    public void OnSubmitButtonPressed()
    {
        Debug.Log("button pressed");

        string p = UsernameInput.GetComponent<InputField>().text;
        string n = PasswordInput.GetComponent<InputField>().text;

        string msg;

        if (CreateAccountToggle.GetComponent<Toggle>().isOn)
        {
            msg = ClientToServerSignifiers.CreateAccount + "," + n + "," + p;
        }
        else
        {
            msg = ClientToServerSignifiers.Login + "," + n + "," + p;
        }

        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
        Debug.Log(msg);

    }

    public void OnJoinGameRoomButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.JoinGameRoomQueue + "");
        changeState(GameStates.WaitingInQueue);
    }

    public void OnReplayButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.JoinReplay + "");
        changeState(GameStates.Replay);

    }

    public void LoginToggleChanged(bool changedValue)
    {
        CreateAccountToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!changedValue);
    }

    public void AccountToggleChanged(bool changedValue)
    {
        LoginToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!changedValue);
    }

    public void changeState(int newState)
    {
        LoginScreen.SetActive(false);

        JoinGameRoomButton.SetActive(false);

        Grid.SetActive(false);

        WaitingForPlayerText.SetActive(false);
        inGame = false;
        if (newState == GameStates.LoginMenu)
        {
            LoginScreen.SetActive(true);
        }
        else if (newState == GameStates.MainMenu)
        {
            LoginScreen.SetActive(false);
            JoinGameRoomButton.SetActive(true);
        }
        else if (newState == GameStates.WaitingInQueue)
        {
            WaitingForPlayerText.SetActive(true);



        }
        else if (newState == GameStates.Game)
        {
            Grid.SetActive(true);
            WaitingForPlayerText.SetActive(false);

            
            allButtons = GameObject.FindGameObjectsWithTag("GameButton");

            //allButtons[0].get
            //allButtons[1] = GameObject.Find("Top Center");
            //allButtons[2] = GameObject.Find("Top Right");
            //allButtons[3] = GameObject.Find("Center Left");
            //allButtons[4] = GameObject.Find("Center");
            //allButtons[5] = GameObject.Find("Center Right");
            //allButtons[6] = GameObject.Find("Bottom Left");
            //allButtons[7] = GameObject.Find("Bottom Center");
            //allButtons[8] = GameObject.Find("Bottom Right");

            for (int i = 0; i < allButtons.Length; i++)
            {
                Debug.Log(allButtons[i]);
            }

            inGame = true;

        }

        else if(newState == GameStates.Replay)
        {
            Grid.SetActive(true);
        }

    }
}

static public class GameStates
{
    public const int LoginMenu = 1;
    public const int MainMenu = 2;
    public const int WaitingInQueue = 3;
    public const int Game = 4;
    public const int Replay = 5;
}

