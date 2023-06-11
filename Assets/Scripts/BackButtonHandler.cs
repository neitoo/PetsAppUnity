using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToPreviousScene();
        }
    }

    private void GoToPreviousScene()
    {
        int previousSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 1;
        if (previousSceneIndex >= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(previousSceneIndex);
        }
    }
}

