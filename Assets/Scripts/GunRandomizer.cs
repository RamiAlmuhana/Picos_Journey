using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRandomizer : MonoBehaviour
{
    public List<GameObject> weaponPrefabs;
    public Transform spawnPoint;
    public float seconds = 10;
    private GameObject currentWeapon;
    private bool isRespawning = false;

    void Start()
    {
        SpawnRandomWeapon();
    }

    void SpawnRandomWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        int randomIndex = Random.Range(0, weaponPrefabs.Count);
        currentWeapon = Instantiate(weaponPrefabs[randomIndex], spawnPoint.position, spawnPoint.rotation);
        
        currentWeapon.tag = "GeneratedWeapon"; 
    
        isRespawning = false;
    }


    public void WeaponPickedUp()
    {
        if (!isRespawning && currentWeapon != null)
        {
            Destroy(currentWeapon);
            StartCoroutine(RespawnWeapon());
        }
    }

    IEnumerator RespawnWeapon()
    {
        isRespawning = true;
        yield return new WaitForSeconds(seconds);
        SpawnRandomWeapon();
    }
}