
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class TerrainToTexture 
{
    public static void Save(TerrainData terrainData, string dir)
    {
        if(terrainData)
        {
            if(dir.EndsWith("/")==false)
            {
                dir += "/";
            }
            if(Directory.Exists(dir)==false)
            {
                Directory.CreateDirectory(dir);
            }
            for (int i = 0; i < terrainData.alphamapTextures.Length; ++i)
            {
                var texture = terrainData.alphamapTextures[i];
                string saveFile = dir + texture.name + ".png";

                saveTextureToPng(saveFile, texture);
            }
        }
    }
    public static bool saveTextureToPng(string filePath,Texture2D texture)
    {  
        if (texture == null)
        {
            return false;
        }
        Texture2D savedTexture = texture;
        try
        {
            Texture2D newTexture = new Texture2D(savedTexture.width, savedTexture.height, texture.format, false);
            newTexture.SetPixels(0, 0, savedTexture.width, savedTexture.height, savedTexture.GetPixels());
            newTexture.Apply();
            byte[] bytes = newTexture.EncodeToPNG();
            if (bytes != null && bytes.Length > 0)
            {
                File.WriteAllBytes(filePath, bytes);
                Debug.Log("保存成功:" + filePath);
            }
        }
        catch (IOException ex)
        {
           
            return false;
        }

        return true;
    }

}


