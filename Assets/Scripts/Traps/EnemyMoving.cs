using System.Collections;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    public enum MoveDirection { Vertical, Horizontal }

    [SerializeField] private MoveDirection moveDirection = MoveDirection.Vertical;
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

        Vector3 targetPosition;
        if (moveDirection == MoveDirection.Vertical)
        {
            targetPosition = startPos + Vector3.up * moveDistance;
        }
        else
        {
            targetPosition = startPos + Vector3.right * moveDistance;
        }

        // Beweeg naar de target positie
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Wacht boven/in uiterste positie
        yield return new WaitForSeconds(delay);

        // Beweeg terug naar de startpositie
        while (Vector3.Distance(transform.position, startPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Wacht in de startpositie
        yield return new WaitForSeconds(delay);

        isMoving = false;
    }
}