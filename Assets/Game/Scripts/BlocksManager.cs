using System.Collections.Generic;
using UnityEngine;


public class BlocksManager : MonoBehaviour
// Hybrid manager, creates all blocks in game.
// i chose to keep them in a list, and tag all middle blocks in a special "middle block" tag.
{
    #region Inspector

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject brickPrefab;

    #endregion


    #region Fields

    private readonly List<GameObject> _bricks = new List<GameObject>(); // I keep all blocks here
    private float _animationTime;


    //utilities for the procedural blocks animation
    private int _diagonalIndex;
    private float _remaining;


    //parameters for creating a grid of block (rows, columns, the spacing between them in the grid)
    private const int Rows = 5;
    private const int Cols = 11;
    private const float HorizonSpacing = 0.1f;
    private const float VerticSpacing = 0.2f;


    // i keep track of the current state of the grid with this variable
    private int _blocksRemaining = 55;

    #endregion

    #region Properties

    public int BlocksRemaining
    {
        get => _blocksRemaining;
        set => _blocksRemaining -= value;
    }

    #endregion


    #region Methods

    public void ReduceBlock(GameObject block)
    {
        _bricks.Remove(block);
        _blocksRemaining -= 1;
        // if (_blocksRemaining != 0) return;
        // Debug.Log("WIN!");
        // _gameManager.Win = true;
        // ResetLevel();
    }

    private void AnimateDiagonalBlocks(int diagIndex)
    {
        foreach (GameObject brick in _bricks)
        {
            var brickBehaviour = brick.GetComponent<BlockBehaviour>();
            var coord = brickBehaviour.Coordinates;
            if (coord.x + coord.y == diagIndex)
            {
                brickBehaviour.animationTrigger = true;
            }
        }
    }


    public void ResetLevel()
    {
        _diagonalIndex = 0;
        _remaining = 0f;
        foreach (var brick in _bricks)
        {
            Destroy(brick);
        }

        _bricks.Clear();


        for (var y = 0; y < Rows; y++)
        {
            var color = GetColorFromString(_rowColors[y]);
            for (var x = 0; x < Cols; x++)
            {
                var localScale = brickPrefab.transform.localScale;
                var spawnPos = (Vector2) transform.position + new Vector2(
                    x * (localScale.x + HorizonSpacing),
                    -y * (localScale.y + VerticSpacing));

                var brick = Instantiate(brickPrefab, spawnPos, Quaternion.identity);
                if (y == 2)
                {
                    brick.tag = "MidRowBlock"; // spacial tag for all middle blocks
                }

                var blockBehaviour = brick.GetComponent<BlockBehaviour>();
                var coordinates = new Vector2Int(y, x);
                blockBehaviour.Coordinates = coordinates;
                blockBehaviour.SetColor(color);
                _bricks.Add(brick);
            }
        }
    }

    #endregion


    #region MonoBehaviour

    private void Awake()
    {
        _animationTime = gameManager.blocksAnimationTime;
    }


    private void Update()
    {
        if (_diagonalIndex >= 15)
        {
            return;
        }

        _remaining -= Time.deltaTime;
        if (_remaining > 0)
            return;

        AnimateDiagonalBlocks(_diagonalIndex);
        _diagonalIndex += 1;
        _remaining = _animationTime;
    }

    #endregion


    #region Color Conversion

    // these are the colors i picked:
    private readonly List<string> _rowColors = new List<string> {"E6F07D", "F0C5BD", "F0A5D5", "C48DF0", "99F0CD"};


    // helper functions: 
    private static int HexToDec(string hex)
    {
        var dec = System.Convert.ToInt32(hex, 16);
        return dec;
    }

    // private static string DecToHex(int value)
    // {
    //     return value.ToString("X2");
    // }

    private float HexToFloatNormalized(string hex)
    {
        return HexToDec(hex) / 255f;
    }

    // main function:
    private Color GetColorFromString(string hexString)
    {
        float red = HexToFloatNormalized(hexString.Substring(0, 2));
        float green = HexToFloatNormalized(hexString.Substring(2, 2));
        float blue = HexToFloatNormalized(hexString.Substring(4, 2));
        return new Color(red, green, blue);
    }

    #endregion
    // (about Color Conversion) :
    // I wanted to be able to change the rows colors easily for design purposes, so i made this set of functions
    //  this allows me to insert a hexcode (as a string) and convert it into a Color object.
}