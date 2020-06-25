using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCone : MonoBehaviour
{
    [SerializeField] private FollowAI followAi;
    [SerializeField] private PlayerCtrl playerCtrl;


    private Mesh mesh;
    private Vector3 origin;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layerMask2;

    [SerializeField] float fov = 100;                        //각도
    [SerializeField] int rayCount = 50;                       //삼각형 개수
    [SerializeField] float viewDistance = 8f;                //거리

    Transform p_player;

    bool check=false;
    // Start is called before the first frame update
    void Start()
    {
        //새로운 Mesh입력
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        GetComponent<MeshRenderer>().material.shader = Shader.Find("Legacy Shaders/Diffuse");
        GetComponent<MeshRenderer>().material.color = new Color(211,248,0,0.2f);

        //followAi = FindObjectsOfType(typeof(FollowAI)) as FollowAI[];
        followAi = transform.parent.GetComponent<FollowAI>();

        p_player = GameObject.Find("Player").transform;
        playerCtrl = p_player.GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        Sight();
        MeshCreate();       
    }

    private void Sight()
    {
        //[] t_cols = Physics.OverlapSphere(transform.position, viewDistance, layerMask2);

        Transform t_tplayer = p_player;

        Vector3 t_direction = (t_tplayer.position - transform.position).normalized;
        float t_angle = Vector3.Angle(t_direction, transform.forward);
        if (t_angle < fov * 0.5f&&Vector3.Distance(t_tplayer.position,transform.position)<=viewDistance)
        {
            Debug.DrawRay(transform.position, t_direction * viewDistance, Color.red);
            if (Physics.Raycast(transform.position, t_direction, out RaycastHit t_hit, viewDistance))
            {
                if (t_hit.transform.name == "Player")
                {
                    //raycasthit가 플레이어일때 플레이어좌표추격

                    followAi.SetTarget(t_hit.transform);

                    check = true;
                }
                else
                {
                    if (check == true)
                    {
                        //    Debug.Log("1");
                        //ray가 플레이어가아닐때 
                        followAi.seek = true;
                        check = false;
                    }
                }
            }
        }
        else
        {
          //  Debug.Log("2");3
            if (check == true)
            {
                //ray가 플레이어가아닐때 
                followAi.seek = true;
                check = false;
            }

        }
    }

    private void MeshCreate()
    {
        Vector3 angleVec;

        float angle = 0;                           //각도

        float angleIncrease = fov / rayCount;   //각도 증가


        Vector3[] vertices = new Vector3[rayCount + 1 + 1]; //점개수
        Vector2[] uv = new Vector2[vertices.Length];    //uv
        int[] triangles = new int[rayCount * 3];          //삼각형

        vertices[0] = origin;
        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 dir = transform.InverseTransformDirection(transform.forward);

            var quate = Quaternion.Euler(0, -fov / 2-angle , 0);
            angleVec = quate * dir;

            Vector3 vertex;
            RaycastHit hit;
            Physics.Raycast(gameObject.transform.position, transform.TransformDirection(angleVec), out hit, viewDistance, layerMask);
            // Debug.DrawRay(gameObject.transform.position,transform.TransformDirection(angleVec) * 8f, Color.black, 1f);

            //레이캐스트 

            if (hit.collider == null)
            {
                vertex = origin + angleVec * viewDistance;
            }
            else
            {
                vertex = gameObject.transform.InverseTransformPoint(hit.point);
            }
            vertices[vertexIndex] = vertex;
            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex-1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;

            }
            vertexIndex++;
            angle -= angleIncrease;

        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
       // GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
