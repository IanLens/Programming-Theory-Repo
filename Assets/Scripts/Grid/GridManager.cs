using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //public static GridManager Instance;

    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;

    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _highLight;

    //private void Awake()
    //{
    //    Instance = this;
    //}

    public void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Tile spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.parent = _parent;

                
                spawnedTile.Init(x, y);
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);



        GameManager.Instance.ChangeState(GameState.SpawnPieces);
    }

    public void SetHighLight(Vector2 pos)
    {
        _highLight.transform.position = pos;
    }
}
