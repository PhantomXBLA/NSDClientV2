using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemManager : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject submitButton;
    GameObject usernameInput;
    GameObject passwordInput;

    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
