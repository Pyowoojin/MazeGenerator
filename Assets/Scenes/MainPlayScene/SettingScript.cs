using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ESC를 누를 시에 게임이 중지되며 설정창이 나옴
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
//			Debug.Log("시간 정지");
			Time.timeScale = 0;
			GM_Script.TriggerEvent(EventType.eGamePaused, null);
			isStop = true;
		}
		else if(Input.GetKeyDown(KeyCode.Escape) && isStop && GM_Script.isPlaying)
		{ 
			// Debug.Log("시간 재개");
			Time.timeScale = 1;
			isStop = false;
			GM_Script.TriggerEvent(EventType.eGamePaused, null);
			menuPanel.SetActive(false);
		}
	}
}
