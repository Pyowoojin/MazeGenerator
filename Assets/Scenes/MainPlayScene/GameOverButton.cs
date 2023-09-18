using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
	// Start is called before the first frame update

	GameObject rankingUI;
	UIManager score;
	Scene curScene;
	private void Start()
	{
		rankingUI = GameObject.Find("InputObject").transform.GetChild(0).gameObject;
		score = FindObjectOfType(typeof(UIManager)) as UIManager;
		curScene = SceneManager.GetActiveScene();
	}

	public void RegisterRank()
	{
		rankingUI.SetActive(true);
		// BackendRank.Instance.RankInsert(score.score);
		BackendRank.Instance.TetrisRankInsert(UIManager.Instance.score);
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
		// Debug.Log(curScene.name);
		SceneManager.LoadScene(curScene.name);
		// SceneManager.LoadScene("Main");
		// Debug.Log(UIManager.Instance.score);
	}
}
