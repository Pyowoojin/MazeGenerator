using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fixed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetResolution();
        // SceneManager.LoadScene("Main");
    }

    // Update is called once per frame
    private void SetResolution()
    {
        int setWidth = 1024;
        int setHeight = 768;
        Screen.SetResolution(setWidth, setHeight, false);
    }
}
