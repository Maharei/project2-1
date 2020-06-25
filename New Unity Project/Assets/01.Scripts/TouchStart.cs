using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TouchStart : MonoBehaviour
{
    public Transform[] door;
    public Text start;

    bool check = false;
    Camera camera;
    float time=1;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            time -= Time.deltaTime;
            if (time>=0)
            {
                door[0].transform.position -= Vector3.right*0.15f;
                door[1].transform.position += Vector3.right*0.15f;
            }

            if (Camera.main.fieldOfView >= 14)
            {
                camera.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 11, Time.deltaTime * 2);
            }
            else
            {
                check = false;
            }
        }

    }
    
    public void Onclick()
    {
        start.text = "";
        check = true;
        
    }
}
