using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Pawn : MonoBehaviour
{
    [SerializeField]
    private PlayerColor playerColor;
    [SerializeField]
    private PieceType pieceType;

    public bool isActive { get; private set; } = true;

    public Vector2Int coordinates { get; private set; }

    protected bool hasMoved = false;


    public void Init(int x, int y)
    {
        coordinates = new Vector2Int(x, y);
    }

    public PlayerColor GetColor()
    {
        return playerColor;
    }

    public PieceType GetPieceType()
    {
        return pieceType;
    }

    public void MovePiece(Vector2Int coords)
    {
        hasMoved = true;
        transform.position = new Vector2(coords.x, coords.y);
        coordinates = coords;
    }

    public virtual List<Vector2Int> ReturnPossibleLocations()
    {
        List<Vector2Int> possibleLocations = new List<Vector2Int>();

        int y = playerColor == PlayerColor.Light ? coordinates.y + 1 : coordinates.y - 1;
        int x = coordinates.x;

        // Basic movement if tile is not taken
        if (!LocationOccupiedCheck(new Vector2Int(x, y)))
        {
            possibleLocations.Add(new Vector2Int(x, y));
        }

        if (!hasMoved && !LocationOccupiedCheck(new Vector2Int(x, playerColor == PlayerColor.Light ? y + 1 : y - 1)))
        {
            possibleLocations.Add(new Vector2Int(x, playerColor == PlayerColor.Light ? y + 1 : y - 1));
        }

        // Takeover movement of Pawn
        Vector2Int[] availableTakeOvers = { new Vector2Int(x - 1, y), new Vector2Int(x + 1, y) };

        foreach (Vector2Int coords in availableTakeOvers)
        {
            if (LocationOccupiedCheck(coords))
            {
                if(TakeoverColorCheck(coords)) possibleLocations.Add(coords);
            }
        }

        return possibleLocations;
    }

    public void SetPieceInactive()
    {
        isActive = false;
    }

    protected bool LocationOccupiedCheck(Vector2Int coords)
    {
        return GameManager.Instance.chessPieceList.Any(piece => piece.coordinates == coords);
    }

    protected bool TakeoverColorCheck(Vector2Int coordsToCheck)
    {
        return GameManager.Instance.chessPieceList.Find(piece => piece.coordinates == coordsToCheck).GetColor() != GetColor();
    }

    private void OnMouseUp()
    {
        if(isActive) GameManager.Instance.interactionHandler.ClickPiece(this);
    }

    private void OnMouseOver()
    {
        if(isActive) GameManager.Instance.interactionHandler.HoverPiece(this, true);
    }

    private void OnMouseExit()
    {
        if(isActive) GameManager.Instance.interactionHandler.HoverPiece(this, false);
    }
}
