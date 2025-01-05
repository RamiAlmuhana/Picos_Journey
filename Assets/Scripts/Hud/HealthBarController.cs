using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; 
    public Sprite[] healthSprites;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        int spriteIndex = Mathf.Clamp(Mathf.FloorToInt((currentHealth / maxHealth) * (healthSprites.Length - 1)), 0, healthSprites.Length - 1);
        spriteRenderer.sprite = healthSprites[spriteIndex];
    }
}