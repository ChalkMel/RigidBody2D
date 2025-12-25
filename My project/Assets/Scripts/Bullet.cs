using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletLifetime;
    [SerializeField] private float bulletImpulse;
    private void Awake()
    {
        Destroy(gameObject, bulletLifetime);
        GetComponent<Rigidbody2D>()
            .AddForce(transform.up * bulletImpulse, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D other)
    { 
        Destroy(gameObject); 
    }

}
