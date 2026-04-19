using UnityEngine;

public class ScrollWithGround : MonoBehaviour
{
    private float scrollSpeed;

    public void SetSpeed(float speed)
    {
        scrollSpeed = speed;
    }

    private void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
    }
}