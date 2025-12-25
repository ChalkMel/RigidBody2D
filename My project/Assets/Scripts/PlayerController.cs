using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [Header("Ground check")]
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundCheckMask;
    [Header("Attack")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint; 
    [Header("UI")]
    [SerializeField] private int startHP;
    [SerializeField] private TextMeshProUGUI hpText;

    private SpriteRenderer _sr;
    private Rigidbody2D _rb;

    private float _currInputX;
    private bool _isGrounded;
    private int _hp;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();

        _hp = startHP;
        DrawHP();
    }

    private void Update()
    {
        Move();
        Jump();
        CheckGround();
        Shoot();
    }

    private void Move()
    {
        _currInputX = Input.GetAxis("Horizontal");
        _rb.linearVelocity = new Vector2(_currInputX * speed, _rb.linearVelocity.y);
        if (_currInputX != 0)
        {
            _sr.flipX = _currInputX < 0;
            float rotationZ = _currInputX > 0 ? -90 : 90;
            firePoint.rotation = Quaternion.Euler(0,0,rotationZ);
        }
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    private void CheckGround() => 
        _isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundCheckMask);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }
    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
    public void GetDamage(int value)
    {
        _hp -= value;
        if (_hp <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DrawHP();
    }

    private void DrawHP() =>
        hpText.text = $"HP: {_hp}";
}
