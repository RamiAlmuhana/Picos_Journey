using System.Collections;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private float moveDistance = 2.0f;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float delay = 1.0f;

    private Vector3 startPos;
    private bool isMoving;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveSpikes());
        }
    }

    private IEnumerator MoveSpikes()
    {
        isMoving = true;

        // Beweeg omhoog
        Vector3 upPosition = startPos + Vector3.up * moveDistance;
        while (Vector3.Distance(transform.position, upPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, upPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Wacht boven
        yield return new WaitForSeconds(delay);

        // Beweeg naar beneden
        while (Vector3.Distance(transform.position, startPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Wacht beneden
        yield return new WaitForSeconds(delay);

        isMoving = false;
    }
}