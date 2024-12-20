using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Button startButton;

    void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}