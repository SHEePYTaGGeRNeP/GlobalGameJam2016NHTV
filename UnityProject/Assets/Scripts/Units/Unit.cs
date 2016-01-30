namespace Assets.Scripts.Units
{
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    internal class Unit : MonoBehaviour
    {
        public int MaxHealth { get; protected set; }

        public int CurrentHealth { get; protected set; }

        public int Damage { get; protected set; }


        public void TakeDamage(int damage)
        {
            this.CurrentHealth -= damage;
            if (this.CurrentHealth < 0)
                this.CurrentHealth = 0;
        }

        public virtual void DoAttack()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Oh no i got hit!");
            
        }
    }
}
