using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Pawn : MonoBehaviour
{
    public PlayerColor playerColor;

    public PieceType pieceType;

    public bool isActive { get; private set; } = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void MovableCheck(PlayerColor currentPlayerColor)
    //{
    //    isMovable = playerColor == currentPlayerColor ? true : false;
    //}

    public List<Vector2> ReturnPossibleLocations()
    {
        List<Vector2> possibleLocations = new List<Vector2>();

        int y = playerColor == PlayerColor.White ? (int)transform.position.y + 1 : (int)transform.position.y - 1;
        int x = (int)transform.position.x;

        // Basic movement if tile is not taken
        if (!GameManager.Instance.chessPieceDictionary.ContainsValue(new Vector2(x, y)))
        {
            possibleLocations.Add(new Vector2(x, y));
        }

        // Takeover movement of Pawn
        Vector2[] availableTakeOvers = { new Vector2(x - 1, y), new Vector2(x + 1, y) };

        foreach (Vector2 pos in availableTakeOvers)
        {
            if (GameManager.Instance.chessPieceDictionary.ContainsValue(pos))
            {
                possibleLocations.Add(pos);
            }
        }

        return possibleLocations;
    }

    public void SetPieceInactive()
    {
        isActive = false;
    }

    private void OnMouseUp()
    {
        if(isActive) GameManager.Instance.ClickPiece(this);
    }

    private void OnMouseOver()
    {
        if(isActive) GameManager.Instance.HoverPiece(this, true);
    }

    private void OnMouseExit()
    {
        if(isActive) GameManager.Instance.HoverPiece(this, false);
    }
}
