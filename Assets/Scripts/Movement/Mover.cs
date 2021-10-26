﻿using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;

        NavMeshAgent navMeshAgent;
        Ray lastRay;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
            // debug ray on the scene 
            // Debug.DrawRay(lastRay.origin, lastRay.direction * 100);
        }


        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        // move to agent (player) to the Vector3 point. 
        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
            // print("MoveTo Command: isStopped: " + navMeshAgent.isStopped);
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
            // print("Stop Command: isStopped: " + navMeshAgent.isStopped);
        }


        private void UpdateAnimator()
        {
            // velocity of the character
            Vector3 velocity = navMeshAgent.velocity;
            // change global velocity to local velocity 
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;    // z axis
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);

        }


    }
}


