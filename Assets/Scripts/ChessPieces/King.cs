using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Pawn
{
    public override List<Vector2Int> ReturnPossibleLocations()
    {
        List<Vector2Int> possibleLocations = new List<Vector2Int>();

        possibleLocations.AddRange(KingMoves(Vector2Int.right));
        possibleLocations.AddRange(KingMoves(Vector2Int.left));
        possibleLocations.AddRange(KingMoves(Vector2Int.zero));

        return possibleLocations;
    }

    private List<Vector2Int> KingMoves(Vector2Int offSet)
    {
        List<Vector2Int> possibleLocations = new List<Vector2Int>();

        List<Vector2Int> checkList = new List<Vector2Int>();

        checkList.Add(offSet);
        checkList.Add(offSet + Vector2Int.up);
        checkList.Add(offSet + Vector2Int.down);

        foreach (Vector2Int coordsOffset in checkList)
        {
            Vector2Int coordsToCheck = coordinates + coordsOffset;
            if ((coordsToCheck.x >= 0 && coordsToCheck.x < 8 && coordsToCheck.y >= 0 && coordsToCheck.y < 8))
            {
                if (!LocationOccupiedCheck(coordsToCheck)) possibleLocations.Add(coordsToCheck);
                else if (TakeoverColorCheck(coordsToCheck)) possibleLocations.Add(coordsToCheck);
            }
        }

        return possibleLocations;
    }
}
