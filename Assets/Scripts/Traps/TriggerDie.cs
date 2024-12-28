using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDie : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("Level1");
        }
    }
}
