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

    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach(GameObject go in allObjects)
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
