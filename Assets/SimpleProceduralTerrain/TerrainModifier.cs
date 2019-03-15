#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

public class TerrainModifier: ScriptableWizard
{
    #region Field and Property
    [Tooltip("Target modify terrain data.")]
    public TerrainData terrainData;

    [Tooltip("Raise height base on current.")]
    public float raiseHeight = 0;
    #endregion

    #region Private Method
    [MenuItem("Tool/Terrain Modifier &T")]
    private static void ShowEditor()
    {
        DisplayWizard("Terrain Modifier", typeof(TerrainModifier), "Modify");
    }

    private void OnWizardUpdate()
    {
        if (terrainData)
        {
            isValid = true;
        }
        else
        {
            isValid = false;
        }
    }

    private void OnWizardCreate()
    {
        var modify = EditorUtility.DisplayDialog(
            "Modify Confirm",
            "Modify operate can not be recovered.\n" +
            "Make sure you have a backup of target terrain data.\n" +
            "Don't set a negative value to the Raise Height unless you know terrain data inside out.",
            "Modify",
            "Cancle");
        if (modify)
        {
            var heights = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);
          
            raiseHeight = Mathf.Clamp(raiseHeight, -terrainData.bounds.center.y, terrainData.size.y - terrainData.bounds.center.y);

            var relativeRaiseHeight = raiseHeight / terrainData.size.y;
            for (int w = 0; w < terrainData.heightmapWidth; w++)
            {
                for (int h = 0; h < terrainData.heightmapHeight; h++)
                {
                    heights[w, h] += relativeRaiseHeight;
                }
            }
            terrainData.SetHeights(0, 0, heights);
        }
    }
    #endregion
}
#endif