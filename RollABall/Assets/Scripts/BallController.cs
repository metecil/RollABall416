using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody sphereRigidbody;
    public float ballSpeed = 2f;
    public float jumpForce = 5f;

    public bool isGrounded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            Debug.Log("Calling the Start method");

    }

    // Update is called once per frame
    void Update()
    {
            if (sphereRigidbody == null)
        {
            Debug.LogError("sphereRigidbody is not assigned! Please assign it in the Inspector.");
            return;
        }
            Vector2 inputVector = Vector2.zero;

            if (Input.GetKey(KeyCode.W))
            {
                inputVector += Vector2.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                inputVector += Vector2.down;
            }
            if (Input.GetKey(KeyCode.D))
            {
                inputVector += Vector2.right;
            }
            if (Input.GetKey(KeyCode.A))
            {
                inputVector += Vector2.left;
            }
            
            // (same input checks as above)

            // Convert (x,y) to (x,0,z) in 3D
            Vector3 inputXZPlane = new(inputVector.x, 0, inputVector.y);

            // Apply force to the rigidbody
            sphereRigidbody.AddForce(inputXZPlane * ballSpeed);

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Add upward force (impulse mode gives that immediate "jump" feel)
            sphereRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        // If the collided object is tagged "Ground," mark isGrounded = true
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        // If we stop colliding with the Ground, mark isGrounded = false
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
