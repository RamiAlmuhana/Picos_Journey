using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public TMP_Text enemyCounterText;
    public GameObject levelCompleteTrigger;

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
        if (levelCompleteTrigger != null)
        {
            levelCompleteTrigger.SetActive(false);
        }
        
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        defeatedEnemies = 0;
        
        UpdateEnemyCounter();
    }

    public void EnemyDefeated()
    {
        defeatedEnemies++;
        UpdateEnemyCounter();
        
        if (defeatedEnemies >= totalEnemies)
        {
            ActivateLevelTrigger();
        }
    }

    private void UpdateEnemyCounter()
    {
        enemyCounterText.text = $"Enemies: {defeatedEnemies}/{totalEnemies}";
    }

    private void ActivateLevelTrigger()
    {
        if (levelCompleteTrigger != null)
        {
            levelCompleteTrigger.SetActive(true);
        }
    }
}