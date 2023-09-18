using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ������ �ٲ��ְ�, ������ ����Ǿ��� ���� ��ŷ ��� �� ���� ����� UI�� �����
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
        tmPro.text = score.ToString() + " ��";
    }
    private void GameOverUI()
    {
        gameOverPanel.SetActive(true);
        GM_Script.TriggerEvent(EventType.eGamePaused, null);
    }
}