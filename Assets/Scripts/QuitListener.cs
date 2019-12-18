using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class QuitListener : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        ListenForQuit();
    }
    private void ListenForQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                        EditorApplication.isPlaying = false;
            #else
                     Application.Quit();
            #endif
        }
    }
}
