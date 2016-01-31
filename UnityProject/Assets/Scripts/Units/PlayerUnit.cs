namespace Assets.Scripts.Units
{
    using UnityEngine;

    internal class PlayerUnit : Unit
    {
        [SerializeField]
        private Weapon _weapon;
        
        

        public void DoAttack()
        {
            this._weapon.Attack();
        }
    }
}
