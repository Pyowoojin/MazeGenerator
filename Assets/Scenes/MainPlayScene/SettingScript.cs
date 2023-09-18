using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ESC�� ���� �ÿ� ������ �����Ǹ� ����â�� ����
public class SettingScript : MonoBehaviour
{
	bool isStop = false;
	GameObject menuPanel;
	private void Start()
	{
		menuPanel = GameObject.Find("Canvas").transform.Find("MenuPanel").gameObject;
	}

	// Update is called once per frame
	void Update()
	{
		SettingPanelOnOff();
	}

	void SettingPanelOnOff()
    {
		if (Input.GetKeyDown(KeyCode.Escape) && !isStop && GM_Script.isPlaying)
        {
			menuPanel.SetActive(true);
//			Debug.Log("�ð� ����");
			Time.timeScale = 0;
			GM_Script.TriggerEvent(EventType.eGamePaused, null);
			isStop = true;
		}
		else if(Input.GetKeyDown(KeyCode.Escape) && isStop && GM_Script.isPlaying)
		{ 
			// Debug.Log("�ð� �簳");
			Time.timeScale = 1;
			isStop = false;
			GM_Script.TriggerEvent(EventType.eGamePaused, null);
			menuPanel.SetActive(false);
		}
	}
}
