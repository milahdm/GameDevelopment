using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum CurrentPlayer 
    {
        Player1,
        Player2
    }

    CurrentPlayer currentPlayer;
    bool isWinningShotForPlayer1;
    bool isWinningShotForPlayer2;
    int player1BallsRemaining = 7;
    int player2BallsRemaining = 7;
    bool isWaitingForBallMovementToStop = false;
    bool willSwapPlayers = false;
    bool isGameOver = false;
    bool ballPocketed = false;
    [SerializeField] float shotTimer = 3f;
    private float currentTimer;
    [SerializeField] float movementThreshold;

    [SerializeField] TextMeshProUGUI player1BallsText;
    [SerializeField] TextMeshProUGUI player2BallsText;
    [SerializeField] TextMeshProUGUI currentTurnText;
    [SerializeField] TextMeshProUGUI messageText;

    [SerializeField] GameObject restartButton;

    [SerializeField] Transform headPosition;

    [SerializeField] Camera cueStickCamera;
    [SerializeField] Camera overHeadCamera;
    Camera currentCamera;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = CurrentPlayer.Player1;
        currentCamera = cueStickCamera;
        currentTimer = shotTimer;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaitingForBallMovementToStop && !isGameOver) 
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer > 0) 
            {
                return;
            }


            bool allStopped = true;

            foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball")) 
            {
                if (ball.GetComponent<Rigidbody>().velocity.magnitude >= movementThreshold) 
                {
                    Debug.Log(ball.GetComponent<Rigidbody>().velocity.magnitude);
                    allStopped = false;
                }
            }

            if (allStopped) 
            {
                isWaitingForBallMovementToStop = false;
                if (willSwapPlayers || !ballPocketed)
                {
                    NextPlayerTurn();
                }
                else 
                {
                    SwitchCameras();
                }
                currentTimer = shotTimer;
                ballPocketed = false;
            }

        }
        
    }

    public void SwitchCameras() 
    {
        if (currentCamera == cueStickCamera)
        {
            cueStickCamera.enabled = false;
            overHeadCamera.enabled = true;
            currentCamera = overHeadCamera;
            isWaitingForBallMovementToStop = true;
        }
        else 
        {
            cueStickCamera.enabled = true;
            overHeadCamera.enabled = false;
            currentCamera = cueStickCamera;
            currentCamera.gameObject.GetComponent<CameraController>().ResetCamera();
        }
    }

    bool Scratch() 
    {
        if (currentPlayer == CurrentPlayer.Player1)
        {
            if (isWinningShotForPlayer1)
            {
                ScratchOnWinningShot("Player 1");
                return true;
            }
        }
        else 
        {
            if (isWinningShotForPlayer2) 
            {
                ScratchOnWinningShot("Player 2");
                return true;
            }
        }
        willSwapPlayers = true;
        return false;
    }

    void EarlyEightBall() 
    {
        if (currentPlayer == CurrentPlayer.Player1)
        {
            Lose("Player 1 hit in the Eight Ball too early and has lost!");
        }
        else 
        {
            Lose("Player 2 hit in the Eight Ball too early and has lost!");
        }

    }

    void ScratchOnWinningShot(string player) 
    {
        Lose(player + " Scrated on winning shot!");

    }


    bool CheckBall(Ball ball) 
    {
        if (ball.IsCueBall())
        {
            if (Scratch())
            {
                return true;
            }
            else 
            {
                return false;
            }

        }
        else if (ball.IsEightBall())
        {
            if (currentPlayer == CurrentPlayer.Player1)
            {
                if (isWinningShotForPlayer1)
                {
                    Win("Player 1");
                    return true;
                }
            }
            else 
            {
                if (isWinningShotForPlayer2) 
                {
                    Win("Player 2");
                    return true;
                }
            }
           
            EarlyEightBall();

        }
        else 
        {
            if (ball.IsBallRed())
            {
                player1BallsRemaining--;
                player1BallsText.text = "Player 1 Balls Remaining: " + player1BallsRemaining;
                if (player1BallsRemaining <= 0)
                {
                    isWinningShotForPlayer1 = true;
                }
                if (currentPlayer != CurrentPlayer.Player1)
                {
                    willSwapPlayers = true;
                }
            }
            else 
            {
                player2BallsRemaining--;
                player2BallsText.text = "Player 2 Balls Remaining: " + player2BallsRemaining;
                if (player2BallsRemaining <= 0) 
                {
                    isWinningShotForPlayer2 = true;
                }
                if (currentPlayer != CurrentPlayer.Player2) 
                {
                    willSwapPlayers = true;
                }
            }

        }

        return true;

    }

    void Lose(string message) 
    {
        isGameOver = true;
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        restartButton.SetActive(true);

    }

    void Win(string player) 
    {
        isGameOver = true;
        messageText.gameObject.SetActive(true);
        messageText.text = player + " has won!";
        restartButton.SetActive(true);

    }

    void NextPlayerTurn() 
    {
        if (currentPlayer == CurrentPlayer.Player1)
        {
            currentPlayer = CurrentPlayer.Player2;
            currentTurnText.text = "Current Turn: Player 2";
        }
        else 
        {
            currentPlayer = CurrentPlayer.Player1;
            currentTurnText.text = "Current Turn: Player 1";
        }
        willSwapPlayers = false;

        SwitchCameras();

    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Ball") 
        {
            ballPocketed = true;
            if (CheckBall(other.gameObject.GetComponent<Ball>()))
            {
                Destroy(other.gameObject);
            }
            else 
            {
                other.gameObject.transform.position = headPosition.position;
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }

    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(0);
    }

}
