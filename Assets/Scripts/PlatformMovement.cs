using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float platformSpeed = 10.0f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = new Vector3(0.0f, 0.0f, 1.0f) * platformSpeed * Time.deltaTime;
        transform.position -= movement;
    }
}