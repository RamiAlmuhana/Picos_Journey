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
            // yield return ShowDialogue("Pico: This… this is no longer our home. The ice is melting faster than ever, and my family has nowhere left to live. I can’t just stand by and watch everything disappear!", 12f);
            // yield return ShowDialogue("Pico: But why is the ice melting?", 5f);
            // yield return ShowDialogue("Mother: Pico, you are our only hope. Find a way to stop this. We believe in you.", 8f);
            // yield return ShowDialogue("Pico: It’s time to set out. I heard the old ice machine might still work. If I collect enough water droplets to power it, maybe it can generate enough ice for us!", 12f);
            // yield return ShowDialogue("Pico: That way, my family can survive for now… until I find a better solution!", 8f);
            dialoguePanel.SetActive(false);
            playerMovementScript.canMove = true;
            
        } else if (activeScene.name == "Level2")
        {
            dialoguePanel.SetActive(false);
            playerMovementScript.canMove = true;
        } else if (activeScene.name == "Level3")
        {
            yield return ShowDialogue("Why is there garbage everywhere?", 3f);
            yield return ShowDialogue("I need to destroy the drones to prevent them from polluting everything!", 3f);
            yield return ShowDialogue("I have to find something to destroy the drones with!", 3f);
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
                    skipDialogue = false;
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

        while (timer < duration)
        {
            if (skipDialogue) break;
            timer += Time.deltaTime;
            yield return null;
        }

        skipDialogue = false;
    }

}