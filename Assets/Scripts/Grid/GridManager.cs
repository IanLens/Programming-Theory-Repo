using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridManager : MonoBehaviour
{
    //public static GridManager Instance;

    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;

    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _highLight;
    [SerializeField] private Transform _selectedHighLight;

    [SerializeField] private TMP_Text locationText;

    private List<Tile> tileGrid = new List<Tile>();

    public void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Tile spawnedTile = Instantiate(_tilePrefab, new Vector2(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.parent = _parent;

                
                spawnedTile.Init(x, y);
                tileGrid.Add(spawnedTile);
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);



        GameManager.Instance.ChangeState(GameState.SpawnPieces);
    }

    public void SetHighLight(Vector2Int pos, bool setActive)
    {
        HighLightFunctionality(_highLight, pos, setActive);
    }

    public void SetSelectedHighLight(Vector2Int pos, bool setActive)
    {
        HighLightFunctionality(_selectedHighLight, pos, setActive);
    }

    public void HighLightFunctionality(Transform highlight, Vector2Int coords, bool setActive)
    {
        if (setActive)
        {
            highlight.gameObject.SetActive(true);
            highlight.position = new Vector2(coords.x, coords.y);
        }
        else
        {
            highlight.gameObject.SetActive(false);
        }
    }

    public Tile GetTile(Vector2Int coords)
    {
        Tile tile = tileGrid.Find(x => x.coordinates == coords);
        return tile;
    }
}
