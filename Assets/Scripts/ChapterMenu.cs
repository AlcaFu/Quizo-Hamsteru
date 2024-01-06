using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterMenu : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        int unlockedChapter = PlayerPrefs.GetInt("Unlocked Chapter", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedChapter; i++)
        {
            buttons[i].interactable = true;
        }
    }
}
