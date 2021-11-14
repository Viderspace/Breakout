using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksManager : MonoBehaviour
{
    private int rows = 5;
    private int cols = 11;
    public float horizonSpacing;
    public float verticSpacing;
    public GameObject brickPrefab = default;
    private List<GameObject> bricks = new List<GameObject>();
    
    private int blocksRemaining = 55;

    public void ReduceBlock()
    {
        blocksRemaining -= 1;
        if (blocksRemaining == 0)
        {
            Debug.Log("WIN!");
            ResetLevel();
        }
        // Debug.Log($"Blocks remaining: %{blocksRemaining}");

    }
    
    public void ResetLevel()
    {
        foreach (GameObject brick in bricks)
        {
            Destroy(brick);
        }
        bricks.Clear();

        for (int y = 0; y < rows; y++)
        {
            Color color = GetColorFromString(rowColors[y]);
            for (int x = 0; x < cols; x++) 
            {
                
                Vector2 spawnPos = (Vector2)transform.position + new Vector2(
                    x * (brickPrefab.transform.localScale.x + horizonSpacing),
                    -y * (brickPrefab.transform.localScale.y + verticSpacing));
                
                GameObject brick = Instantiate(brickPrefab, spawnPos, Quaternion.identity);
                if (y == 2) { brick.tag = "MidRowBlock"; } // special tag for all middle blocks

                BlockBehaivior blockBehaivior = brick.GetComponent<BlockBehaivior>();
                blockBehaivior.SetColor(color);
                bricks.Add(brick);
            }
        }
    }

    
    
    
    // I wanted to be able to change the rows colors easily so i made this set of functions,
    // so now i can just insert a hexcode (as a string) and get a Color object from it.
    #region Color Conversion
    
    // these are the colors i picked:
    private List<string> rowColors = new List<string>  {"E6F07D", "F0C5BD", "F0A5D5", "C48DF0", "99F0CD"};
    

    
    // helper functions: 
    private int HexToDec(string hex)
    {
        int dec = System.Convert.ToInt32(hex, 16);
        return dec;
    }
    
    private string DecToHex(int value)
    {
        return value.ToString("X2");
    }
    
    private string FloatNormalizedToHex(float value)
    {
        return DecToHex(Mathf.RoundToInt(value * 255f));
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
    

}
