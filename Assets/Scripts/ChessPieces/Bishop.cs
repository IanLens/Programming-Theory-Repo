using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Rook
{
    public override List<Vector2Int> ReturnPossibleLocations()
    {
        List<Vector2Int> possibleLocations = new List<Vector2Int>();

        possibleLocations.AddRange(GetAvailableMovesInDirection(new Vector2Int(1,1)));
        possibleLocations.AddRange(GetAvailableMovesInDirection(new Vector2Int(-1,1)));
        possibleLocations.AddRange(GetAvailableMovesInDirection(new Vector2Int(1,-1)));
        possibleLocations.AddRange(GetAvailableMovesInDirection(new Vector2Int(-1,-1)));

        return possibleLocations;
    }
}
