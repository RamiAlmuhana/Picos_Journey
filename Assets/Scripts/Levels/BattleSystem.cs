using System.Collections;
using TMPro;
using UnityEngine;

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
        if (Input.GetKeyDown(KeyCode.Space))
            skipDialogue = true;
    }

    private IEnumerator SetupBattle()
    {
        playerMovementScript.canMove = false;
        dialoguePanel.SetActive(true);

        yield return ShowDialogue("What's happening?", 3f);
        yield return ShowDialogue("Why is the ice melting?", 3f);

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

            yield return ShowDialogue("Hi friend, do you know why the ice is melting?", 3f);
            yield return ShowDialogue("Hi Pico, the ice is melting because of a person who is causing climate disruptions toward the north.", 6f);

            dialoguePanel.SetActive(false);
            playerMovementScript.canMove = true;
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

        skipDialogue = false; // Reset voor volgende dialoog
    }
}
