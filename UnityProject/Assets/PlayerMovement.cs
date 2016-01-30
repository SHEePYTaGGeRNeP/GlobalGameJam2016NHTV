using UnityEngine;
using System.Collections;
using Assets.Scripts.Units;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerNumber
    {
        P1,
        P2
    }

    public float movementSpeed = 6.66f;
    public float minInput = 0.25f;
    public float rotationSpeed = 8f;
    public PlayerNumber playerNumber;
    public Weapon weapon;


    private Vector3 frontAxis = new Vector3(0, 1, 0);
    private string inputPrefix;

    // Use this for initialization
    void Start()
    {
        inputPrefix = playerNumber.ToString() + "_";
    }
    
    void FixedUpdate()
    {
        UpdateInput();
    }

    // updates input received, tramsformations are updated according to input
    void UpdateInput()
    {
        // update movement
        UpdateMovement();

        if (Input.GetAxis(inputPrefix + "Attack") != 0f)
        {
            weapon.Attack();
        }
    }

    void UpdateMovement()
    {

        Vector3 velocity = new Vector3(0, 0, 0);
        float vaxis = 0f, haxis = 0f;
        if ((haxis = Input.GetAxis(inputPrefix + "Horizontal")) > minInput || haxis < -minInput)
        {
            velocity += new Vector3(movementSpeed * haxis, 0, 0);
        }

        if ((vaxis = Input.GetAxis(inputPrefix + "Vertical")) > minInput || vaxis < -minInput)
        {
            velocity += new Vector3(0, 0, movementSpeed * -vaxis);
        }

        if (velocity.magnitude != 0f)
        {
            var vn = velocity.normalized;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(vn, Vector3.up), rotationSpeed);
        }

        var rb = GetComponent<Rigidbody>();
        rb.velocity = velocity;
        //transform.position += velocity;
    }
}
