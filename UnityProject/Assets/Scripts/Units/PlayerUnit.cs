namespace Assets.Scripts.Units
{
    using UnityEngine;

    internal class PlayerUnit : Unit
    {
        [SerializeField]
        private Weapon _weapon;
        

        public void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                this.DoAttack();
            }
        }

        public override void DoAttack()
        {
            this._weapon.Attack();
        }
    }
}
