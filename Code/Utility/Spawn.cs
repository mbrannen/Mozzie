using Godot;
using System;

public static class Spawn
{
    /// <summary>
    /// Returns a random point at a random angle from the center position at a specified radius
    /// </summary>
    /// <param name="position">The center position from which to spawn from</param>
    /// <param name="radius">The distance to spawn from the center of the circle</param>
    /// <returns>Vector2</returns>
    public static Vector2 GetRandomPointInCircleAroundPosition(Vector2 position, float radius)
    {
        var randomAngleRadian = Mathf.DegToRad((float)GD.RandRange(0.1, 360));
        return GetPointVector(position, radius, randomAngleRadian);
    }

    public static Vector2[] GetPointsInCircleAroundPosition(Vector2 position, float radius, int numberOfPoints)
    {
        var pointLocations = new Vector2[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            var angleRadian = Mathf.DegToRad((360f/numberOfPoints*i));
            pointLocations[i] = GetPointVector(position, radius, angleRadian);
        }

        return pointLocations;
    }

    private static Vector2 GetPointVector(Vector2 center, float radius, float angleRadian)
    {
        Vector2 point;
        point.X = center.X + radius * Mathf.Sin(angleRadian);
        point.Y = center.Y + radius * Mathf.Cos(angleRadian);
        return point;
    }
}
