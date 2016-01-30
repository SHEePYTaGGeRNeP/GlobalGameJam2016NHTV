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

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        inputPrefix = playerNumber.ToString() + "_";
    }

    void FixedUpdate()
    {
        UpdateMovement();
        UpdateInput();
    }

    void UpdateInput()
    {
        if (Input.GetAxis(inputPrefix + "Attack") > minInput && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            weapon.Attack();
        }

        if (Input.GetAxis(inputPrefix + "Shield") > minInput)
        {
            animator.SetBool("Shield", true);
        }
    }

    // updates input received, tramsformations are updated according to input
    void UpdateMovement()
    {
        var inputPrefix = playerNumber.ToString() + "_";

        Vector3 velocity = new Vector3(0, 0, 0);
        float vaxis = Input.GetAxis(inputPrefix + "Vertical");
        float haxis = Input.GetAxis(inputPrefix + "Horizontal");
        if (vaxis < this.minInput && vaxis >- this.minInput )
            vaxis = KeyboardManager.GetPlayerVertical(this.playerNumber);
        if (haxis < this.minInput && haxis >- this.minInput)
            haxis = KeyboardManager.GetPlayerHorizontal(this.playerNumber);
        
        if (haxis > minInput || haxis < -minInput)
        {
            velocity += new Vector3(movementSpeed * haxis, 0, 0);
        }

        if (vaxis > minInput || vaxis < -minInput)
        {
            velocity += new Vector3(0, 0, movementSpeed * -vaxis);
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

        var rb = GetComponent<Rigidbody>();
        rb.velocity = velocity;
        //transform.position += velocity;

    }
}
