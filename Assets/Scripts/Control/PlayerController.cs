using System;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;
using RPG.Core;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] GameObject ClickIndicator;
        GameObject ActiveIndicator;
        Health health;

        // different types of cursor 
        enum CursorType
        {
            None,
            Movement,
            Combat,
            UI
        }

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;

        void Start()
        {
            health = GetComponent<Health>();
        }
        void Update()
        {
            if (InteractWithUI()) return;
            if (health.IsDead()) 
            {
                SetCursor(CursorType.None);
                return;
            }
            if (InteractWithCombat()) return;
            
            if (InteractWithMovement()) return;

            // none of the above, set default cursor
            SetCursor(CursorType.None);
            //print("nothing to do");

        }

        private bool InteractWithUI()
        {
            if  (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        // combat 
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                // check if player can attack target 
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) 
                {
                    continue;
                }
            
                // left click  is combat 
                if (Input.GetMouseButton(0))
                {
                    if (ActiveIndicator) Destroy(ActiveIndicator);
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                SetCursor(CursorType.Combat);
                return true;
            }
            return false;
        }


        //set the cursor to CursorType
        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }
        

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
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
                SetCursor(CursorType.Movement);
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
            GetComponent<Mover>().StartMoveAction(p, 1f);
            // print("player is moving to: " + p);
        }

        // place an indicator to show where the mouse is clicked. 
        private void PlaceCursor(Vector3 p)
        {   
            if (ActiveIndicator) Destroy(ActiveIndicator);
            ActiveIndicator = Instantiate(ClickIndicator, p, Quaternion.identity) as GameObject;
            // print("indicator is placed at: " + p); 
        }


        //destory indicator if player step on it
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "indicator")
            {
                // Debug.Log(other.name);

                Destroy(ActiveIndicator);
            }
        }


    }
}
