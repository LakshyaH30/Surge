using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private Transform[] groundTiles;
    [SerializeField] private float tileWidth = 30f;

    private void Update()
    {
        foreach(Transform tile in groundTiles)
        {
            //Move
            tile.position += Vector3.left * scrollSpeed * Time.deltaTime;

            // Reset tile to center
            if(tile.position.x < -tileWidth)
            {
                tile.position += Vector3.right * tileWidth * 2f; 
            }
        }
    }
}
