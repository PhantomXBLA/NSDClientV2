using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject X;
    public GameObject O;

    public bool xHere = false;
    public bool oHere = false;

    GameObject NetworkedClient;

    public int buttonID;
    public string buttonName;

    public float boxX;
    public float boxY;

    public int totalMoves = 0;
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
        boxX = this.gameObject.transform.position.x;
        boxY = this.gameObject.transform.position.y;
        

        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.InGame + "," + GameSignifiers.PlayerMoved + "," + buttonName + "," + boxX + "," +boxY);
    }             

    public void drawShape(GameObject shape, float BoxX, float BoxY)
    {
        Instantiate(shape, new Vector3(BoxX, BoxY), Quaternion.identity);
        this.gameObject.GetComponent<Button>().enabled = false;

        if(shape == X)
        {
            xHere = true;
        }

        if (shape == O)
        {
            oHere = true;
        }

        Debug.Log(gameObject.name + " X Here?: " + xHere + " O Here?: " + oHere);
        totalMoves++;
    }

}
