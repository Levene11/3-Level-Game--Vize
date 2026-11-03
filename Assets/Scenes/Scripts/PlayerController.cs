using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;

    private Rigidbody rb;
    private bool isGrounded = false;

    private float horizontal;
    private float vertical;
    public int Score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

   
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        direction = direction.normalized;
        rb.AddForce(direction * moveSpeed);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        isGrounded = false;
        print("Zıpladın!");
    }

    private void UpdateScoreUi()
    {
        scoreText.text = "Score : " + Score.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            print("Yerdesin!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GreenBall"))
        {
            Score += 10;
            UpdateScoreUi();
            Debug.Log("GreenBall! Score" + Score);
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("RedBall"))
        {
            Score -= 5;
            UpdateScoreUi();
            Debug.Log("RedBall! Score" + Score);
            Destroy(other.gameObject);

        }

    }






}

