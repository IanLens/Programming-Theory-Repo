using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;

    //private bool isOccupied { get; } = false;
    public void Init(int x, int y)
    {
        bool isOffset = (x + y) % 2 == 1;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    private void OnMouseOver()
    {
        GameManager.Instance.HoverLocation(this.transform.position, true);
    }

    private void OnMouseExit()
    {
        GameManager.Instance.HoverLocation(this.transform.position, false);
    }

    private void OnMouseUp()
    {
        GameManager.Instance.MoveCurrentPieceToPosition(transform.position);
    }
}
