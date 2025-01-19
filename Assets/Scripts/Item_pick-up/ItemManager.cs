using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public TMP_Text itemCounterText;
    public GameObject[] objectsToActivate;

    private int totalItems;
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
        DeactivateObjects();
        
        totalItems = GameObject.FindGameObjectsWithTag("Item").Length;
        collectedItems = 0;

        UpdateItemCounter();
    }

    public void CollectItem()
    {
        collectedItems++;
        UpdateItemCounter();
        
        if (collectedItems >= totalItems)
        {
            ActivateObjects();
        }
    }

    private void UpdateItemCounter()
    {
        Scene activeScene = SceneManager.GetActiveScene();
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
