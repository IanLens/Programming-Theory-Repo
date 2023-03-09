using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Rook
{
    public override List<Vector2Int> ReturnPossibleLocations()
    {
        List<Vector2Int> possibleLocations = new List<Vector2Int>();

        possibleLocations.AddRange(CheckDirection(Vector2Int.right));
        possibleLocations.AddRange(CheckDirection(Vector2Int.left));
        possibleLocations.AddRange(CheckDirection(Vector2Int.up));
        possibleLocations.AddRange(CheckDirection(Vector2Int.down));
        possibleLocations.AddRange(CheckDirection(new Vector2Int(1, 1)));
        possibleLocations.AddRange(CheckDirection(new Vector2Int(-1, 1)));
        possibleLocations.AddRange(CheckDirection(new Vector2Int(1, -1)));
        possibleLocations.AddRange(CheckDirection(new Vector2Int(-1, -1)));

        return possibleLocations;
    }
}
