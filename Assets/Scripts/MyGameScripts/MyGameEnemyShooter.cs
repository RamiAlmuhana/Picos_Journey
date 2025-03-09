using System.Collections;
using UnityEngine;

public class MyGameEnemyShooter : MonoBehaviour
{
    // Prefab van de kogel die de enemy gaat schieten
    public GameObject bulletPrefab;
    // Het punt waar de kogel verschijnt
    public Transform shootPoint;
    // Tijd tussen de schoten (in seconden)
    public float shootInterval = 1f;
    // Snelheid van de kogel
    public float bulletSpeed = 10f;

    private void Start()
    {
        // Start de coroutine die continu schiet
        StartCoroutine(ShootContinuously());
    }

    private IEnumerator ShootContinuously()
    {
        // Blijf voor altijd schieten
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootInterval);
        }
    }

    private void Shoot()
    {
        // Maak de kogel aan op de positie van shootPoint
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        // Zorg dat de kogel naar links beweegt
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.left * bulletSpeed;
        }
    }
}
