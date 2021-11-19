using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{

    public Text chat;

    Dropdown dropdownMenu;

    GameObject NetworkedClient;

    string textMessage;

    public GameObject chatInput;
    public GameObject sendButton;

    // Start is called before the first frame update



    private void Start()
    {
        dropdownMenu = this.gameObject.GetComponent<Dropdown>();

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            if (go.name == "NetworkedClient")
            {
                NetworkedClient = go;
            }
        }

        sendButton.GetComponent<Button>().onClick.AddListener(OnSendButtonPressed);
        chatInput.GetComponent<InputField>().characterLimit = 10;


    }

    public void chatOption(int selection)
    {
        if (dropdownMenu.value == 1)
        {
            //chat.text = dropdownMenu.options[1].text;
            textMessage = dropdownMenu.options[1].text;
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.InGame + "," + ChatSignifiers.PremadeMessage + "," + textMessage);

        }

        if (dropdownMenu.value == 2)
        {
            //chat.text = dropdownMenu.options[2].text;
            textMessage = dropdownMenu.options[2].text;
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.InGame + "," + ChatSignifiers.PremadeMessage + "," + textMessage);

        }

        if (dropdownMenu.value == 3)
        {
            //chat.text = dropdownMenu.options[3].text;
            textMessage = dropdownMenu.options[3].text;
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.InGame + "," + ChatSignifiers.PremadeMessage + "," + textMessage);

        }

        if (dropdownMenu.value == 4)
        {
            //chat.text = dropdownMenu.options[4].text;
            textMessage = dropdownMenu.options[4].text;
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.InGame + "," + ChatSignifiers.PremadeMessage + "," + textMessage);

        }


    }

    void OnSendButtonPressed()
    {
        string message = chatInput.GetComponent<InputField>().text;
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.InGame + "," + ChatSignifiers.Message + "," + message);


    }
}
