using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject X;
    public GameObject O;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onclickButton()
    {
        Debug.Log(this.gameObject.name);
        Instantiate(X, this.gameObject.transform.position, Quaternion.identity);

    }
}
