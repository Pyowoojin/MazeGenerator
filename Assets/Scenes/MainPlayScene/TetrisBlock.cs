using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
	public float lapsedTime = 0.0f;
	public float fallingSpeed = 1.0f;
	
	bool inputCheck = false;
	bool isPaused = false;

	int endOfY = 0;
	int endOfLeftX = 1;
	public static int endOfRightX = 13;
	public static int startOfY = 20;


	public static Transform[,] grid = new Transform[endOfRightX+1, startOfY];

    private void Start()
    {
		GM_Script.StartListening(EventType.eGamePaused, GamePaused);
	}
    void Update()
	{
		if (!isPaused)
		{
			InputMove();
			AutoFalling(inputCheck);
			SpaceForFallingBlock();
			RotateBlock();
		}
	}

	void GamePaused()
    {
		isPaused = !isPaused;
    }

	void CheckForLines()
    {
		for(int i = startOfY-1; i>=0; i--)
        {
            if (HasLine(i))
            {
				DeletedBlocks();
				DelLine(i);
				RowDown(i);
            }
        }
    }

	bool HasLine(int i)
    {
		for(int j = 1; j<=endOfRightX; j++)
        {
			if (grid[j, i] == null)
				return false;
        }
		return true;
    }

	void DelLine(int i)
    {
		for(int j = 1; j<=endOfRightX; j++)
        {
			Destroy(grid[j, i].gameObject);
			grid[j, i] = null;
        }
    }

	void RowDown(int i)
    {
		for (int y = i; y < startOfY; y++)
        {
			for (int j = 1; j <= endOfRightX; j++)
            {
				if (grid[j,y] != null)
                {
					grid[j, y - 1] = grid[j, y];
					grid[j, y] = null;
					grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

	/*void PostNotification()
	{
		CheckForLines();
		GM_Script.instance.PostNotification(Event_type.eBlockMovingFalse);
	}*/

	void NeedToCreateNewBlock()
	{
		CheckForLines();
		GM_Script.TriggerEvent(EventType.eBlockMovingFalse, null);
	}

	void DeletedBlocks() {
		GM_Script.TriggerEvent(EventType.eBlockDeleted, null);
		GM_Script.TriggerEvent(EventType.eScoreChanged, null);
	}

	/*	void PostNotif_BlockDel()
		{
			GM_Script.instance.PostNotification(Event_type.eBlockDeleted);
			GM_Script.instance.PostNotification(Event_type.eScoreChanged);
		}*/

	void AddToGrid()
	{
		foreach (Transform children in transform)
        {
			int roundX = Mathf.RoundToInt(children.transform.position.x);
			int roundY = Mathf.RoundToInt(children.transform.position.y);

			grid[roundX, roundY] = children;
		}
    }


	// Down Arrow Input이 없을 경우 자동으로 블록을 떨어트려주는 함수
	void AutoFalling(bool check)
    {
        if (check)
        {
			lapsedTime = 0.0f;
			inputCheck = false;
        }

		lapsedTime += Time.deltaTime;

		if(lapsedTime >= fallingSpeed && transform.position.y >= endOfY)
        {
			transform.position += new Vector3(0, -1, 0);
            if (!Valid())
            {
				// Debug.Log("AutoFalling End");
				// transform.position -= new Vector3(0, -1, 0);
				NotValid(0, -1, 0);
				AddToGrid();
				this.enabled = false;
				NeedToCreateNewBlock();
			}
			lapsedTime = 0;
        }
    }

	// 스페이스 바를 누르면 블록이 맨 아래로 떨어짐
	void SpaceForFallingBlock()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
			int target = (int)transform.position.y;
			for (int i = 0; i <= target; i++)
			{ 
				if(Valid())
					transform.Translate(new Vector3(0, -1, 0), Space.World);

                if (!Valid())
                {
					// Debug.Log("Z_Falling End");
					NotValid(0, -1, 0);
					AddToGrid();
					this.enabled = false;
					NeedToCreateNewBlock();
					break;
				}
			}
        }
    }

	void NotValid(int x, int y, int z)
    {
		transform.position -= new Vector3(x, y, z);
    }

	void InputMove()
	{
		// 왼쪽 이동
		if (transform.position.x - 1 >= endOfLeftX)
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				transform.position += new Vector3(-1, 0, 0);
				if (!Valid())
				{
					//transform.position -= new Vector3(-1, 0, 0);
					NotValid(-1, 0, 0);
				}
			}
		}
		// 오른쪽 이동
		if (transform.position.x + 1 <= endOfRightX)
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				transform.position += new Vector3(1, 0, 0);
				if (!Valid())
				{
					// transform.position -= new Vector3(1, 0, 0);
					NotValid(1, 0, 0);
				}
			}
		}
		// 아래 이동
		if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y >= endOfY)
		{
			transform.position += new Vector3(0, -1, 0);
			inputCheck = true;
			if (!Valid())
			{
				// Debug.Log("DownArrowFalling End");
				// transform.position -= new Vector3(0, -1, 0);
				NotValid(0, -1, 0);
				AddToGrid();
				this.enabled = false;
				NeedToCreateNewBlock();
			}
		}
	}

	// 블록을 회전하는 함수
	void RotateBlock()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && gameObject.tag != "Sqaure_Block")
        {
			transform.eulerAngles += new Vector3(0, 0, -90);
            if (!Valid())
            {
				transform.eulerAngles -= new Vector3(0, 0, -90);
			}
        }
    }
	bool Valid()
    {
		foreach (Transform t in transform)
		{
			int roundX = Mathf.RoundToInt(t.transform.position.x);
			int roundY = Mathf.RoundToInt(t.transform.position.y);
			
			if (roundX > endOfRightX || roundX < endOfLeftX || roundY < endOfY || roundY > startOfY)
            {
				return false;
            }
			if (grid[roundX, roundY] != null)
				return false;
		}

		return true;
	}
}