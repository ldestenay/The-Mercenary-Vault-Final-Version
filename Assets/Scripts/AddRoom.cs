using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
	private RoomTemplates templates;
	void Start()
	{
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		//Add the room to the end of the list
		templates.rooms.Add(this.gameObject);
	}
}