using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 move;
    private Rigidbody rb;
    [SerializeField] private float movementSpeed = 8f;
    private RaycastHit2D hit;
// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(move.x, 0, move.y) * (movementSpeed * Time.deltaTime) );
        rb.angularVelocity = Vector2.zero;
        
    }
}
