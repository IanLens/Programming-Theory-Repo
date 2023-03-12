using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public enum GameState { GenerateGrid, SpawnPieces, PlayerDarkTurn, PlayerLightTurn, GameOver }
public enum PlayerColor { Dark, Light }
public enum PieceType { Pawn, Rook, Bishop, Knight, Queen, King}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState;

    [Header("Managers")]
    public GridManager gridManager;

    [Header("GamePieces")]
    [SerializeField] private Pawn[] gamePiecePrefabs;
    [SerializeField] private Transform chessPiecesParent;

    [Header("PlayerInfoTags")]
    [SerializeField] private TMP_Text LightPlayerNameTag;
    [SerializeField] private TMP_Text LightPlayerScoreTag;
    [SerializeField] private TMP_Text DarkPlayerNameTag;
    [SerializeField] private TMP_Text DarkPlayerScoreTag;

    [Header("GameOverMenu")]
    [SerializeField] private GameObject gameOverMenu;

    public List<Pawn> chessPieceList { get; private set; } = new List<Pawn>();

    public PlayerColor currentColor { get; private set; }

    private SpawnHandler spawnHandler;
    public InteractionHandler interactionHandler { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnHandler = ScriptableObject.CreateInstance<SpawnHandler>();
        interactionHandler = ScriptableObject.CreateInstance<InteractionHandler>();
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.GenerateGrid:
                gridManager.GenerateGrid();
                break;
            case GameState.SpawnPieces:
                SetPlayerNamesAndScore();
                spawnHandler.SpawnPieces(gamePiecePrefabs);
                break;
            case GameState.PlayerDarkTurn:
                currentColor = PlayerColor.Dark;
                break;
            case GameState.PlayerLightTurn:
                currentColor = PlayerColor.Light;
                break;
            case GameState.GameOver:
                GameOver();
                break;
            default:
                break;
        }
    }

    private void SetPlayerNamesAndScore()
    {
        LightPlayerNameTag.text = !string.IsNullOrEmpty(DataManager.instance?.playerLightName) ? DataManager.instance.playerLightName : "Light";
        LightPlayerScoreTag.text = DataManager.instance?.playerLightScore.ToString();
        DarkPlayerNameTag.text = !string.IsNullOrEmpty(DataManager.instance?.playerDarkName) ? DataManager.instance.playerDarkName : "Dark";
        DarkPlayerScoreTag.text = DataManager.instance?.playerDarkScore.ToString();
    }

    private void GameOver()
    {
        foreach (Pawn piece in chessPieceList)
        {
            piece.SetPieceInactive();
        }

        gameOverMenu.SetActive(true);

        string gameOverText = currentColor == PlayerColor.Dark ? DataManager.instance.playerDarkName : DataManager.instance.playerLightName;
        DataManager.instance.AddToPlayerScore(currentColor);
        gameOverMenu.transform.Find("GameOverText").GetComponent<TMP_Text>().text = gameOverText + " wins!";
    }
}
