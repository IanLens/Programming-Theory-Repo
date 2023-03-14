using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler
{
    public void SpawnPieces(Pawn[] gamePiecePrefabs)
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
                    CreatePiece(piece, new Vector2Int(3, piece.Color == PlayerColor.Dark ? 7 : 0));
                    break;
                case PieceType.King:
                    CreatePiece(piece, new Vector2Int(4, piece.Color == PlayerColor.Dark ? 7 : 0));
                    break;
                default:
                    break;
            }
        }

        GameManager.Instance.ChangeState(GameState.PlayerLightTurn);
    }

    private void SpawnPawns(Pawn pawn)
    {
        int yPos = pawn.Color == PlayerColor.Dark ? 6 : 1;
        for (int i = 0; i < 8; i++)
        {
            CreatePiece(pawn, new Vector2Int(i, yPos));
        }
    }

    private void SpawnDualPieces(Pawn piece, int xPosOffset)
    {
        int yPos = piece.Color == PlayerColor.Dark ? 7 : 0;
        CreatePiece(piece, new Vector2Int(0 + xPosOffset, yPos));
        CreatePiece(piece, new Vector2Int(7 - xPosOffset, yPos));
    }

    private void CreatePiece(Pawn piece, Vector2Int coords)
    {
        Pawn currentPawn = Pawn.Instantiate(piece, new Vector2(coords.x, coords.y), Quaternion.identity);
        currentPawn.Init(coords.x, coords.y);
        GameManager.Instance.chessPieceList.Add(currentPawn);
        currentPawn.transform.parent = GameObject.Find("ChessPieces").transform; // chessPiecesParent;
    }
}
