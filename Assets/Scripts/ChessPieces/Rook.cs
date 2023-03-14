using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// INHERITANCE
public class Rook : Pawn
{
    // POLYMORPHISM
    public override List<Vector2Int> ReturnPossibleLocations()
    {
        List<Vector2Int> possibleLocations = new List<Vector2Int>();

        possibleLocations.AddRange(GetAvailableMovesInDirection(Vector2Int.right));
        possibleLocations.AddRange(GetAvailableMovesInDirection(Vector2Int.left));
        possibleLocations.AddRange(GetAvailableMovesInDirection(Vector2Int.up));
        possibleLocations.AddRange(GetAvailableMovesInDirection(Vector2Int.down));
        
        return possibleLocations;
    }
    // ABSTRACTION
    protected List<Vector2Int> GetAvailableMovesInDirection(Vector2Int direction)
    {
        List<Vector2Int> possibleLocations = new List<Vector2Int>();

        Vector2Int coordsToCheck = coordinates;

        do
        {
            coordsToCheck += direction;
            if (!LocationOccupiedCheck(coordsToCheck)) possibleLocations.Add(coordsToCheck);
            else
            {
                if (TakeoverColorCheck(coordsToCheck))
                {
                    possibleLocations.Add(coordsToCheck);
                }
                return possibleLocations;
            }

        } while (coordsToCheck.x >= 0 && coordsToCheck.x < 8 && coordsToCheck.y >= 0 && coordsToCheck.y < 8);

        return possibleLocations;
    }
}
