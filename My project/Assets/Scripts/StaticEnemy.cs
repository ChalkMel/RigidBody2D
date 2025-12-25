using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    [SerializeField] private int damage;

    private bool _isAlive;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isAlive = true;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            if (_isAlive)
                player.GetDamage(damage);
        }
        else if (other.gameObject.TryGetComponent(out Bullet bullet))
        {
            if (_isAlive)
            {
                _isAlive = false;
                _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        
    }
}
