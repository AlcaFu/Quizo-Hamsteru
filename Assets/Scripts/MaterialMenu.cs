using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialMenu : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        int unlockedMaterial = PlayerPrefs.GetInt("Unlocked Material", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedMaterial; i++)
        {
            buttons[i].interactable = true;
        }
    }
}
