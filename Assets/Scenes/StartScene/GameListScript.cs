using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameListScript : MonoBehaviour
{
    // Start is called before the first frame update


    public void TetrisOnClick() {
        SceneManager.LoadScene("Main");
    }
    public void MazeOnClick()
	{
        SceneManager.LoadScene("Maze");
    }
}
