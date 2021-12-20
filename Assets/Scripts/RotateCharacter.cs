using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateCharacter : MonoBehaviour
{
    private Vector3 angles;
    private Ray rayMouse;
    private GameObject charaterSelect;

    public Camera camera;

    void Start()
    {
        angles = transform.rotation.eulerAngles;
    }

    void Update()
    {
        rayMouse = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(rayMouse, out hit, 100))
        {
            hit.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            if (Input.GetMouseButtonDown(0))
            {
                charaterSelect = hit.transform.gameObject;
                Debug.Log("The player has chosen : " + charaterSelect.name);
                Scenes.Load("Level1Scene", "Character", charaterSelect.name);
            }
        }
        else
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        angles.y += Time.deltaTime * 100;
        transform.rotation = Quaternion.Euler(angles);
    }
}