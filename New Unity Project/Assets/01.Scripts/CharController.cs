using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 4f;

    Vector3 forward, right;

    void Start()
    {
        forward = Camera.main.transform.forward;
        
        forward.y = 0;
        forward.Normalize();
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    
    void Update()
    {
        if (Input.anyKey)
            Move();
    }

    void Move()
    {
    //    Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        Vector3 upMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;
    }
}
