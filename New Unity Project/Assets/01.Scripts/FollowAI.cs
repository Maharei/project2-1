using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{
   // [SerializeField] private MeshCone meshcone;
    Transform t_target;

    NavMeshAgent m_enemy = null;
    [SerializeField] Transform[] m_tfWayPoints = null;

    int m_cout = 0;

    public bool seek=false;
    float time = 0;

    [SerializeField]
    private float Timer=3.0f;

    void MoveToNextWayPoint()
    {
        if (t_target == null)
        {
            if (m_enemy.velocity == Vector3.zero)
            {
                m_enemy.SetDestination(m_tfWayPoints[m_cout++].position);

                if (m_cout >= m_tfWayPoints.Length)
                {
                    m_cout = 0;
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_enemy = GetComponent<NavMeshAgent>();
        DisReset();
        InvokeRepeating("MoveToNextWayPoint", 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (t_target!=null)
        {
            m_enemy.SetDestination(t_target.position);
           // Debug.Log("추격");
        }
        if (seek==true)
        {

            time += Time.deltaTime;
            Debug.Log(time);

            if (time >= 3.0f)
            {
                RemoveTarget();
                time = 0f;
                seek = false;
                Debug.Log("");
            }
        }

    }


    void DisReset()
    {
        float dis1 = Vector3.Distance(transform.position, m_tfWayPoints[0].position);
        for (int i = 0; i < m_tfWayPoints.Length; i++)
        {
            float dis2 = Vector3.Distance(transform.position, m_tfWayPoints[i].position);
            if (dis1 > dis2)
            {
                dis1 = dis2;
                m_cout = i;
            }
        }
    }


    public void SetTarget(Transform target)
    {
        CancelInvoke();
        time = 0f;
        t_target = target;
    }
    public void RemoveTarget()
    {
        t_target = null;
        DisReset();
        m_enemy.SetDestination(transform.position);
        InvokeRepeating("MoveToNextWayPoint", 0f, 2f);
        //Debug.Log(t_target);
       Debug.Log("추격끝");
    }
}
