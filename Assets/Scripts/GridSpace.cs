using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    public Button button;
    public Text buttonText;
    public string playerSide;

    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    public void SetSpace()
    {
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;

        int bestMove = gameController.findBestMove();

        //Debug.Log("AI: " + bestMove.ToString());

        gameController.EndTurn();

        gameController.makeMove(bestMove);
    }

    public void makeMove()
    {
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;

        gameController.EndTurn();
    }
}
