namespace Assets.Scripts.Units
{
    using Assets.Scripts.Helpers;

    using UnityEngine;

    public class Unit : MonoBehaviour
    {
        //Health
        public float maxHealt;
        public float currentHealth;
        public StatusBarFunction healthBarRef;
        public bool dead = false;
        //Stamina
        public float maxStamina;
        public float currentStamina;
        public StatusBarFunction StaminaBarRef;
        public float staminaRefillTimer = 2;
        public float staminaRechargeSpeed = 2;
        float currentrefillTimer;

        [HideInInspector]
        public float StaminaCostAttack = 15;
        [HideInInspector]
        public float StaminaCostDodge = 75;
        public float StaminaCostShield = 2;
        public float StaminaShieldTick = 0.2f;
        [HideInInspector]
        public float StaminaCostQTE = 50;
        [HideInInspector]
        public float StaminaRegenRate = 30;

        public float ShieldUpStartTime;

        //Reference
        public Feedback feedbackRef;

        //Invincible
        public float damageBlinkSpeed = 0.3f;
        public float invincibleTime = 1f;
        private bool invincible = false;

        [SerializeField]
        private ParticleSystem _bloodParticleSystemPrefab;
        [SerializeField]
        private GameObject _rubblePrefab;
        private Animator animator = null;

        public void Awake()
        {
            currentHealth = maxHealt;

            if (StaminaBarRef != null)
                currentStamina = (maxStamina / 2);
        }

        public void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void Update()
        {
            //print("current stamina: " + currentStamina);
            if (this.healthBarRef != null)
                healthBarRef.statusAmount = currentHealth / maxHealt;
            if (StaminaBarRef != null)
                StaminaBarRef.statusAmount = currentStamina / maxStamina;


            if (currentStamina < maxStamina && currentrefillTimer < staminaRefillTimer)
            {
                currentrefillTimer += Time.deltaTime;
            }

            if (currentStamina < maxStamina && currentrefillTimer >= staminaRefillTimer)
            {
                currentStamina += (staminaRechargeSpeed * Time.deltaTime);
            }

        }

        public void TakeDamage(int damage, bool addBlood)
        {
            if (!invincible)
            {
                if (this.currentHealth <= 0) return;

                if (this._bloodParticleSystemPrefab != null && addBlood)
                {
                    Instantiate(this._bloodParticleSystemPrefab, new Vector3(
                        this.transform.position.x, this.transform.position.y + 2, this.transform.position.z),
                        Quaternion.Euler(this.transform.eulerAngles));
                }

                this.currentHealth -= damage;
                if (this.currentHealth <= 0)
                {
                    animator.SetBool("Is dead", true);
                    animator.SetTrigger("Death");
                    dead = true;
                    this.currentHealth = 0;
                    animator.SetBool("Walking", false);
                    animator.SetBool("Shield", false);
                }
            }
        }
        public void TakeQTEDamage(int damage)
        {
            this.GetComponent<Animator>().SetTrigger("HitHeavy");
            this.TakeDamage(damage, false);
            Instantiate(this._rubblePrefab, transform.position, Quaternion.Euler(-90, 0, 0));
            feedbackRef.PlayImpactsF();
        }
        void OnCollisionEnter(Collision collision)
        {
            this.CollidedWithDamageGiver(collision.collider);
        }
        void OnTriggerEnter(Collider collider)
        {
            this.CollidedWithDamageGiver(collider);
        }

        private void CollidedWithDamageGiver(Collider col)
        {
            if (dead)
            {
                print(gameObject.name + " is dead");
                return;
            }
            if (col.tag == "DamageGiver")
            {
                var damageGiver = col.GetComponent<DamageGiver>();
                if (damageGiver == null) return;
                if (damageGiver.attacking)
                {
                    if (transform.name == "Boss" && col.transform.name == "DamageGiverSphere")
                        return;
                    if (animator != null)
                    {
                        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Shield stay up"))
                        {
                            LogHelper.Log(typeof(Unit), "Blocked damage");
                            return;
                        }
                    }
                    TakeDamage(damageGiver.damageAmount, true);

                    if (currentHealth > 0)
                    {
                        invincible = true;
                        Invoke("SetInvincibleFalse", invincibleTime);
                        feedbackRef.StartInvincibilityMaterialF(invincibleTime);
                    }
                }
            }
        }
        public void SetInvincibleFalse()
        {
            invincible = false;
        }

        public void useStamina(float staminaReduction)
        {
            currentStamina -= staminaReduction;
            currentrefillTimer = 0;
        }
    }
}
