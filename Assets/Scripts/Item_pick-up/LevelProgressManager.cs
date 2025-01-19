using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgressManager : MonoBehaviour
{
     public static LevelProgressManager Instance;

    [Header("UI Elements")]
    public TMP_Text itemCounterText;
    public TMP_Text enemyCounterText;

    [Header("Objects to Activate")]
    public GameObject[] objectsToActivate;

    [Header("Level Configuration")]
    public bool requireItems = true;
    public bool requireEnemies = true;

    private int totalItems;
    private int collectedItems;

    private int totalEnemies;
    private int defeatedEnemies;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DeactivateObjects();
        
        if (requireItems)
        {
            totalItems = GameObject.FindGameObjectsWithTag("Item").Length;
        }
        else
        {
            totalItems = 0;
        }
        
        if (requireEnemies)
        {
            totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        }
        else
        {
            totalEnemies = 0;
        }

        collectedItems = 0;
        defeatedEnemies = 0;

        UpdateCounters();
    }

    public void CollectItem()
    {
        if (!requireItems) return;

        collectedItems++;
        UpdateCounters();
        CheckProgress();
    }

    public void EnemyDefeated()
    {
        if (!requireEnemies) return;

        defeatedEnemies++;
        UpdateCounters();
        CheckProgress();
    }

    private void UpdateCounters()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        
        if (requireItems && itemCounterText != null)
        {
            if (activeScene.name == "Level1")
            {
                itemCounterText.text = $"Water Drops: {collectedItems}/{totalItems}";
            }
            else if (activeScene.name == "Level2")
            {
                itemCounterText.text = $"Seeds: {collectedItems}/{totalItems}";
            }
            else if (activeScene.name == "Level3")
            {
                itemCounterText.text = $"Garbage: {collectedItems}/{totalItems}";
            }
            else if (activeScene.name == "Level4")
            {
                itemCounterText.text = $"Plastic: {collectedItems}/{totalItems}";
            }
        }
        else if (itemCounterText != null)
        {
            itemCounterText.text = "";
        }
        
        if (requireEnemies && enemyCounterText != null)
        {
            enemyCounterText.text = $"Enemies: {defeatedEnemies}/{totalEnemies}";
        }
        else if (enemyCounterText != null)
        {
            enemyCounterText.text = "";
        }
    }

    private void CheckProgress()
    {
        bool itemsComplete = !requireItems || collectedItems >= totalItems;
        bool enemiesComplete = !requireEnemies || defeatedEnemies >= totalEnemies;

        if (itemsComplete && enemiesComplete)
        {
            ActivateObjects();
        }
    }

    private void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    private void DeactivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
