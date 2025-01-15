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

    private bool skipDialogue;

    void Start()
    {
        StartCoroutine(SetupBattle());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            skipDialogue = true;
    }

    private IEnumerator SetupBattle()
    {
        playerMovementScript.canMove = false;
        dialoguePanel.SetActive(true);
        
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "Level1")
        {
            yield return ShowDialogue("What's happening?", 3f);
            yield return ShowDialogue("Why is the ice melting?", 3f);
            
        } else if (activeScene.name == "Level2")
        {
            dialoguePanel.SetActive(false);
            playerMovementScript.canMove = true;
        } else if (activeScene.name == "Level3")
        {
            yield return ShowDialogue("Why is there garbage everywhere?", 3f);
            yield return ShowDialogue("I have to prevent the drones from polluting the planet!", 3f);
        }
        
        dialoguePanel.SetActive(false);
        playerMovementScript.canMove = true;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueTrigger trigger = GetComponent<DialogueTrigger>();

            if (trigger != null)
            {
                animator.SetBool("run", false);
                animator.SetBool("walkWithAK", false);
                animator.SetBool("grounded", true);

                dialoguePanel.SetActive(true);
                playerMovementScript.canMove = false;

                foreach (DialogueLine line in trigger.dialogueLines)
                {
                    yield return ShowDialogue(line.text, line.duration);
                }

                dialoguePanel.SetActive(false);
                playerMovementScript.canMove = true;
            }
        }
    }

    private IEnumerator ShowDialogue(string message, float duration)
    {
        dialogueText.text = message;
        float timer = 0f;

        while (timer < duration && !skipDialogue)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        skipDialogue = false;
    }
}