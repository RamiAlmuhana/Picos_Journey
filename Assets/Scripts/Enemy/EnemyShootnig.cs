using UnityEngine;


public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPosition;
    private GameObject player;
    [SerializeField]
    private float enemyRange;
    
    private float timer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
        
        float distance = Vector2.Distance(transform.position, player.transform.position);
        // Debug.Log(distance);
        if (distance < enemyRange)
        {
            FacePlayer();
            
            timer += Time.deltaTime;
            
            if (timer > 1)
            {
                timer = 0;
                Shoot();
            }
        }
    }
    
    private void FacePlayer()
    {
        // Bereken de richting naar de speler
        Vector3 direction = player.transform.position - transform.position;

        // Als de speler aan de rechterkant is, kijk naar rechts, anders naar links
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Normale schaal voor rechts
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // Gespiegeld voor links
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, bulletPosition.position, Quaternion.identity);
    }
}