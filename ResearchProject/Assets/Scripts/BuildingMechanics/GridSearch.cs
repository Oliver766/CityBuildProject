using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// script reference by Sunny Valley Studio - https://www.youtube.com/watch?v=8ayFCDbfIIM&list=PLcRSafycjWFd6YOvRE3GQqURFpIxZpErI
// script eddited by Oliver Lancashire
// sid 1901981
/// <summary>
/// Source https://github.com/lordjesus/Packt-Introduction-to-graph-algorithms-for-game-developers
/// </summary>
public class GridSearch {

    /// <summary>
    /// list point grid
    /// </summary>
    public struct SearchResult
    {
        public List<Point> Path { get; set; }
    }
    /// <summary>
    /// a star search
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="startPosition"></param>
    /// <param name="endPosition"></param>
    /// <param name="isAgent"></param>
    /// <returns></returns>
    public static List<Point> AStarSearch(Grid grid, Point startPosition, Point endPosition, bool isAgent = false)
    {
        List<Point> path = new List<Point>();

        List<Point> positionsTocheck = new List<Point>();
        Dictionary<Point, float> costDictionary = new Dictionary<Point, float>();
        Dictionary<Point, float> priorityDictionary = new Dictionary<Point, float>();
        Dictionary<Point, Point> parentsDictionary = new Dictionary<Point, Point>();

        positionsTocheck.Add(startPosition);
        priorityDictionary.Add(startPosition, 0);
        costDictionary.Add(startPosition, 0);
        parentsDictionary.Add(startPosition, null);

        while (positionsTocheck.Count > 0)
        {
            Point current = GetClosestVertex(positionsTocheck, priorityDictionary);
            positionsTocheck.Remove(current);
            if (current.Equals(endPosition))
            {
                path = GeneratePath(parentsDictionary, current);
                return path;
            }

            foreach (Point neighbour in grid.GetAdjacentCells(current, isAgent))
            {
                float newCost = costDictionary[current] + grid.GetCostOfEnteringCell(neighbour);
                if (!costDictionary.ContainsKey(neighbour) || newCost < costDictionary[neighbour])
                {
                    costDictionary[neighbour] = newCost;

                    float priority = newCost + ManhattanDiscance(endPosition, neighbour);
                    positionsTocheck.Add(neighbour);
                    priorityDictionary[neighbour] = priority;

                    parentsDictionary[neighbour] = current;
                }
            }
        }
        return path;
    }
    /// <summary>
    /// get closest vertex
    /// </summary>
    /// <param name="list"></param>
    /// <param name="distanceMap"></param>
    /// <returns></returns>
    private static Point GetClosestVertex(List<Point> list, Dictionary<Point, float> distanceMap)
    {
        Point candidate = list[0];
        foreach (Point vertex in list)
        {
            if (distanceMap[vertex] < distanceMap[candidate])
            {
                candidate = vertex;
            }
        }
        return candidate;
    }
    /// <summary>
    /// get distance
    /// </summary>
    /// <param name="endPos"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    private static float ManhattanDiscance(Point endPos, Point point)
    {
        return Math.Abs(endPos.X - point.X) + Math.Abs(endPos.Y - point.Y);
    }
    /// <summary>
    /// generate path
    /// </summary>
    /// <param name="parentMap"></param>
    /// <param name="endState"></param>
    /// <returns></returns>
    public static List<Point> GeneratePath(Dictionary<Point, Point> parentMap, Point endState)
    {
        List<Point> path = new List<Point>();
        Point parent = endState;
        while (parent != null && parentMap.ContainsKey(parent))
        {
            path.Add(parent);
            parent = parentMap[parent];
        }
        return path;
    }
}
