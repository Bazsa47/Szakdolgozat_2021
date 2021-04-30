using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent agent;
    public EnemyClass ec;
    private float turnSmoothVelocity;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (ec.Target != null)
        {
            RotateAgent();
            agent.SetDestination(ec.Target.position - (ec.Target.position - gameObject.transform.position).normalized * 1f);
        }
       
    }

    private void RotateAgent()
    {
        if (Vector3.Distance(transform.position,agent.steeringTarget) > 1.01f)
        {
            Debug.Log(Vector3.Distance(transform.position, agent.steeringTarget));
            Vector3 relativePos = agent.steeringTarget - transform.position;
            relativePos.y = 0.0f;
            float targetAngle = Mathf.Atan2(relativePos.x, relativePos.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Quaternion rotation = Quaternion.LookRotation(relativePos);
        }
        

    }
}
