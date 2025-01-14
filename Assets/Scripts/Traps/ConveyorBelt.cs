using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField]
    private float pushForce = 100f;
    [SerializeField]
    private bool pushLeft = true;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerBody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerBody != null)
            {
                Vector2 forceDirection = pushLeft ? Vector2.left : Vector2.right;

                playerBody.AddForce(forceDirection * pushForce, ForceMode2D.Force);
            }
        }
    }
}