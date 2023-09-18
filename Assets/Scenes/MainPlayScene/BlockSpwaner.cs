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

		// �ش� ��ġ�� ����� ���ٸ�, ����� ��������
		if (TetrisBlock.grid[myX, myY] == null)
		{
			Instantiate(blocks[Random.Range(0, blockSize)], transform.position, Quaternion.identity);
		}
		// ����� �����Ѵٸ� ������ �����Ű�� �ñ׳��� ������.
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