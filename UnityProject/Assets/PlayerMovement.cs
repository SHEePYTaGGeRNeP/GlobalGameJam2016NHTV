using System;

using UnityEngine;
using System.Collections;

using Assets.Scripts.Units;

using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerNumber
    {
        P1,
        P2
    }

    public float movementSpeed = 0.2f;
    public float minInput = 0.25f;
    public float rotationSpeed = 8f;
    public PlayerNumber playerNumber;
    public Weapon weapon;

    private Vector3 frontAxis = new Vector3(0, 1, 0);
    private Animator animator;
    string inputPrefix;
    private Unit unit;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        inputPrefix = playerNumber.ToString() + "_";
        unit = GetComponent<Unit>();
    }

    void FixedUpdate()
    {
        if (GetComponent<Unit>().dead)
        {
            return;
        }

        //print("Updating input: " + gameObject.name);

        UpdateMovement();
        UpdateInput();
    }

    void UpdateInput()
    {
        bool shielding = animator.GetBool("Shield");
        bool attacking = animator.GetBool("Attacking");
        if (!attacking && Input.GetAxis(inputPrefix + "Attack") > minInput || KeyboardManager.GetPlayerAttack(playerNumber))
        {
            if (unit.currentStamina >= unit.StaminaCostAttack)
            {
                weapon.Attack();
                unit.useStamina(unit.StaminaCostAttack);
            }
        }

        if ((Input.GetAxis(inputPrefix + "Shield") > minInput || KeyboardManager.GetPlayerBlock(playerNumber)) && !shielding)
        {
            if (unit.currentStamina >= unit.StaminaCostShield)
            {
                Debug.Log("Shieldtrigger");
                animator.SetBool("Shield", true);
                animator.SetTrigger("ShieldTrigger");
                if (Time.timeSinceLevelLoad - unit.ShieldUpStartTime > unit.StaminaShieldTick)
                {
                    unit.useStamina(unit.StaminaCostShield);
                    unit.ShieldUpStartTime = Time.timeSinceLevelLoad;
                }
            }
        }
        else if (shielding && (Input.GetAxis(inputPrefix + "Shield") < minInput))
        {
            animator.SetBool("Shield", false);
        }
        else if (shielding)
        {
            if (unit.currentStamina < unit.StaminaCostShield)
            {
                animator.SetBool("Shield", false);
            }
            if (Time.timeSinceLevelLoad - unit.ShieldUpStartTime > unit.StaminaShieldTick)
            {
                unit.useStamina(unit.StaminaCostShield);
                unit.ShieldUpStartTime = Time.timeSinceLevelLoad;
            }
        }
    }

    // updates input received, tramsformations are updated according to input
    void UpdateMovement()
    {
        var inputPrefix = playerNumber.ToString() + "_";

        Vector3 velocity = new Vector3(0, 0, 0);
        float vaxis = Input.GetAxis(inputPrefix + "Vertical");
        float haxis = Input.GetAxis(inputPrefix + "Horizontal");
        if (vaxis < this.minInput && vaxis > -this.minInput)
            vaxis = KeyboardManager.GetPlayerVertical(this.playerNumber);
        if (haxis < this.minInput && haxis > -this.minInput)
            haxis = KeyboardManager.GetPlayerHorizontal(this.playerNumber);

        float reducedMovementSpeed = 1f;
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
            reducedMovementSpeed = 0.3f;



        if (haxis > minInput || haxis < -minInput)
        {
            velocity += new Vector3(movementSpeed * reducedMovementSpeed * haxis, 0, 0);
        }

        if (vaxis > minInput || vaxis < -minInput)
        {
            velocity += new Vector3(0, 0, movementSpeed * reducedMovementSpeed * -vaxis);
        }

        if (velocity.magnitude != 0f)
        {
            var vn = velocity.normalized;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(-vn, Vector3.up), rotationSpeed);

            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }


        if (!animator.GetBool("Shield"))
        {
            var rb = GetComponent<Rigidbody>();
            rb.velocity = velocity;
            //transform.position += velocity;
        }
    }
}
