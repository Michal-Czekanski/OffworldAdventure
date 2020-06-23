using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public GameObject leftBound;
    public GameObject rightBound;
    public GameObject topBound;
    public GameObject bottomBound;

    public float xOffset = 0f;
    public float yOffset = 0f;
    
    public float zCoord = -10f;

    Vector3 playerPosition;

    float leftBoundX;
    float rightBoundX;
    float topBoundY;
    float bottomBoundY;

    float halfCamWidth;
    float halfCamHeight;

    public void Start()
    {
        EdgeCollider2D leftCollider = leftBound.GetComponent<EdgeCollider2D>();
        leftBoundX = leftCollider.bounds.center.x;

        EdgeCollider2D rightCollider = rightBound.GetComponent<EdgeCollider2D>();
        rightBoundX = rightCollider.bounds.center.x;

        EdgeCollider2D topCollider = topBound.GetComponent<EdgeCollider2D>();
        topBoundY = topCollider.bounds.center.y;

        EdgeCollider2D bottomCollider = bottomBound.GetComponent<EdgeCollider2D>();
        bottomBoundY = bottomCollider.bounds.center.y;


        // Get cam width and height
        Camera camera = Camera.main;
        halfCamHeight = camera.orthographicSize;
        halfCamWidth = camera.aspect * halfCamHeight;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 newPosition = new Vector3(player.transform.position.x - xOffset, player.transform.position.y - yOffset, zCoord);

        float newCamLeft = newPosition.x - halfCamWidth;
        float newCamRight = newPosition.x + halfCamWidth;

        float newCamTop = newPosition.y + halfCamHeight;
        float newCamBottom = newPosition.y - halfCamHeight;


        // TODO: Based on cam width and height check if camera is withing game bounds
        if (newCamLeft < leftBoundX)
        {
            newPosition.x = leftBoundX + halfCamWidth;
        }
        if (newCamRight > rightBoundX)
        {
            newPosition.x = rightBoundX - halfCamWidth;
        }
        if(newCamTop > topBoundY)
        {
            newPosition.y = topBoundY - halfCamHeight;
        }
        if (newCamBottom < bottomBoundY)
        {
            newPosition.y = bottomBoundY + halfCamHeight;
        }



        transform.position = newPosition;
    }
}
