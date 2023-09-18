using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BTNScript : MonoBehaviour
{
    public TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnOnClick()
	{
        Debug.Log("e");
        if(inputField.text != null)
		{
            Debug.Log(inputField.text);
        }
	}
}
