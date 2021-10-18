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
            InteractWithCombat();
            
            InteractWithMovement();

        }

        private void InteractWithCombat()
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
            }
        }

        private void InteractWithMovement()
        {
            // Mouse left click
            if (Input.GetMouseButton(0))
            {
                Ray ray = GetMouseRay();
                RaycastHit hit;
                bool hasHit = Physics.Raycast(ray, out hit);
                if (hasHit)
                {
                    Vector3 click_point = hit.point;
                    PlaceCursor(click_point);
                    MoveToCursor(click_point);
                }
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        // move the agent to mouse click position 
        private void MoveToCursor(Vector3 p)
        {
            GetComponent<Mover>().MoveTo(p);
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
