using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PremadeChat : MonoBehaviour
{

    public Text chat;

    Dropdown dropdownMenu;
    // Start is called before the first frame update

    private void Start()
    {
        dropdownMenu = this.gameObject.GetComponent<Dropdown>();
    }

    public void chatOption(int selection)
    {
        if (dropdownMenu.value == 1)
        {
            chat.text = "What a save!";
        }

        if (dropdownMenu.value == 2)
        {
            chat.text = "No fair!";
        }

        if (dropdownMenu.value == 3)
        {
            chat.text = "Good game.";
        }

        if (dropdownMenu.value == 4)
        {
            chat.text = "Too easy.";
        }


    }
}
