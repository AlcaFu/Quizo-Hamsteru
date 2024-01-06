using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  

public class PreviousButton : MonoBehaviour
{
    public int targetSceneIndex;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            targetSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        }
        else
        {
            targetSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        }

        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadSceneOnClick);
        }
    }

    private void LoadSceneOnClick()
    {
        SceneManager.LoadScene(targetSceneIndex);
    }
}
