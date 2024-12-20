using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton 

    public Text gameOverText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowGameOver()
    {
        gameOverText.enabled = true;
        StartCoroutine(SwitchToMenu());
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    private IEnumerator SwitchToMenu()
    {
        yield return new WaitForSeconds(3.0f); 

        // Switch to MenuScene
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
}