using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayKeys : MonoBehaviour
{
    [SerializeField] GameObject key1Icon;
    [SerializeField] GameObject key2Icon;
    [SerializeField] GameObject key3Icon;

    private KeyManager keyManager;

    void Start()
    {
        keyManager = FindObjectOfType<KeyManager>();
    }

    void Update()
    {
        key1Icon.SetActive(keyManager.HasKey(KeyManager.key1));
        key2Icon.SetActive(keyManager.HasKey(KeyManager.key2));
        key3Icon.SetActive(keyManager.HasKey(KeyManager.key3));
    }
}
