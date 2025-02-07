using UnityEngine;

public class RioController : MonoBehaviour
{
    public float MoveSpeed = 10;

    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 newPosition = transform.position + direction * MoveSpeed * Time.deltaTime;

        // Clamp within platform bounds (adjust values accordingly)
        newPosition.x = Mathf.Clamp(newPosition.x, -100f, 100f);
        newPosition.z = Mathf.Clamp(newPosition.z, -100f, 100f);

        transform.position = newPosition;

        if (direction != Vector3.zero)
        {
            transform.LookAt(transform.position + direction);
        }
    }
}
