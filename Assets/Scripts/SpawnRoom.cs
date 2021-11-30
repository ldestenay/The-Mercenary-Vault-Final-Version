using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    private RoomTemplates templates;
    private GameObject closedRoom;

    public int needRoomDirection;

    public bool spawned = false;
    private float waitTime = 4;

    private int rand;

    /*
    # = SpawnPoint
    ╔═══1═══╗
    ║       ║
    4       2
    ║       ║
    ╚═══3═══╝
    */

    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (spawned == false)
        {
            switch (needRoomDirection)
            {
                case 0:
                    break;
                case 1:
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                    break;
                case 2:
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                    break;
                case 3:
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                    break;
                case 4:
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                    break;
            }
            spawned = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SpawnPoint"))
        {
            if (collision.gameObject.GetComponent<SpawnRoom>().spawned == false && spawned == false)
            {
                closedRoom = GameObject.Find("CloseRoom");
                Instantiate(closedRoom, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}