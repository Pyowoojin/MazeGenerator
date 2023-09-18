using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
	int up = 8;
	int right = 4;
	int down = 2;
	int left = 1;

	public int all = 15;

	public bool visited = false;
	public GameObject[] wall;

	private void Start()
	{
		// GridSetting();
	}

	public void GridSetting(int cellList_All)
	{
		up = cellList_All & up;
		down = cellList_All & down;
		left = cellList_All & left;
		right = cellList_All & right;
		// 줄일 방법을 찾자.
		if (up < 1)
		{
			// Debug.Log(up);
			this.wall[1].SetActive(false);
		}
		if(right < 1)
		{
			// Debug.Log(right);
			this.wall[2].SetActive(false);
		}
		if (down < 1)
		{
			// Debug.Log(down);
			this.wall[3].SetActive(false);
		}
		if (left < 1)
		{
			// Debug.Log(left);
			this.wall[4].SetActive(false);
		}
	}

}
