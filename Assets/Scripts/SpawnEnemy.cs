using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public GameObject[] patternEnemyList;
    public GameObject[] lootList;
    private readonly int waitTime = 1;

    private int rand;

    void Start()
    {
        rand = Random.Range(0, 4);

        switch (rand)
        {
            case 0:
                Instantiate(lootList[rand], transform.position, lootList[rand].transform.rotation);
                break;
            default:
                rand = Random.Range(0, patternEnemyList.Length);
                Instantiate(patternEnemyList[rand], transform.position, patternEnemyList[rand].transform.rotation);
                break;
        }
        Destroy(gameObject, waitTime);
    }
}