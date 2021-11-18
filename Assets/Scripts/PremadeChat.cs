using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PremadeChat : MonoBehaviour
{

    public Text chat;

    Dropdown dropdownMenu;

    GameObject NetworkedClient;

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

    }

    public void chatOption(int selection)
    {
        if (dropdownMenu.value == 1)
        {
            chat.text = dropdownMenu.options[1].text;
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.InGame + "," + ChatSignifiers.PremadeMessage + "," + chat.text);

        }

        if (dropdownMenu.value == 2)
        {
            chat.text = dropdownMenu.options[2].text;
        }

        if (dropdownMenu.value == 3)
        {
            chat.text = dropdownMenu.options[3].text;
        }

        if (dropdownMenu.value == 4)
        {
            chat.text = dropdownMenu.options[4].text;
        }


    }
}