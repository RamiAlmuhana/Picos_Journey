using UnityEngine;

public class IceFalling : MonoBehaviour
{
    public Animator iceAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            iceAnimator.SetTrigger("Fall");
        }
    }
}