using UnityEngine;
using RPG.Movement;
using UnityEngine.AI;

namespace RPG.Combat
{


    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;




        private void Update()
        {   
            if (target == null ) return; 
            if ( !isInRange()) 
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
            }
        }

        private bool isInRange()
        {
            float v = Vector3.Distance(transform.position, target.position);
            bool isInRange = v < weaponRange;
            return isInRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }

        public void cancel()
        {
            target = null;
        }


    }
}