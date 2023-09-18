using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpwaner : MonoBehaviour { 

	public GameObject[] blocks;
	public int blockSize = 7;
	public int score = 0;
	
	// Start is called before the first frame update
	void Start()
	{
		CreateBlock();
		GM_Script.StartListening(EventType.eBlockMovingFalse, CreateBlock);
		GM_Script.StartListening(EventType.eBlockDeleted, ScoreUp);
	}

	void CreateBlock()
	{
		int myX = Mathf.RoundToInt(gameObject.transform.position.x);
		int myY = Mathf.RoundToInt(gameObject.transform.position.y);

		// 해당 위치에 블록이 없다면, 블록을 생성해줌
		if (TetrisBlock.grid[myX, myY] == null)
		{
			Instantiate(blocks[Random.Range(0, blockSize)], transform.position, Quaternion.identity);
		}
		// 블록이 존재한다면 게임을 종료시키는 시그널을 보낸다.
        else
        {
			GM_Script.TriggerEvent(EventType.eGameOver, null);
			GM_Script.isPlaying = false;
        }
	}

	void ScoreUp()
    {
		score += 15;
    }

	
}