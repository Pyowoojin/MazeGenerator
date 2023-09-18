using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	[SerializeField] private int _maxSize = 20;

	[SerializeField] public Cell _tilePrefab;
	[SerializeField] private Camera _cam;
	[SerializeField] private GameObject _Cell_root;

	MazeGeneratorScript mg = new();
	public List<List<Cell>> cellList;

	private void Start()
	{
		_cam = FindObjectOfType<Camera>();
		_cam.orthographicSize = (_maxSize / 2) + 3;
		cellList = new();
		cellList = mg.GetCell(ref cellList, _maxSize);
		GenerateGrid();
	}
	void GenerateGrid()
	{
		for(int i = 0; i < _maxSize; i++)
		{
			for(int j = 0; j < _maxSize; j++)
			{
				var spawnedTile = Instantiate(_tilePrefab, new Vector3(j, -i), Quaternion.identity);
				// 입구 만들기
				if (i == 0 && j == 0)
				{
					spawnedTile.GridSetting(cellList[i][j].all - 8);
					spawnedTile.name = "Start";
				}
				// 출구 만들기
				else if (i == _maxSize / 2 && j == _maxSize - 1)
				{
					spawnedTile.GridSetting(cellList[i][j].all - 4);
					spawnedTile.name = "Goal";
				}
				// 일반 블록
				else
				{
					spawnedTile.GridSetting(cellList[i][j].all);
					spawnedTile.name = $"Tile {i} {j}";
				}
				spawnedTile.gameObject.transform.parent = _Cell_root.transform;
			}
		}

		_cam.transform.position = new Vector3((float)_maxSize / 2 - 0.5f, -(_maxSize / 2 - 0.5f), -10);
		MazeManager.Instance.GeneratePlayer();
	}
}
