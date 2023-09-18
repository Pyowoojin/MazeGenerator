using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankingRegisterScript : MonoBehaviour
{
	public TMP_InputField playerInput;
	// Start is called before the first frame update
	GameObject ui_grandparent;
	private void Start()
	{
		// ui_grandparent = gameObject.transform.parent.gameObject.transform.parent.gameObject;

	}

	private void Update()
	{
		ESCorXButton1();
		// Debug.Log(playerName);
	}
	public void ESCorXButton1()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && this.gameObject.activeSelf)
		{
			// Debug.Log("¡æ∑·«‘");
			this.gameObject.SetActive(false);
		}
	}

	public void ESCorXButton()
	{
		this.gameObject.SetActive(false);
	}

	public void LoginBtn()
	{

	}
}
