namespace Assets.Scripts.Units
{
    using UnityEngine;
    public class Weapon : MonoBehaviour
    {
        enum AttackState
        {
            Neutral,
            Attacking,
            Returning
        }

        public float speed = 1.0f;
        public float distance = 1f;

        private Rigidbody currRigidbody;
        private float distanceCovered = 0f;
        private Vector3 startPos;
        private Vector3 velocity;
        private AttackState attackState = AttackState.Neutral;

        void Start()
        {
            currRigidbody = gameObject.GetComponent<Rigidbody>();
        }

        public void Attack()
        {
            if (attackState == AttackState.Neutral)
            {
                attackState = AttackState.Attacking;
                startPos = transform.position;
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
                        velocity = transform.forward * speed * Time.fixedDeltaTime;
                        transform.position += velocity;


                        //var distDiff = Vector3.SqrMagnitude(transform.position - startPos) - ;
                        //print(dist);
                        //if (dist < 1f)
                        //{
                        //    attackState = AttackState.Returning;
                        //}

                        //distanceCovered = Vector3.Distance(transform.localPosition, startPos);
                        /*
                        float step = speed * Time.fixedDeltaTime;
                        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red);
                        var target = startPos + transform.forward * distance;
                        */
                        //transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, step);

                        /*
                        Debug.DrawLine(startPos, target, Color.red);

                        velocity = (target - startPos) * step;
                        transform.position += velocity;

                        var dist = Vector3.SqrMagnitude(transform.position -target);
                        print(dist);
                        if (dist < 1f)
                        {
                            attackState = AttackState.Returning;
                        }
                        
                        */

                        break;
                    }
                case AttackState.Returning:
                    {
                        velocity = -transform.forward * speed * Time.fixedDeltaTime;
                        transform.position += velocity;
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

            //transform.localPosition += velocity;
        }

    }
}
