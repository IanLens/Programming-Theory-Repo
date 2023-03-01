using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { GenerateGrid, SpawnPieces, Player1Turn, Player2Turn, GameOver }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;

    [Header("Managers")]
    public GridManager gridManager;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;

        switch (newState)
        {
            case GameState.GenerateGrid:
                gridManager.GenerateGrid();
                break;
            case GameState.SpawnPieces:
                break;
            case GameState.Player1Turn:
                break;
            case GameState.Player2Turn:
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }
    }
}
