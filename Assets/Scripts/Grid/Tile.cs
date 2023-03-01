using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GridManager _gridManager;

    public void Init(int x, int y)
    {
        bool isOffset = (x + y) % 2 == 1;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    private void OnMouseEnter()
    {
        GameManager.Instance.gridManager.SetHighLight(transform.position);
    }

    private void OnMouseExit()
    {
        //_highLight.SetActive(false);
    }
}
