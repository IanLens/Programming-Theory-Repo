using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GameState { GenerateGrid, SpawnPieces, Player1Turn, Player2Turn, GameOver }
public enum PlayerColor { Black, White }
public enum PieceType { Pawn, Rook, Bishop, Knight, Queen, King}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;

    [Header("Managers")]
    public GridManager gridManager;

    [Header("GamePieces")]
    [SerializeField] private Pawn[] gamePiecePrefabs;
    [SerializeField] private Transform chessPiecesParent;

    public Dictionary<Pawn, Vector2> chessPieceDictionary { get; private set; } = new Dictionary<Pawn, Vector2>();

    private PlayerColor currentColor;
    private Pawn currentPiece;

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
                SpawnPieces();
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

    private void SpawnPieces()
    {
        foreach (Pawn piece in gamePiecePrefabs)
        {
            switch (piece.pieceType)
            {
                case PieceType.Pawn:
                    SpawnPawns(piece);
                    break;
                case PieceType.Rook:
                    break;
                case PieceType.Bishop:
                    break;
                case PieceType.Knight:
                    break;
                case PieceType.Queen:
                    break;
                case PieceType.King:
                    break;
                default:
                    break;
            }
        }
    }

    private void SpawnPawns(Pawn pawn)
    {
        int yPos = pawn.playerColor == PlayerColor.Black ? 6 : 1;
        for (int i = 0; i < 8; i++)
        {
            CreatePiece(pawn, new Vector2(i, yPos));
        }
    }

    private void CreatePiece(Pawn piece, Vector2 location)
    {
        Pawn currentPawn = Instantiate(piece, location, Quaternion.identity);
        chessPieceDictionary.Add(currentPawn, location);
        currentPawn.transform.parent = chessPiecesParent;
    }

    public void HoverPiece(Pawn piece, bool isOver)
    {
        bool highlight = false;
        if (currentPiece == null)
        {
            highlight = piece.playerColor == currentColor ? isOver : false;
        }
        else if(ComparePossibleLocations(piece.transform.position))
        {
            highlight = piece.playerColor != currentColor ? isOver : false;
        }
        gridManager.SetHighLight(piece.transform.position, highlight);
    }

    public void HoverLocation(Vector2 pos, bool isOver)
    {
        if (currentPiece != null && ComparePossibleLocations(pos))
        {
            gridManager.SetHighLight(pos, isOver);
        }
    }

    public void ClickPiece(Pawn piece)
    {
        if (piece.playerColor == currentColor)
        {
            // undo highlight
            if (piece == currentPiece)
            {
                Debug.Log("Deselected piece " + piece.pieceType + " at position " + piece.transform.position);
                currentPiece = null;
                gridManager.SetSelectedHighLight(piece.transform.position, false);
            }
            else // if (currentPiece == null)
            {
                Debug.Log("Selected piece " + piece.pieceType + " at position " + piece.transform.position);
                // highlight piece.ReturnPossibleLocations()
                currentPiece = piece;
                gridManager.SetSelectedHighLight(piece.transform.position, true);
            }
        }
        else if(currentPiece != null && ComparePossibleLocations(piece.transform.position))
        {
            MoveCurrentPieceToPosition(piece.transform.position);
            MovePieceOffBoard(piece);
        }
    }

    public void MoveCurrentPieceToPosition(Vector2 pos)
    {
        // Check if viable position -> Move piece
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Clean this up with proper class to hold official chessboard locations
        if (currentPiece != null && ComparePossibleLocations(pos))
        {
            Debug.Log("test");
            currentPiece.gameObject.transform.position = pos;
            chessPieceDictionary[currentPiece] = pos;
            currentPiece = null;
            gridManager.SetSelectedHighLight(pos, false);
        }
    }

    private void MovePieceOffBoard(Pawn piece)
    {
        if (piece.pieceType != PieceType.King)
        {
            int offBoardPieces = chessPieceDictionary.Where(x => x.Key.playerColor == piece.playerColor && !x.Key.isActive).Count();
            piece.SetPieceInactive();

            int startX = piece.playerColor == PlayerColor.Black ? 9 : -5;

            piece.gameObject.transform.position = new Vector2(startX + offBoardPieces % 4, offBoardPieces / 4);
        }
    }

    private bool ComparePossibleLocations(Vector2 pos)
    {
        return currentPiece.ReturnPossibleLocations().Any(x => x.ToString() == pos.ToString());
    }
}
