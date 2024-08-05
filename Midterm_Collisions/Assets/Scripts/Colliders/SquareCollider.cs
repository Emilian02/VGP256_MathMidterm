using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SquareCollider : MonoBehaviour, ICustomCollider
{
    public Vector2 size;
    public Vector2 offset;

    void Start()
    {
        // It auto adjust to fit the gameobject at first
        size = new Vector2(transform.localScale.x, transform.localScale.y);
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
        // Calculate the actual position of this square collider + the offset
        Vector2 squarePosition = (Vector2)transform.position + offset;

        // If the other collider is also a SquareCollider, check for overlap
        if (other is SquareCollider square)
        {
            // Calculate the actual position of the other square collider, including the offset
            Vector2 otherSquarePosition = (Vector2)square.transform.position + square.offset;
            return IsOverlapping(squarePosition, size, otherSquarePosition, square.size);
        }

        // If the other collider is a CircleCollider, use the circle's collision detection logic
        if (other is CircleCollider circle)
        {
            return circle.IsColliding(this);
        }

        return false;
    }

    // Check if two rectangles overlap
    private bool IsOverlapping(Vector2 pos1, Vector2 size1, Vector2 pos2, Vector2 size2)
    {
        // Check for overlap along the x-axis
        bool overlapX = Mathf.Abs(pos1.x - pos2.x) < (size1.x + size2.x) / 2;

        // Check for overlap along the y-axis
        bool overlapY = Mathf.Abs(pos1.y - pos2.y) < (size1.y + size2.y) / 2;

        // Squares are colliding if they overlap along both axes
        return overlapX && overlapY;
    }


    // Return the type of collider as a string

    public string GetColliderType()
    {
        return "SqaureCollider";
    }

    // Return the size of the collider
    public Vector2 GetSize()
    {
        return size;
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
        Gizmos.DrawWireCube((Vector2)transform.position + offset, size);
    }
}
