using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    // Creating a satic list to have all the colliders in the scene on it
    public static List<ICustomCollider> allColliders = new List<ICustomCollider>();

    // Adding the collider in the list
    public static void RegsiterCollider(ICustomCollider collider)
    {
        if(!allColliders.Contains(collider))
        {
            allColliders.Add(collider);
        }
    }

    // Removing the collude from the list
    public static void UnregisterCollider(ICustomCollider collider)
    {
        if(allColliders.Contains(collider))
        {
            allColliders.Remove(collider);
        }
    }
}

// A simple interface so that all colliders have the same methods
    public interface ICustomCollider
    {
        // Returns the GameObject
        GameObject GetGameObject();
        // A colliding checker 
        bool IsColliding(ICustomCollider other);
        // Returns the type of collider that it has
        string GetColliderType();
    }
