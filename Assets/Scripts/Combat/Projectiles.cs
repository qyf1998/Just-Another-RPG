using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;


namespace RPG.Combat
{
    public class Projectiles : MonoBehaviour
    {

        [SerializeField] float speed = 1;
        [SerializeField] bool isHoming = true;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 15f;
        [SerializeField] GameObject[] destoryOnHit = null;
        [SerializeField] float lifeAfterImpact = 2f;

        Health target = null;
        float damage = 0;


        void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;

            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }


        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            if (target.IsDead()) return;
            target.TakeDamage(damage);

            speed = 0;

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }


            foreach (GameObject toDestory in destoryOnHit)
            {
                Destroy(toDestory);
            }
            Destroy(gameObject, lifeAfterImpact);
        }

    }

}
