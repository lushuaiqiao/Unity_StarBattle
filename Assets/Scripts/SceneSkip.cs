using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSkip : MonoBehaviour
{
    void Update()
    {

        if (Input.anyKeyDown&&!(Input.GetKey(KeyCode.Escape))) {

            SceneManager.LoadScene("Main");
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

    }
}
