using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string text;
    public float duration;
}

public class DialogueTrigger : MonoBehaviour
{
    public DialogueLine[] dialogueLines;
}