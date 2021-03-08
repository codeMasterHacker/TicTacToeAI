using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    public GameObject gameOverPanel; 
    public Text gameOverText;
    public GameObject restartButton;

    private string playerSide;
    private string user = "X";
    private string computer = "O";
    private int moveCount;

    void Start()
    {
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        SetGameControllerReferenceOnButtons();
        playerSide = "X";
        moveCount = 0;
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        moveCount++;

        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (moveCount >= 9) 
        {
            GameOver("tie");
        }

        ChangeSides();
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);

        if (winningPlayer == "tie")
        {
            SetGameOverText("It's a Tie!");
        }
        else
        {
            if (winningPlayer == computer)
                SetGameOverText("AI Wins!");
            else
                SetGameOverText("Human Wins!");
        }

        restartButton.SetActive(true);
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        playerSide = "X";
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        SetBoardInteractable(true);

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    public int findBestMove()
    {
        int bestVal = -1000;
        int bestMove = -1;

        for (int i = 0; i < buttonList.Length; i++)
        {
            if (buttonList[i].text == "")
            {
                buttonList[i].text = computer;

                int moveVal = minimax(0, false);

                buttonList[i].text = "";

                if (moveVal > bestVal)
                {
                    bestMove = i;
                    bestVal = moveVal;
                }
            }
        }

        return bestMove;
    }

    private int minimax(int depth, bool isMax)
    {
        int score = evaluate();

        if (score == 10)
            return score;

        if (score == -10)
            return score;

        if (isMovesLeft() == false)
            return 0;

        if (isMax)
        {
            int best = -1000;

            for (int i = 0; i < buttonList.Length; i++)
            {
                if (buttonList[i].text == "")
                {
                    buttonList[i].text = computer;

                    best = Mathf.Max(best, minimax(depth + 1, !isMax));

                    buttonList[i].text = "";
                }
            }

            return best;
        }
        else
        {
            int best = 1000;

            for (int i = 0; i < buttonList.Length; i++)
            {
                if (buttonList[i].text == "")
                {
                    buttonList[i].text = user;

                    best = Mathf.Min(best, minimax(depth + 1, !isMax));

                    buttonList[i].text = "";
                }
            }

            return best;
        }
    }

    private int evaluate()
    {
        if (buttonList[0].text == buttonList[1].text && buttonList[1].text == buttonList[2].text)
        {
            if (buttonList[1].text == computer)
                return +10;
            else if (buttonList[1].text == user)
                return -10;
        }

        if (buttonList[3].text == buttonList[4].text && buttonList[4].text == buttonList[5].text)
        {
            if (buttonList[4].text == computer)
                return +10;
            else if (buttonList[4].text == user)
                return -10;
        }

        if (buttonList[6].text == buttonList[7].text && buttonList[7].text == buttonList[8].text)
        {
            if (buttonList[7].text == computer)
                return +10;
            else if (buttonList[7].text == user)
                return -10;
        }


        if (buttonList[0].text == buttonList[3].text && buttonList[3].text == buttonList[6].text)
        {
            if (buttonList[3].text == computer)
                return +10;
            else if (buttonList[3].text == user)
                return -10;
        }

        if (buttonList[1].text == buttonList[4].text && buttonList[4].text == buttonList[7].text)
        {
            if (buttonList[4].text == computer)
                return +10;
            else if (buttonList[4].text == user)
                return -10;
        }

        if (buttonList[2].text == buttonList[5].text && buttonList[5].text == buttonList[8].text)
        {
            if (buttonList[5].text == computer)
                return +10;
            else if (buttonList[5].text == user)
                return -10;
        }


        if (buttonList[0].text == buttonList[4].text && buttonList[4].text == buttonList[8].text)
        {
            if (buttonList[4].text == computer)
                return +10;
            else if (buttonList[4].text == user)
                return -10;
        }

        if (buttonList[2].text == buttonList[4].text && buttonList[4].text == buttonList[6].text)
        {
            if (buttonList[4].text == computer)
                return +10;
            else if (buttonList[4].text == user)
                return -10;
        }

        return 0;
    }

    private bool isMovesLeft()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            if (buttonList[i].text == "")
                return true;
        }

        return false;
    }

    public void makeMove(int bestMove)
    {
        if (bestMove >= 0)
            buttonList[bestMove].GetComponentInParent<GridSpace>().makeMove();
    }
}
