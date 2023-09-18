using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public GameObject player;

    private static MazeManager instance = null;

	private void Awake()
	{
		if(instance == null)
		{
            instance = this;
            DontDestroyOnLoad(gameObject);
		}
		else
		{
            Destroy(gameObject);
		}
	}

    public static MazeManager Instance
	{
		get
		{
			if (null == instance)
				return null;
			return instance;
		}
	}
	
	public void GeneratePlayer()
	{
		GameObject start = GameObject.Find("Start");
		Vector3 generateLocation = start.transform.position;
		// start.transform.position
		Instantiate(player, generateLocation, Quaternion.identity);
	}

}
