using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GameState { GenerateGrid, SpawnPieces, PlayerDarkTurn, PlayerLightTurn, GameOver }
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

    public List<Pawn> chessPieceList { get; private set; } = new List<Pawn>();

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
            case GameState.PlayerDarkTurn:
                currentColor = PlayerColor.Black;
                break;
            case GameState.PlayerLightTurn:
                currentColor = PlayerColor.White;
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
            switch (piece.GetPieceType())
            {
                case PieceType.Pawn:
                    SpawnPawns(piece);
                    break;
                case PieceType.Rook:
                    SpawnDualPieces(piece, 0);
                    break;
                case PieceType.Bishop:
                    SpawnDualPieces(piece, 2);
                    break;
                case PieceType.Knight:
                    SpawnDualPieces(piece, 1);
                    break;
                case PieceType.Queen:
                    CreatePiece(piece, new Vector2Int(3, piece.GetColor() == PlayerColor.Black ? 7 : 0));
                    break;
                case PieceType.King:
                    CreatePiece(piece, new Vector2Int(4, piece.GetColor() == PlayerColor.Black ? 7 : 0));
                    break;
                default:
                    break;
            }
        }
    }

    private void SpawnPawns(Pawn pawn)
    {
        int yPos = pawn.GetColor() == PlayerColor.Black ? 6 : 1;
        for (int i = 0; i < 8; i++)
        {
            CreatePiece(pawn, new Vector2Int(i, yPos));
        }
    }

    private void SpawnDualPieces(Pawn piece, int xPosOffset)
    {
        int yPos = piece.GetColor() == PlayerColor.Black ? 7 : 0;
        CreatePiece(piece, new Vector2Int(0 + xPosOffset, yPos));
        CreatePiece(piece, new Vector2Int(7 - xPosOffset, yPos));
    }

    private void CreatePiece(Pawn piece, Vector2Int coords)
    {
        Pawn currentPawn = Instantiate(piece, new Vector2(coords.x, coords.y), Quaternion.identity);
        currentPawn.Init(coords.x, coords.y);
        chessPieceList.Add(currentPawn);
        currentPawn.transform.parent = chessPiecesParent;
    }

    public void HoverPiece(Pawn piece, bool isOver)
    {
        bool highlight = false;
        if (currentPiece == null)
        {
            highlight = piece.GetColor() == currentColor ? isOver : false;
        }
        else if(ComparePossibleLocations(piece.coordinates))
        {
            highlight = piece.GetColor() != currentColor ? isOver : false;
        }
        gridManager.SetHighLight(piece.coordinates, highlight);
    }

    public void HoverLocation(Vector2Int coords, bool isOver)
    {
        if (currentPiece != null && ComparePossibleLocations(coords))
        {
            gridManager.SetHighLight(coords, isOver);
        }
    }

    public void ClickPiece(Pawn piece)
    {
        if (piece.GetColor() == currentColor)
        {
            // undo highlight
            if (piece == currentPiece)
            {
                currentPiece = null;
                gridManager.SetSelectedHighLight(piece.coordinates, false);
            }
            else
            {
                // highlight piece.ReturnPossibleLocations()
                currentPiece = piece;
                gridManager.SetSelectedHighLight(piece.coordinates, true);
            }
        }
        else if(currentPiece != null && ComparePossibleLocations(piece.coordinates))
        {
            MoveCurrentPieceToPosition(piece.coordinates);
            MovePieceOffBoard(piece);
        }
    }

    public void MoveCurrentPieceToPosition(Vector2Int coords)
    {
        if (currentPiece != null && ComparePossibleLocations(coords))
        {
            currentPiece.MovePiece(coords);
            currentPiece = null;
            gridManager.SetSelectedHighLight(coords, false);
            SwitchPlayer();
        }
    }

    private void MovePieceOffBoard(Pawn piece)
    {
        if (piece.GetPieceType() != PieceType.King)
        {
            int offBoardPieces = chessPieceList.Where(x => x.GetColor() == piece.GetColor() && !x.isActive).Count();
            

            int startX = piece.GetColor() == PlayerColor.Black ? 9 : -5;
            Vector2Int offBoardLocation = new Vector2Int(startX + offBoardPieces % 4, offBoardPieces / 4);

            piece.MovePiece(offBoardLocation);
            piece.SetPieceInactive();
        }
    }

    private bool ComparePossibleLocations(Vector2Int coordinates)
    {
        return currentPiece.ReturnPossibleLocations().Any(x => x == coordinates);
    }

    private void SwitchPlayer()
    {
        ChangeState(GameState == GameState.PlayerLightTurn ? GameState.PlayerDarkTurn : GameState.PlayerLightTurn);
    }
}
