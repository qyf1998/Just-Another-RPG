using System;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        public GameObject ClickIndicator;
        void Update()
        {

            if (InteractWithCombat()) return;
            
            if (InteractWithMovement()) return;

            print("nothing to do");

        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            // Mouse left click
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 click_point = hit.point;
                    PlaceCursor(click_point);
                    MoveToCursor(click_point);
                }
                return true;    
            }
            
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        // move the agent to mouse click position 
        private void MoveToCursor(Vector3 p)
        {
            GetComponent<Mover>().StartMoveAction(p);
            // print("player is moving to: " + p);
        }

        // place an indicator to show where the mouse is clicked. 
        private void PlaceCursor(Vector3 p)
        {
            ClickIndicator.transform.position = p;
            // print("indicator is placed at: " + p); 
        }


    }
}
