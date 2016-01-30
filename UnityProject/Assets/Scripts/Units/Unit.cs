namespace Assets.Scripts.Units
{
    using UnityEngine;

    public class Unit : MonoBehaviour
    {
		public int MaxHealt; 
		public int CurrentHealth;


		public void Awake(){
			CurrentHealth = MaxHealt; 
		}

        public void TakeDamage(int damage)
        {
            this.CurrentHealth -= damage;
            if (this.CurrentHealth < 0)
                this.CurrentHealth = 0;
        }

        public virtual void DoAttack()
        {

        }

        void OnTriggerEnter(Collider collider){
			if (collider.tag == "DamageGiver") {
				TakeDamage (collider.GetComponent<DamageGiver> ().damageAmount); 
				Debug.Log (CurrentHealth);
			}
        }
    }
}
