using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public TextMeshProUGUI tmPro;

    bool bgmON = true;
    public void BGMOnAndOff()
    {
        bgmON = !bgmON;
        GM_Script.TriggerEvent(EventType.eBGMONOFF, null);
        if (bgmON)
        {
            tmPro.text = "BGM OFF";
        }
        else if (!bgmON)
        {
            tmPro.text = "BGM ON";
        }

    }
    public void GameEnd()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
                 Application.OpenURL(webplayerQuitURL);
        #else
                 Application.Quit();
        #endif
    }
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
