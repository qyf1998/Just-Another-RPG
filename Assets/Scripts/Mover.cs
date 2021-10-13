using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    Ray lastRay;

    // Update is called once per frame
    void Update()
    {
      
      // Mouse left click
      if (Input.GetMouseButton(0))
      {
        MoveToCursor();
      }

      UpdateAnimator();
      
      // debug ray on the scene 
      // Debug.DrawRay(lastRay.origin, lastRay.direction * 100);
      
      // GetComponent<UnityEngine.AI.NavMeshAgent>().destination = target.position;
    }

    // move the agent to mouse click position 
    private void MoveToCursor()
      {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
          GetComponent<UnityEngine.AI.NavMeshAgent>().destination = hit.point;
        }
      }

    
    private void UpdateAnimator()
    {
      // velocity of the character
      Vector3 velocity = GetComponent<UnityEngine.AI.NavMeshAgent>().velocity;
      // change global velocity to local velocity 
      Vector3 localVelocity = transform.InverseTransformDirection(velocity);
      float speed = localVelocity.z;    // z axis
      GetComponent<Animator>().SetFloat("forwardSpeed", speed);
      
    }


}

