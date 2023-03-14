using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionHandler
{
    public Pawn currentPiece { get; private set; }
    public void HoverPiece(Pawn piece, bool isOver)
    {
        bool highlight = false;
        if (currentPiece == null)
        {
            highlight = piece.Color == GameManager.Instance.currentColor ? isOver : false;
        }
        else if (ComparePossibleLocations(piece.coordinates))
        {
            highlight = piece.Color != GameManager.Instance.currentColor ? isOver : false;
        }
        GameManager.Instance.gridManager.SetHighLight(piece.coordinates, highlight);
    }

    public void HoverLocation(Vector2Int coords, bool isOver)
    {
        if (currentPiece != null && ComparePossibleLocations(coords))
        {
            GameManager.Instance.gridManager.SetHighLight(coords, isOver);
        }
    }

    public void ClickPiece(Pawn piece)
    {
        Vector2Int targetCoordinates = piece.coordinates;
        if (piece.Color == GameManager.Instance.currentColor)
        {
            // undo highlight
            if (piece == currentPiece)
            {
                currentPiece = null;
                GameManager.Instance.gridManager.SetSelectedHighLight(targetCoordinates, false);
            }
            else
            {
                // highlight piece.ReturnPossibleLocations()
                currentPiece = piece;
                GameManager.Instance.gridManager.SetSelectedHighLight(targetCoordinates, true);
            }
        }
        else if (ComparePossibleLocations(targetCoordinates))
        {
            MovePieceOffBoard(piece);
            MoveCurrentPieceToPosition(targetCoordinates);
        }
    }

    public void MoveCurrentPieceToPosition(Vector2Int coords)
    {
        currentPiece.MovePiece(coords);
        currentPiece = null;
        GameManager.Instance.gridManager.SetSelectedHighLight(coords, false);
        if (GameManager.Instance.gameState != GameState.GameOver) SwitchPlayer();
    }

    private void MovePieceOffBoard(Pawn piece)
    {
        int offBoardPieces = GameManager.Instance.chessPieceList.Where(x => x.Color == piece.Color && !x.isActive).Count();

        int startX = piece.Color == PlayerColor.Dark ? 9 : -5;
        Vector2Int offBoardLocation = new Vector2Int(startX + offBoardPieces % 4, offBoardPieces / 4);

        piece.MovePiece(offBoardLocation);
        piece.SetPieceInactive();

        if (piece.GetPieceType() == PieceType.King)
        {
            GameManager.Instance.ChangeState(GameState.GameOver);
        }
    }

    public bool ComparePossibleLocations(Vector2Int coordinates)
    {
        return currentPiece != null && currentPiece.ReturnPossibleLocations().Any(x => x == coordinates);
    }

    private void SwitchPlayer()
    {
        GameManager.Instance.ChangeState(GameManager.Instance.gameState == GameState.PlayerLightTurn ? GameState.PlayerDarkTurn : GameState.PlayerLightTurn);
    }
}
