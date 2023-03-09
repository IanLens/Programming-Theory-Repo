using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Pawn
{
    public override List<Vector2Int> ReturnPossibleLocations()
    {
        List<Vector2Int> possibleLocations = KnightMoves(new Vector2Int(2, 1));

        return possibleLocations;
    }

    private List<Vector2Int> KnightMoves(Vector2Int offset)
    {
        List<Vector2Int> possibleLocations = new List<Vector2Int>();

        List<Vector2Int> checkList = new List<Vector2Int>();

        checkList.Add(new Vector2Int(offset.x, offset.y));
        checkList.Add(new Vector2Int(offset.x, -offset.y));
        checkList.Add(new Vector2Int(offset.y, offset.x));
        checkList.Add(new Vector2Int(offset.y, -offset.x));
        checkList.Add(new Vector2Int(-offset.x, offset.y));
        checkList.Add(new Vector2Int(-offset.x, -offset.y));
        checkList.Add(new Vector2Int(-offset.y, offset.x));
        checkList.Add(new Vector2Int(-offset.y, -offset.x));

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
