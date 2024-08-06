using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    static public int key1 = 1 << 0; // 001
    static public int key2 = 1 << 1; // 010
    static public int key3 = 1 << 2; // 100

    static public int collectedKeys = 0; // 000

    // Adding keys
    public void AddKey(int key)
    {
        collectedKeys |= key;
    }

    // Checks if all keys are collected
    public bool HasAllKeys()
    {
        return collectedKeys == (key1 | key2 | key3);
    }

    public bool HasKey(int key)
    {
        return (collectedKeys & key) == key;
    }
}
