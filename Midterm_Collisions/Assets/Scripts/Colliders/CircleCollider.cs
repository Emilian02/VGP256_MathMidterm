using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CircleCollider : MonoBehaviour, ICustomCollider
{
    public float radius = 0.5f;

    void OnEnable()
    {
        ColliderManager.RegsiterCollider(this);
    }

    void OnDisable()
    {
        ColliderManager.UnregisterCollider(this);
    }

    public bool IsColliding(ICustomCollider other)
    {
        // Handle collision with another circle collider
        if (other is CircleCollider circle)
        {
            float distance = Vector2.Distance(transform.position, circle.transform.position);
            return distance < radius + circle.radius;
        }
        // Handle collision with a square collider
        if (other is SquareCollider square)
        {
            return CircleSquareCollision(this, square);
        }

        return false;
    }

    // Check collision between a circle and a square collider
    bool CircleSquareCollision(CircleCollider circle, SquareCollider square)
    {
        Vector2 circleDistance = new Vector2(
                                            Mathf.Abs(circle.transform.position.x - square.transform.position.x),
                                            Mathf.Abs(circle.transform.position.y - square.transform.position.y));

        // Check if the circle is too far from the square to collide
        if (circleDistance.x > (square.GetSize().x / 2 + circle.radius) || circleDistance.y > (square.GetSize().y / 2 + circle.radius))
        {
            return false;
        }

        // Check if the circle is close enough to the square to collide
        if (circleDistance.x <= (square.GetSize().x / 2) || circleDistance.y <= (square.GetSize().y / 2))
        {
            return false;
        }

        // Check collision with the square's corners
        float cornerDistanceSq = Mathf.Pow(circleDistance.x - square.GetSize().x / 2, 2) +
                                 Mathf.Pow(circleDistance.y - square.GetSize().y / 2, 2);

        return (cornerDistanceSq <= Mathf.Pow(circle.radius, 2));
    }

    // Return the type of this collider for debugging purposes
    public string GetColliderType()
    {
        return "CircleCollider";
    }

    void Start()
    {
        // It auto adjust to fit the gameobject at first
        radius = Mathf.Max(transform.localScale.x, transform.localScale.y) / 2f;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
