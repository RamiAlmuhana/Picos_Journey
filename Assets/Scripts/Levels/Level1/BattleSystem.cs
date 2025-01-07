using System.Collections;
using TMPro;
using UnityEngine;


public class BattleSystem : MonoBehaviour
{
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;
    public PlayerMovement playerMovementScript;
    public Animator animator;
    
    void Start()
    {
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        playerMovementScript.canMove = false;
        
        dialogueText.text =  "Whats happening?";
        
        yield return new WaitForSeconds(3f);
        
        dialogueText.text =  "Why is the ice melting?";
        
        yield return new WaitForSeconds(3f);
        
        dialoguePanel.SetActive(false);
        
        playerMovementScript.canMove = true;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("run", false);
            animator.SetBool("walkWithAK", false);
            animator.SetBool("grounded", true);

            dialoguePanel.SetActive(true);
            playerMovementScript.canMove = false;
            
            dialogueText.text =  "Hi friend, do you know why the ice is melting?";
        
            yield return new WaitForSeconds(3f);
        
            dialogueText.text =  "Hi Pico, the ice is melting because of a person who is causing climate disruptions toward the north.";
            
            yield return new WaitForSeconds(6f);
            
            dialoguePanel.SetActive(false);
        
            playerMovementScript.canMove = true;
        }
    }
}
