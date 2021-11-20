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

    GameObject XWinText;
    GameObject OWinText;
    GameObject TieText;

    GameObject[] allButtons;

    bool inGame;
    bool gameWon;

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

            else if (go.name == "XWinsText")
            {
                XWinText = go;
            }

            else if (go.name == "OWinsText")
            {
                OWinText = go;
            }

            else if (go.name == "TieText")
            {
                TieText = go;
            }
        }

        SubmitButton.GetComponent<Button>().onClick.AddListener(OnSubmitButtonPressed);
        LoginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggleChanged);
        CreateAccountToggle.GetComponent<Toggle>().onValueChanged.AddListener(AccountToggleChanged);
        JoinGameRoomButton.GetComponent<Button>().onClick.AddListener(OnJoinGameRoomButtonPressed);
        ReplayButton.GetComponent<Button>().onClick.AddListener(OnReplayButtonPressed);



        changeState(GameStates.LoginMenu);





        gameWon = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (inGame == true && gameWon == false)
        {
            checkWinForX();
            checkWinForO();



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

    void checkWinForX()
    {
        if (allButtons[0].GetComponent<ButtonScript>().xHere == true &&
            allButtons[1].GetComponent<ButtonScript>().xHere == true &&
            allButtons[2].GetComponent<ButtonScript>().xHere == true)
        {
            Debug.Log("yeah buddy that's a win for x across the top, lets go");
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.WinForX + "");

            gameWon = true;
        }

        else if (allButtons[0].GetComponent<ButtonScript>().xHere == true &&
                 allButtons[3].GetComponent<ButtonScript>().xHere == true &&
                 allButtons[6].GetComponent<ButtonScript>().xHere == true)
        {
            Debug.Log("yeah buddy that's a win for x down the left side, lets go");
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.WinForX + "");

            gameWon = true;
        }

        else if (allButtons[0].GetComponent<ButtonScript>().xHere == true &&
                 allButtons[4].GetComponent<ButtonScript>().xHere == true &&
                 allButtons[8].GetComponent<ButtonScript>().xHere == true)
        {
            Debug.Log("yeah buddy that's a win for x diagonal top left/bottom right, lets go");
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.WinForX + "");

            gameWon = true;
        }

        else if (allButtons[2].GetComponent<ButtonScript>().xHere == true &&
                 allButtons[4].GetComponent<ButtonScript>().xHere == true &&
                 allButtons[6].GetComponent<ButtonScript>().xHere == true)
        {
            Debug.Log("yeah buddy that's a win for x diagonal top right/bottom left, lets go");
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.WinForX + "");

            gameWon = true;
        }

        else if (allButtons[1].GetComponent<ButtonScript>().xHere == true &&
                 allButtons[4].GetComponent<ButtonScript>().xHere == true &&
                 allButtons[7].GetComponent<ButtonScript>().xHere == true)
        {
            Debug.Log("yeah buddy that's a win for x down the center, lets go");
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.WinForX + "");

            gameWon = true;
        }

        else if (allButtons[2].GetComponent<ButtonScript>().xHere == true &&
                 allButtons[5].GetComponent<ButtonScript>().xHere == true &&
                 allButtons[8].GetComponent<ButtonScript>().xHere == true)
        {
            Debug.Log("yeah buddy that's a win for x down the right, lets go");
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.WinForX + "");

            gameWon = true;
        }

        else if (allButtons[3].GetComponent<ButtonScript>().xHere == true &&
                 allButtons[4].GetComponent<ButtonScript>().xHere == true &&
                 allButtons[5].GetComponent<ButtonScript>().xHere == true)
        {
            Debug.Log("yeah buddy that's a win for x across the center, lets go");
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.WinForX + "");

            gameWon = true;
        }

        else if (allButtons[6].GetComponent<ButtonScript>().xHere == true &&
                 allButtons[7].GetComponent<ButtonScript>().xHere == true &&
                 allButtons[8].GetComponent<ButtonScript>().xHere == true)
        {
            Debug.Log("yeah buddy that's a win for x across the bottom, lets go");
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.WinForX + "");

            gameWon = true;
        }
    }

    void checkWinForO()
    {
        if (allButtons[0].GetComponent<ButtonScript>().oHere == true &&
            allButtons[1].GetComponent<ButtonScript>().oHere == true &&
            allButtons[2].GetComponent<ButtonScript>().oHere == true)
        {
            Debug.Log("yeah buddy that's a win for o across the top, lets go");
            gameWon = true;
        }

        else if (allButtons[0].GetComponent<ButtonScript>().oHere == true &&
                 allButtons[3].GetComponent<ButtonScript>().oHere == true &&
                 allButtons[6].GetComponent<ButtonScript>().oHere == true)
        {
            Debug.Log("yeah buddy that's a win for o down the left side, lets go");
            gameWon = true;
        }

        else if (allButtons[0].GetComponent<ButtonScript>().oHere == true &&
                 allButtons[4].GetComponent<ButtonScript>().oHere == true &&
                 allButtons[8].GetComponent<ButtonScript>().oHere == true)
        {
            Debug.Log("yeah buddy that's a win for o diagonal top left/bottom right, lets go");
            gameWon = true;
        }

        else if (allButtons[2].GetComponent<ButtonScript>().oHere == true &&
                 allButtons[4].GetComponent<ButtonScript>().oHere == true &&
                 allButtons[6].GetComponent<ButtonScript>().oHere == true)
        {
            Debug.Log("yeah buddy that's a win for o diagonal top right/bottom left, lets go");
            gameWon = true;
        }

        else if (allButtons[1].GetComponent<ButtonScript>().oHere == true &&
                 allButtons[4].GetComponent<ButtonScript>().oHere == true &&
                 allButtons[7].GetComponent<ButtonScript>().oHere == true)
        {
            Debug.Log("yeah buddy that's a win for o down the center, lets go");
            gameWon = true;
        }

        else if (allButtons[2].GetComponent<ButtonScript>().oHere == true &&
                 allButtons[5].GetComponent<ButtonScript>().oHere == true &&
                 allButtons[8].GetComponent<ButtonScript>().oHere == true)
        {
            Debug.Log("yeah buddy that's a win for o down the right, lets go");
            gameWon = true;
        }

        else if (allButtons[3].GetComponent<ButtonScript>().oHere == true &&
                 allButtons[4].GetComponent<ButtonScript>().oHere == true &&
                 allButtons[5].GetComponent<ButtonScript>().oHere == true)
        {
            Debug.Log("yeah buddy that's a win for o across the center, lets go");
            gameWon = true;
        }

        else if (allButtons[6].GetComponent<ButtonScript>().oHere == true &&
                 allButtons[7].GetComponent<ButtonScript>().oHere == true &&
                 allButtons[8].GetComponent<ButtonScript>().oHere == true)
        {
            Debug.Log("yeah buddy that's a win for o across the bottom, lets go");
            gameWon = true;
        }
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

        ReplayButton.SetActive(false);

        WaitingForPlayerText.SetActive(false);

        inGame = false;

        XWinText.SetActive(false);
        OWinText.SetActive(false);
        TieText.SetActive(false);

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

        else if(newState == GameStates.WinForX)
        {
            Grid.SetActive(true);
            ReplayButton.SetActive(true);
            XWinText.SetActive(true);
        }

        else if (newState == GameStates.WinForO)
        {
            Grid.SetActive(true);
            ReplayButton.SetActive(true);
            OWinText.SetActive(true);
        }

        else if (newState == GameStates.Tie)
        {
            Grid.SetActive(true);
            ReplayButton.SetActive(true);
            TieText.SetActive(true);
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

    public const int WinForX = 6;
    public const int WinForO = 8;
    public const int Tie = 8;
}

