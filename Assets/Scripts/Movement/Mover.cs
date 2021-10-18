using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform target;

        Ray lastRay;

        // Update is called once per frame
        void Update()
        {


            UpdateAnimator();

            // debug ray on the scene 
            // Debug.DrawRay(lastRay.origin, lastRay.direction * 100);

            // GetComponent<UnityEngine.AI.NavMeshAgent>().destination = target.position;
        }



        // move to agent (player) to the Vector3 point. 
        public void MoveTo(Vector3 destination)
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().destination = destination;
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
}


