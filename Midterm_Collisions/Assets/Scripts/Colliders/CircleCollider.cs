using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CircleCollider : MonoBehaviour, ICustomCollider
{
    public float radius = 0.5f;
    public Vector2 offset; 

    void Start()
    {
        // It auto adjust to fit the gameobject at first
        radius = Mathf.Max(transform.localScale.x, transform.localScale.y) / 2f;
    }

    // Adding the collider in the list when the gameobject enables
    void OnEnable()
    {
        ColliderManager.RegsiterCollider(this);
    }

    // Removes the collider from the list when the gameobject is disable
    void OnDisable()
    {
        ColliderManager.UnregisterCollider(this);
    }

    //Checks if it's colliding
    public bool IsColliding(ICustomCollider other)
    {
        // Calculates the actual position of this square collider + the offset
        Vector2 circlePosition = (Vector2)transform.position + offset;

        // Handle collision with another circle collider
        if (other is CircleCollider circle)
        {
            Vector2 otherCirclePosition = (Vector2)circle.transform.position + circle.offset; 
            float distance = Vector2.Distance(circlePosition, otherCirclePosition);
            return distance < radius + circle.radius;
        }
        // Handle collision with a square collider
        if (other is SquareCollider square)
        {
            return CircleSquareCollision(circlePosition, square);
        }

        return false;
    }

    // Check collision between a circle and a square collider
    bool CircleSquareCollision(Vector2 circle, SquareCollider square)
    {
        // Calculate the actual position of the square collider, including the offset
        Vector2 squarePosition = (Vector2)square.transform.position + square.offset;

        // Calculate the distance between the circle and the square's center
        Vector2 circleDistance = new Vector2(
            Mathf.Abs(circle.x - squarePosition.x),
            Mathf.Abs(circle.y - squarePosition.y)
        );

        // Check if the circle is too far from the square to collide
        if (circleDistance.x > (square.GetSize().x / 2 + radius) || circleDistance.y > (square.GetSize().y / 2 + radius))
        {
            return false;
        }

        // Check if the circle is close enough to the square to collide
        if (circleDistance.x <= (square.GetSize().x / 2) || circleDistance.y <= (square.GetSize().y / 2))
        {
            return true;
        }

        // Check collision with the square's corners
        float cornerDistance_sq = Mathf.Pow(circleDistance.x - square.GetSize().x / 2, 2) +
                                  Mathf.Pow(circleDistance.y - square.GetSize().y / 2, 2);

        return (cornerDistance_sq <= Mathf.Pow(radius, 2));
    }

    // Return the type of collider as a string
    public string GetColliderType()
    {
        return "CircleCollider";
    }

    // Return the GameObject this collider is attached to
    public GameObject GetGameObject()
    {
        return gameObject;
    }


    // Draws the outline of the collider
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)transform.position + offset, radius);
    }
}
