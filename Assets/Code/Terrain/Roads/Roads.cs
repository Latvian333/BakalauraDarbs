using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Roads : MonoBehaviour
{
    public Terrain terrain; // reference to the terrain object
    public Texture2D roadMap; // the image that shows the road data

    public GameObject roadPrefab; // the prefab to use for the road
    public float roadHeight; // the height of the road above the terrain

    void Start()
    {
        float[,] heightmap = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);
        Color[] pixelData = roadMap.GetPixels();
        Transform parent = transform;
        float scaleX = (float)terrain.terrainData.heightmapResolution / (float)roadMap.width;
        float scaleZ = (float)terrain.terrainData.heightmapResolution / (float)roadMap.height;
        for (int x = 0; x < terrain.terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrain.terrainData.heightmapResolution; z++)
            {
                Color pixelColor = pixelData[(int)(x / scaleX) + (int)(z / scaleZ) * roadMap.width];
                if (pixelColor.r > pixelColor.g && pixelColor.r > pixelColor.b)
                {
                    float terrainHeight = heightmap[x, z];
                    Vector3 position = new Vector3(x, terrainHeight + roadHeight, z);
                    GameObject road = Instantiate(roadPrefab, position, Quaternion.identity, parent);
                }
            }
        }
    }
}