using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField]
    private float pushForce = 100f; // De kracht waarmee de speler wordt geduwd
    [SerializeField]
    private bool pushLeft = true; // Richting van de duw, links of rechts

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerBody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerBody != null)
            {
                // Bereken de kracht richting (links of rechts)
                Vector2 forceDirection = pushLeft ? Vector2.left : Vector2.right;

                // Pas de kracht toe op de speler
                playerBody.AddForce(forceDirection * pushForce, ForceMode2D.Force);
            }
        }
    }
}