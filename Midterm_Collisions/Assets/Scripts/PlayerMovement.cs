using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    InputActionReference movement;
    [SerializeField]
    Animator animator;
    [SerializeField]
    float speed;
    [SerializeField]
    CircleCollider circleCollider;
    [SerializeField]
    float collisionPushFactor = 0.1f;

    Vector2 movementInput;
    Rigidbody2D rb;
    SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movementInput = movement.action.ReadValue<Vector2>();

        if (movementInput.x < 0 || movementInput.x > 0 ||
            movementInput.y < 0 || movementInput.y > 0)
        {
            if(movementInput.x < 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        // Check for collisions
        foreach (var collider in ColliderManager.allColliders)
        {
            if (circleCollider.IsColliding(collider))
            {
                if (collider.GetGameObject().CompareTag("Wall"))
                {
                    // Handle collision response
                    HandleCollisionResponse();
                }
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movementInput * speed * Time.deltaTime;
    }

    private void HandleCollisionResponse()
    {
        foreach (var collider in ColliderManager.allColliders)
        {
            // Skip self-collision
            if (collider.GetGameObject() == gameObject)
                continue;

            if (circleCollider.IsColliding(collider))
            {
                // Calculate collision point and normal
                Vector2 collisionPoint = GetCollisionPoint(collider);
                Vector2 collisionNormal = (Vector2)transform.position - collisionPoint;
                collisionNormal.Normalize();

                // Calculate penetration depth
                float penetrationDepth = CalculatePenetrationDepth(collider, collisionPoint);

                // Apply collision pushback
                Vector2 scale = transform.localScale;
                Vector2 adjustedPushback = collisionNormal * (penetrationDepth / Mathf.Max(scale.x, scale.y)) * collisionPushFactor;
                rb.position += adjustedPushback;
            }
        }
    }

    private Vector2 GetCollisionPoint(ICustomCollider other)
    {
        if (other is SquareCollider square)
        {
            // Calculates the closest point on the square to the circle center
            Vector2 circleCenter = (Vector2)transform.position;
            Vector2 squareCenter = (Vector2)square.GetGameObject().transform.position;
            Vector2 halfSize = square.GetSize() / 2;

            // Finding the closest point on the square's boundary
            float closestX = Mathf.Clamp(circleCenter.x, squareCenter.x - halfSize.x, squareCenter.x + halfSize.x);
            float closestY = Mathf.Clamp(circleCenter.y, squareCenter.y - halfSize.y, squareCenter.y + halfSize.y);
            return new Vector2(closestX, closestY);
        }
        // For Circle-Circle, collision point is the midpoint of the overlapping region
        else if (other is CircleCollider otherCircle)
        {
            Vector2 directionToOther = (Vector2)other.GetGameObject().transform.position - (Vector2)transform.position;
            return (Vector2)transform.position + directionToOther.normalized * circleCollider.radius;
        }
        return Vector2.zero;
    }

    private float CalculatePenetrationDepth(ICustomCollider other, Vector2 collisionPoint)
    {
        if (other is CircleCollider otherCircle)
        {
            // Calculates depth for circle-circle collision
            float distance = Vector2.Distance(transform.position, collisionPoint);
            return circleCollider.radius + otherCircle.radius - distance;
        }
        else if (other is SquareCollider square)
        {
            // Calculates depth for circle-square collision
            Vector2 circleCenter = (Vector2)transform.position;
            return circleCollider.radius - Vector2.Distance(circleCenter, collisionPoint);
        }
        return 0f;
    }
}
