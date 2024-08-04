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
    float speed;

    Vector2 movementInput;
    Rigidbody2D rb;
    CircleCollider circleCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider>();

    }

    void Update()
    {
        movementInput = movement.action.ReadValue<Vector2>();

        // Check for collisions
        foreach (var collider in ColliderManager.allColliders)
        {
            if (collider != circleCollider && circleCollider.IsColliding(collider))
            {
                Debug.Log($"{circleCollider.GetColliderType()} collided with {collider.GetColliderType()}");
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movementInput * speed * Time.deltaTime;   
    }
}
