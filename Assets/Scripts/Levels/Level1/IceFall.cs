using UnityEngine;

public class IceFall : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float startY = 18.92f;
    public float groundY = -5.08f;
    public bool repeatFall = true;

    void Update()
    {
        transform.Translate(Vector3.right * fallSpeed * Time.deltaTime);
        
        if (transform.position.y <= groundY)
        {
            if (repeatFall)
            {
                transform.position = new Vector3(transform.position.x, startY, transform.position.z);
            }
            else
            {
                enabled = false;
            }
        }
    }
}