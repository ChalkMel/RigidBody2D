using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player))
        { 
            winScreen.SetActive(true);
        }
    }
}
