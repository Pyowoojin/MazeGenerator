using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 점수를 바꿔주고, 게임이 종료되었을 때에 랭킹 등록 및 게임 재시작 UI를 띄워줌
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI tmPro;
    public int score;
    GameObject gameOverPanel;

    private static UIManager instance;

	public static UIManager Instance
	{
        get {
            if(instance == null)
                instance = FindObjectOfType(typeof(UIManager)) as UIManager;
            return instance;
        }
	}

    private void Start()
    {
        gameOverPanel = GameObject.Find("Canvas").transform.Find("GameoverBoard").gameObject;
        tmPro.text = "0";
        GM_Script.StartListening(EventType.eScoreChanged, ChangeScore);
        GM_Script.StartListening(EventType.eGameOver, GameOverUI);
    }

    private void ChangeScore()
    {
        score += 15;
        tmPro.text = score.ToString() + " 점";
    }
    private void GameOverUI()
    {
        gameOverPanel.SetActive(true);
        GM_Script.TriggerEvent(EventType.eGamePaused, null);
    }
}