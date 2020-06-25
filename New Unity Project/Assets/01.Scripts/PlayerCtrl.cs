using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Transform tr;

    public float speed;

   // public bool seek = false;
    // Start is called before the first frame update
    void Start()
    {
        tr = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDir = (Vector3.forward * v + Vector3.right * h);
        tr.Translate(moveDir.normalized * speed * Time.deltaTime, Space.Self);
    }
}
