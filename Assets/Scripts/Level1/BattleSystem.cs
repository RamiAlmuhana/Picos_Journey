using System.Collections;
using TMPro;
using UnityEngine;


public class BattleSystem : MonoBehaviour
{
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;
    public PlayerMovement playerMovementScript;
    
    void Start()
    {
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        playerMovementScript.canMove = false;
        
        dialogueText.text =  "Whats happening?";
        
        yield return new WaitForSeconds(3f);
        
        dialogueText.text =  "Why is the ice smelting?";
        
        yield return new WaitForSeconds(3f);
        
        dialoguePanel.SetActive(false);
        
        playerMovementScript.canMove = true;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialoguePanel.SetActive(true);
            playerMovementScript.canMove = false;
            Debug.Log("Player entered");
            dialogueText.text =  "Whats happening?";
        
            yield return new WaitForSeconds(3f);
        
            dialogueText.text =  "Why is the ice smelting?";
            
            yield return new WaitForSeconds(3f);
            
            dialoguePanel.SetActive(false);
        
            playerMovementScript.canMove = true;
        }
    }
}
