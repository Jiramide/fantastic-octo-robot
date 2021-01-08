﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    
    private MapTile neighbourUp;
    private MapTile neighbourDown;
    private MapTile neighbourLeft;
    private MapTile neighbourRight;

    private SurroundingAwareSprite spriteManager;

    public GameObject occupant;

    public enum Neighbour : int
    {
        Up = 0,
        Right,
        Down,
        Left
    }

    void Awake()
    {
        spriteManager = GetComponent<SurroundingAwareSprite>();
    }

    public void SetNeighbour(Neighbour direction, MapTile newNeighbour)
    {
        spriteManager.SetState(
            (int) direction,
            newNeighbour == null ? 0 : 1
        );

        switch (direction)
        {
            case Neighbour.Up:
                neighbourUp = newNeighbour;
                break;
            case Neighbour.Right:
                neighbourRight = newNeighbour;
                break;
            case Neighbour.Down:
                neighbourDown = newNeighbour;
                break;
            case Neighbour.Left:
                neighbourLeft = newNeighbour;
                break;
        }

        spriteManager.UpdateSprite();
    }

    public MapTile GetNeighbour(Neighbour direction)
    {
        // Damn you Unity 2019 for not supporting switch expressions. We could've had something great here.
        /* 
        return direction switch {
            Neighbour.Up => neighbourUp,
            Neighbour.Right => neighbourRight,
            Neighbour.Down => neighbourDown,
            Neighbour.Left => neighbourLeft,
        }; */

        switch (direction)
        {
            case Neighbour.Up:
                return neighbourUp;
            case Neighbour.Right:
                return neighbourRight;
            case Neighbour.Down:
                return neighbourDown;
            default:
            // default is here because C# doesn't detect that using case Neighbour.Left is exhaustive and complains how not all codepaths lead to a return value.
                return neighbourLeft;
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

}