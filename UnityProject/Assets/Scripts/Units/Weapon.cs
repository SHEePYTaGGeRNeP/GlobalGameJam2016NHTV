namespace Assets.Scripts.Units
{
    using UnityEngine;
    public class Weapon : MonoBehaviour
    {
        enum AttackState
        {
            Neutral,
            Attacking
        }

        public float speed = 1.0f;
        public float distance = 1f;

        private Rigidbody currRigidbody;
        private float distanceCovered = 0f;
        private Vector3 startPos;
        private Vector3 velocity;
        private AttackState attackState = AttackState.Neutral;
        private Animator animator;
        private Collider boxCollider;

        void Start()
        {
            currRigidbody = gameObject.GetComponent<Rigidbody>();
            animator = GetComponent<Collider>().transform.root.GetComponent<Animator>();
            boxCollider = GetComponent<Collider>();
        }

        public void Attack()
        {
            if (attackState == AttackState.Neutral)
            {
                attackState = AttackState.Attacking;
                startPos = transform.position;

                animator.SetTrigger("Attacking");
            }
        }

        private void ReturnToOriginal()
        {
            //currRigidbody.velocity = transform.forward * -speed;
        }

        void FixedUpdate()
        {
            switch (attackState)
            {
                case AttackState.Attacking:
                    {
                        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
                        {
                            attackState = AttackState.Neutral;
                        }

                        break;
                    }
                case AttackState.Neutral:
                    {
                        break;
                    }
                default:
                    {
                        print("Switch state not defined");
                        break;
                    }
            }
            
        }

    }
}
