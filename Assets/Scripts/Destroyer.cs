using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void Start()
    {
        Physics.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider>(), GetComponent<Collider>());
    }

    void OnCollisionEnter(Collision collision)
    {
        //Allows you to delete the CloseRooms only located on the four openings of the starting RoomMain
        //Debug.Log("Destroyer (OnCollisionEnter) " + collision.gameObject.tag);
        if (collision.gameObject.tag == "CloseRoom")
        {
            Destroy(collision.gameObject);
        }
    }
}
