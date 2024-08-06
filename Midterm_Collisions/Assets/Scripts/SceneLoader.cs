using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void GameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void EndScene()
    {
        SceneManager.LoadScene("EndGame");
    }
}
