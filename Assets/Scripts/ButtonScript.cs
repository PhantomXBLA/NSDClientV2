using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject X;
    public GameObject O;



    GameObject NetworkedClient;

    public int buttonID;
    public string buttonName;

    public float boxX;
    public float boxY;
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
        //drawShape(X, boxX, boxY);
    }             

    public void drawShape(GameObject shape, float BoxX, float BoxY)
    {
        Instantiate(shape, new Vector3(BoxX, BoxY), Quaternion.identity);
        Destroy(this.gameObject);
    }
}
