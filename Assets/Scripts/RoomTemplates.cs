using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
	//List of possibilities according to the available direction
	public GameObject[] topRooms;
	public GameObject[] rightRooms;
	public GameObject[] bottomRooms;
	public GameObject[] leftRooms;

	public GameObject closedRoom;
	public GameObject boss;

	//Save the Roms in the order they appear
	public List<GameObject> rooms;

	//Time to wait before spawning the boss
	public float waitTimeSpawnBoss = 4;
	private bool spawnedBoss;
	
	void Update()
	{
		if (waitTimeSpawnBoss <= 0 && spawnedBoss == false)
		{
			for (int i = 0; i < rooms.Count; i++)
			{
				if (i == rooms.Count - 1)
				{
					Instantiate(boss, rooms[i].transform.position, transform.rotation);
					spawnedBoss = true;
					Debug.Log("Spawn Boss!");
				}
			}
		}
		else
        {
			waitTimeSpawnBoss -= Time.deltaTime;
		}
	}
}