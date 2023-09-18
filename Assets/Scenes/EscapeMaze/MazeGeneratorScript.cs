using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGeneratorScript : MonoBehaviour
{
    readonly int[] dx = new int[] { -1, 0, 1, 0 };
    readonly int[] dy = new int[] { 0, 1, 0, -1 };

    [SerializeField]
    private int maxSize;
    // Start is called before the first frame update

    struct CustomPair
    {
        public CustomPair(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x;
        public int y;
    }

    public List<List<Cell>> GetCell(ref List<List<Cell>> cellList, int maxSize)
    {
        for (int x = 0; x < maxSize; x++)
        {
            cellList.Add(new List<Cell>());
            for (int y = 0; y < maxSize; y++)
            {
                cellList[x].Add(new Cell());
            }
        }

        Backtracking(ref cellList, 0, 0);

        return cellList;
    }

    bool IsValid(int x, int y, ref List<List<Cell>> cells)
	{
        return (x > -1 && y > - 1 && cells.Count > x && cells[0].Count > y);
	}

    void Backtracking(ref List<List<Cell>> cellList, int startX, int startY)
	{
        Stack<CustomPair> stk = new();
        CustomPair pair;

        pair.x = startX;
        pair.y = startY;
        
        stk.Push(pair);

        cellList[startX][startY].visited = true;

        int nowX = startX, nowY = startY;

        while (stk.Count > 0)
        {
            List<int> notVisitedList = new();

            for (int k = 0; k < 4; k++)
            {
                int nextX = nowX + dx[k];
                int nextY = nowY + dy[k];
                if (IsValid(nextX, nextY, ref cellList))
                {
                    if (cellList[nextX][nextY].visited == false)
                    {
                        notVisitedList.Add(k);
                    }
                }
            }

            if (notVisitedList.Count > 0)
            {
                int notVisitedDir = RandomNum(ref notVisitedList);
                int nextX = nowX + dx[notVisitedDir], nextY = nowY + dy[notVisitedDir];

                pair.x = nextX;
                pair.y = nextY;

                stk.Push(pair);

                cellList[nextX][nextY].visited = true;

                cellList[nowX][nowY].all -= ReturnHexNum(notVisitedDir);
                cellList[nextX][nextY].all -= ReturnHexNum((notVisitedDir + 2) % 4);

                nowX = pair.x;
                nowY = pair.y;

            }
            else
            {
                pair = stk.Peek(); stk.Pop();
                nowX = pair.x; nowY = pair.y;
            }

            notVisitedList.Clear();
        }
	}

    int ReturnHexNum(int dir)
	{
        if (dir == 0)
        {
            return 8;
        }
        else if (dir == 1)
        {
            return 4;
        }
        else if(dir == 2)
		{
            return 2;
		}
		else
		{
            return 1;
		}
	}

    int RandomNum(ref List <int> arr)
	{
        var rnd = new System.Random();
        arr = arr.OrderBy(item => rnd.Next()).ToList();
        return arr[0];
	}
}
