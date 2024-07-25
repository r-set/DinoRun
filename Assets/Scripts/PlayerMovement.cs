using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float _jumpForce = 10.0f;

    private Rigidbody _rigidbody;
    [HideInInspector] public Animator animator;
    private bool _isGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HorizontalMove();

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }
    }

    private void HorizontalMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, 0.0f) * _moveSpeed * Time.deltaTime;

        transform.position += movement;
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpForce, _rigidbody.velocity.z);
            animator.SetBool("isJumping", true);
            _isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);
            _isGrounded = true;
        }
    }
}