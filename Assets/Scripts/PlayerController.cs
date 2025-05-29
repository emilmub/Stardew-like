using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    [SerializeField] private Tilemap groundTilemap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Move()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movementVector = new Vector3(horizontalInput, verticalInput, 0);
        movementVector *= movementSpeed * Time.deltaTime;
        transform.position += movementVector;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = 0.0f;
            Vector3Int clickedCell = groundTilemap.WorldToCell(worldPoint);
            Debug.Log("Cell " + clickedCell);
        }


    }

    
}
