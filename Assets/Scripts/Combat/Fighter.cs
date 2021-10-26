using UnityEngine;
using RPG.Movement;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Combat
{


    public class Fighter : MonoBehaviour, IAction
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
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }

        }

        private void AttackBehaviour()
        {
            GetComponent<Animator>().SetTrigger("attack");
        }

        
        private bool isInRange()
        {
            float v = Vector3.Distance(transform.position, target.position);
            bool isInRange = v < weaponRange;
            return isInRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }


        // animation
        void Hit()
        {

        }


    }
}