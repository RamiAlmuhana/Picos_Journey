using UnityEngine;


public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPosition;
    private GameObject player;
    [SerializeField] private float enemyRange;
    [SerializeField] private float fireRate = 1f;

    private Animator anim;
    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.isDialogueActive)
        {
            anim.SetBool("isShooting", false);
            return;
        }
        
        float distance = Vector2.Distance(transform.position, player.transform.position);
        
        if (distance < enemyRange)
        {
            FacePlayer();
            anim.SetBool("isShooting", true);
            
            timer += Time.deltaTime;

            if (timer > fireRate)
            {
                timer = 0;
                Shoot();
            }
        }
        else
        {
            anim.SetBool("isShooting", false);
        }
    }

    private void FacePlayer()
    {
        Vector3 direction = player.transform.position - transform.position;

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, bulletPosition.position, Quaternion.identity);
    }
}