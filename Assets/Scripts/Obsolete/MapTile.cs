using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapTiles
{

    /// <summary>
    /// An enum identifying a neighbour
    /// </summary>
    /// The order starts from up, going clockwise.
    /// The values attached to each enum are for the use of SurroundingAwareSprite
    /// which uses its integer value as an index. Due to the nature of Aggregate
    /// being a left fold, it's necessary for the values to decrease as the direction
    /// goes clockwise.
    public enum Neighbour : int
    {
        Up = 3,
        Right = 2,
        Down = 1,
        Left = 0
    }

    public class MapTile : MonoBehaviour
    {
        
        private MapTile neighbourUp;
        private MapTile neighbourDown;
        private MapTile neighbourLeft;
        private MapTile neighbourRight;

        private SurroundingAwareSprite spriteManager;

        public GameObject occupant;

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
            // Curse you Unity 2019 for not supporting switch expressions. We could've had something great here.
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

}