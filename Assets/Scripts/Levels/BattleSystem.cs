using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "Level1")
        {
            playerMovementScript.canMove = false;
        
            dialoguePanel.SetActive(true);
            dialogueText.text = "Whats happening?";
        
            yield return StartCoroutine(WaitForSpaceInput());
        
            dialogueText.text = "Why is the ice melting?";
        
            yield return StartCoroutine(WaitForSpaceInput());
        
            dialoguePanel.SetActive(false);
            playerMovementScript.canMove = true;
        } else if (activeScene.name == "Level2")
        {
            dialoguePanel.SetActive(false);
            playerMovementScript.canMove = true;
        } else if (activeScene.name == "Level3")
        {
            dialoguePanel.SetActive(false);
            playerMovementScript.canMove = true;
        }

    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("run", false);
            animator.SetBool("walkWithAK", false);
            animator.SetBool("walkWithPistol", false);
            animator.SetBool("grounded", true);

            dialoguePanel.SetActive(true);
            playerMovementScript.canMove = false;
            
            dialogueText.text = "Hi friend, do you know why the ice is melting?";
            
            yield return StartCoroutine(WaitForSpaceInput());
        
            dialogueText.text = "Hi Pico, the ice is melting because of a person who is causing climate disruptions toward the north.";
            
            yield return StartCoroutine(WaitForSpaceInput());
            
            dialoguePanel.SetActive(false);
            playerMovementScript.canMove = true;
        }
    }

    private IEnumerator WaitForSpaceInput()
    {
        
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
    }
}
