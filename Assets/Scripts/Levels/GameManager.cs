using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isDialogueActive = false;

    public TMP_Text dialogueText;
    public GameObject dialoguePanel;
    public PlayerMovement playerMovementScript;
    public Animator animator;
    public AudioSource backgroundMusic;

    private bool skipDialogue;
    private Coroutine activeDialogueCoroutine;

    void Start()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.loop = true;
            PlayBackgroundMusic();
        }
        StartCoroutine(SetupBattle());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            skipDialogue = true;

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Main Menu");
    }

    private IEnumerator SetupBattle()
    {
        playerMovementScript.canMove = false;
        dialoguePanel.SetActive(true);

        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "Level1")
        {
            dialoguePanel.SetActive(false);
            playerMovementScript.canMove = true;
        }
        else if (activeScene.name == "Level2")
        {
            dialoguePanel.SetActive(false);
            playerMovementScript.canMove = true;
        }
        else if (activeScene.name == "Level3")
        {
            dialoguePanel.SetActive(false);
            playerMovementScript.canMove = true;
            yield break;
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
                if (activeDialogueCoroutine != null)
                {
                    StopCoroutine(activeDialogueCoroutine);
                    activeDialogueCoroutine = null;
                }

                animator.SetBool("run", false);
                animator.SetBool("walkWithAK", false);
                animator.SetBool("walkWithPistol", false);
                animator.SetBool("walkWithShotgun", false);
                animator.SetBool("grounded", true);

                isDialogueActive = true;
                dialoguePanel.SetActive(true);
                playerMovementScript.canMove = false;

                foreach (DialogueLine line in trigger.dialogueLines)
                {
                    skipDialogue = false;
                    activeDialogueCoroutine = StartCoroutine(ShowDialogue(line.text, line.duration));
                    yield return activeDialogueCoroutine;
                }

                activeDialogueCoroutine = null;
                dialoguePanel.SetActive(false);
                playerMovementScript.canMove = true;
                isDialogueActive = false;

                if (trigger.destroyAfterDialogue)
                {
                    Destroy(trigger.gameObject);
                }
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

    private void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && backgroundMusic.clip != null)
        {
            backgroundMusic.Play();
        }
    }
}
