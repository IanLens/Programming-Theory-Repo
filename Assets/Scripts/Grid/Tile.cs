using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;

    public Vector2Int coordinates { get; private set; }

    //private bool isOccupied { get; } = false;
    public void Init(int x, int y)
    {
        bool isOffset = (x + y) % 2 == 1;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        coordinates = new Vector2Int(x, y);
    }

    private void OnMouseOver()
    {
        GameManager.Instance.HoverLocation(coordinates, true);
    }

    private void OnMouseExit()
    {
        GameManager.Instance.HoverLocation(coordinates, false);
    }

    private void OnMouseUp()
    {
        GameManager.Instance.MoveCurrentPieceToPosition(coordinates);
    }
}

public class Coordinates
{
    public int x { get; private set; }
    public int y { get; private set; }

    public Coordinates(int xPos, int yPos)
    {
        x = xPos;
        y = yPos;
    }
}
