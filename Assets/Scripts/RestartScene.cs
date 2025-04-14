using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public GameObject restartMenu;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        PlayerMovement2D.OnEnemyCollider += ActivateRestartMenu;
    }
    private void OnDisable()
    {
        PlayerMovement2D.OnEnemyCollider -= ActivateRestartMenu;
    }
    private void ActivateRestartMenu()
    {
        Time.timeScale = 0f; 
        restartMenu.SetActive(true);
    }
    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}