using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTorch : MonoBehaviour
{
    public bool enableRandom = true;
    private int rand = 0;
    void Start()
    {
        if (enableRandom)
        {
            rand = Random.Range(0, 2);
            if (rand == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
