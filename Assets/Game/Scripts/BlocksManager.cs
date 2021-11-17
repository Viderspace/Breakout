using System.Collections.Generic;
using UnityEngine;


public class BlocksManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject brickPrefab = default;

    [SerializeField] [Range(0.001f, 1f)] private float animationTime = 0.2f;

    #endregion


    #region Fields

    private static BlocksManager _shared;

    private readonly List<GameObject> _bricks = new List<GameObject>();


    //parameters for blocks animation
    private int _diagonalIndex = 0;
    private float _remaining = 0f;


    //parameters for creating a grid of block   (rows, columns, the spacing between them in the grid)
    private int rows = 5;
    private int cols = 11;
    private float horizonSpacing = 0.1f;
    private float verticSpacing = 0.2f;

    // to keep track of the current state of the grid
    private int _blocksRemaining = 55;

    public int BlocksRemaining
    {
        get => _blocksRemaining;
        private set => _blocksRemaining -= value;
    }

    #endregion


    #region Methods

    public void ReduceBlock(GameObject block)
    {
        _bricks.Remove(block);
        _blocksRemaining -= 1;
        if (_blocksRemaining != 0) return;
        Debug.Log("WIN!");
        ResetLevel();
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
        foreach (var brick in _bricks)
        {
            Destroy(brick);
        }

        _bricks.Clear();


        for (var y = 0; y < rows; y++)
        {
            var color = GetColorFromString(_rowColors[y]);
            for (var x = 0; x < cols; x++)
            {
                var localScale = brickPrefab.transform.localScale;
                var spawnPos = (Vector2) transform.position + new Vector2(
                    x * (localScale.x + horizonSpacing),
                    -y * (localScale.y + verticSpacing));

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
        _shared = this;
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
        _remaining = animationTime;
    }

    #endregion


    #region Color Conversion

    // these are the colors i picked:
    private readonly List<string> _rowColors = new List<string> {"E6F07D", "F0C5BD", "F0A5D5", "C48DF0", "99F0CD"};


    // helper functions: 
    private static int HexToDec(string hex)
    {
        int dec = System.Convert.ToInt32(hex, 16);
        return dec;
    }

    private static string DecToHex(int value)
    {
        return value.ToString("X2");
    }

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