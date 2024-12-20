using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    int redBallsRemaining = 7;
    int blueBallsRemaining = 7;
    float ballRadius;
    float ballDiameter;
    float ballDiameterWithBuffer;

    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform cueBallPos;
    [SerializeField] Transform headBallPos;

    // Called before Start
    private void Awake()
    {
        ballRadius = ballPrefab.GetComponent<SphereCollider>().radius * 100f;
        ballDiameter = ballRadius * 2f;

        PlaceAllBalls();

    }

    void PlaceAllBalls() 
    {
        PlaceCueBall();
        PlaceRandomBalls();

    }

    void PlaceCueBall() 
    {
        GameObject ball = Instantiate(ballPrefab, cueBallPos.position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeCueBall();

    }

    void PlaceEightBall(Vector3 position) 
    {
        GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeEightBall();

    }

    void PlaceRandomBalls() 
    {
        int NumInThisRow = 1;
        int rand;
        Vector3 firstInRowPosition = headBallPos.position;
        Vector3 currentPos = firstInRowPosition;

        void PlaceRedBall(Vector3 position) 
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetUp(true);
            redBallsRemaining--;
        }

        void PlaceBlueBall(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetUp(false);
            blueBallsRemaining--;
        }

        // 5 rows
        for (int i = 0; i < 5; i++)
        {
            // balls in each row
            for (int j = 0; j < NumInThisRow; j++) 
            {
                // check for middle spot, 8 ball
                if (i == 2 && j == 1)
                {
                    PlaceEightBall(currentPos);
                }
                // if red and blue balls remaining choose randomly
                else if (redBallsRemaining > 0 && blueBallsRemaining > 0)
                {
                    rand = Random.Range(0, 2);
                    if (rand == 0)
                    {
                        PlaceRedBall(currentPos);
                    }
                    else
                    {
                        PlaceBlueBall(currentPos);
                    }
                }
                // if only red balls place one
                else if (redBallsRemaining > 0)
                {
                    PlaceRedBall(currentPos);
                }
                // otherwise place blue ball
                else 
                {
                    PlaceBlueBall(currentPos);
                }

                // move current pos of ball 1 spot to the right
                currentPos += new Vector3(1, 0, 0).normalized * ballDiameter;
            }

            // once all the balls in the row have been placed move to next row
            firstInRowPosition += Vector3.back * (Mathf.Sqrt(3) * ballRadius) + Vector3.left * ballRadius;
            currentPos = firstInRowPosition;
            NumInThisRow++;
        }




    }

}
