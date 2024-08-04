using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public static List<ICustomCollider> allColliders = new List<ICustomCollider>();

    public static void RegsiterCollider(ICustomCollider collider)
    {
        if(!allColliders.Contains(collider))
        {
            allColliders.Add(collider);
        }
    }

    public static void UnregisterCollider(ICustomCollider collider)
    {
        if(allColliders.Contains(collider))
        {
            allColliders.Remove(collider);
        }
    }
}
    public interface ICustomCollider
    {
        bool IsColliding(ICustomCollider other);
        string GetColliderType();
    }
