using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject X;
    public GameObject O;

    GameObject NetworkedClient;

    public int buttonID;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            if(go.name == "NetworkedClient")
            {
                NetworkedClient = go;
            }
        }
        }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onclickButton()
    {
        string msg;
        //msg = this.gameObject.name + "";
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.InGame + "," + GameSignifiers.PlayerMoved + "," + buttonID);
        Instantiate(X, this.gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);

    }
}
