using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public TMP_Text itemCounterText;

    private int collectedItems;

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
        collectedItems = 0;
        UpdateItemCounter();
    }

    public void CollectItem()
    {
        collectedItems++;
        UpdateItemCounter();
    }

    private void UpdateItemCounter()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "Level1")
        {
            itemCounterText.text = $"Water Drops: {collectedItems}";
            
        } else if (activeScene.name == "Level2")
        {
            itemCounterText.text = $"Seed: {collectedItems}";
        } else if (activeScene.name == "Level3")
        {
            itemCounterText.text = "";
        } else if (activeScene.name == "Level4")
        {
            itemCounterText.text = $"Garbage: {collectedItems}";
        }
        
    }
}