using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 0.2f;
    public float minInput = 0.25f;
    public float rotationSpeed = 8f;

    private Vector3 frontAxis = new Vector3(0, 1, 0);

    // Use this for initialization
    void Start()
    {

    }
    
    void FixedUpdate()
    {
        UpdateInput();
    }

    // updates input received, tramsformations are updated according to input
    void UpdateInput()
    {
        Vector3 velocity = new Vector3(0, 0, 0);
        float vaxis = 0f, haxis = 0f;
        if ((haxis = Input.GetAxis("Horizontal")) > minInput || haxis < -minInput)
        {
            velocity += new Vector3(movementSpeed * haxis, 0, 0);
        }

        if ((vaxis = Input.GetAxis("Vertical")) > minInput || vaxis < -minInput)
        {
            velocity += new Vector3(0, 0, movementSpeed * -vaxis);
        }

        if (velocity.magnitude != 0f)
        {
            var vn = velocity.normalized;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(vn, Vector3.up), rotationSpeed);
        }
        
        transform.position += velocity;

    }
}
