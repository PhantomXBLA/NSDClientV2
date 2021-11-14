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
    GameObject ReadyButton;

    GameObject Grid;
    GameObject LoginScreen;


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

            else if (go.name == "ReadyButton")
            {
                ReadyButton = go;
            }
        }

        SubmitButton.GetComponent<Button>().onClick.AddListener(OnSubmitButtonPressed);
        LoginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggleChanged);
        CreateAccountToggle.GetComponent<Toggle>().onValueChanged.AddListener(AccountToggleChanged);
        JoinGameRoomButton.GetComponent<Button>().onClick.AddListener(OnJoinGameRoomButtonPressed);
        //ReadyButton.GetComponent<Button>().onClick.AddListener(ReadyUpButtonPressed);


        changeState(GameStates.LoginMenu);

    }

    // Update is called once per frame
    void Update()
    {

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

    public void ReadyUpButtonPressed()
    {
        ReadyButton.GetComponent<Button>().onClick.AddListener(ReadyUpButtonPressed);

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

        ReadyButton.SetActive(false);

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
            //ReadyButton.SetActive(false);


        }
        else if (newState == GameStates.Game)
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
}

