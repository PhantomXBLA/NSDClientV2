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
            chat.text = dropdownMenu.options[1].text;
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
