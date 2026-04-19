using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private ScrollWithGround scroll;

    private void Start()
    {
        scroll = GetComponent<ScrollWithGround>();
        scroll.SetSpeed(5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Hit!!");
        if(collision != null && collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().Die();
        }
    }
}
