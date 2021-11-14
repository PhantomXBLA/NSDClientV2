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

    GameObject Game;

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
                Game = go;
            }
        }

        SubmitButton.GetComponent<Button>().onClick.AddListener(OnSubmitButtonPressed);
        LoginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggleChanged);
        CreateAccountToggle.GetComponent<Toggle>().onValueChanged.AddListener(AccountToggleChanged);

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
        JoinGameRoomButton.SetActive(false);
        SubmitButton.SetActive(true);
        UsernameInput.SetActive(true);
        PasswordInput.SetActive(true);

        LoginToggle.SetActive(true);
        CreateAccountToggle.SetActive(true);

        if (newState == GameStates.LoginMenu)
        {

        }
        else if (newState == GameStates.MainMenu)
        {
            JoinGameRoomButton.SetActive(true);
        }
        else if (newState == GameStates.Game)
        {
            Game.SetActive(true);
        }


    }
}

static public class GameStates
{
    public const int LoginMenu = 1;
    public const int MainMenu = 2;
    public const int Game = 3;
}



public static class ClientToServerSignifiers
{
    public const int CreateAccount = 1;
    public const int Login = 2;
}

public static class ServerToClientSignifiers
{
    public const int LoginComplete = 1;
    public const int LoginFailed = 2;

    public const int AccountCreationComplete = 3;
    public const int AccountCreationFailed = 3;
}

