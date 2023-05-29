using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownPlacement : MonoBehaviour
{
    public Terrain terrain;
    public Texture2D townmap;

    public GameObject townPrefab;

    public float minTownHeight;
    public float maxTownHeight;

    private int counter;

    public float minTownRadius;

    Dictionary<int, string> cities = new Dictionary<int, string>()
        {
        {0,"Liepāja"},
        {1,"Ventspils"},
        {2,"Jelgava"},
        {3,"Rīga"},
        {4,"Valmiera"},
        {5,"Jēkabpils"},
        {6,"Daugavpils"},
        {7,"Rēzekne"}
        };

    void Awake()
    {
        float[,] heightmap = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);
        Color[] pixelData = townmap.GetPixels();
        float scaleX = (float)terrain.terrainData.heightmapResolution / (float)townmap.width;
        float scaleZ = (float)terrain.terrainData.heightmapResolution / (float)townmap.height;
        Transform parent = transform;
        HashSet<Vector2Int> placedTowns = new HashSet<Vector2Int>();

        for (int x = 0; x < terrain.terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrain.terrainData.heightmapResolution; z++)
            {
                Color pixelColor = pixelData[(int)(x / scaleX) + (int)(z / scaleZ) * townmap.width];

                if (pixelColor.r >= 1)
                {
                    float townValue = pixelColor.r;
                    float townHeight = Mathf.Lerp(minTownHeight, maxTownHeight, townValue);

                    Vector3 position = new Vector3(x, 0f, z);
                    float terrainHeight = terrain.SampleHeight(position);
                    position.y = terrainHeight + townHeight / 2f;
                    if (position.y > terrainHeight + 0.1f && !IsWithinRadiusOfTown(x, z, placedTowns, minTownRadius))
                    {
                        GameObject city = Instantiate(townPrefab, position, Quaternion.identity, parent);
                        city.name = cities[counter];
                        counter++;
                    }
                    MarkTownAsPlaced(x, z, placedTowns);
                    }
                }
            }
        }
    

    bool IsWithinRadiusOfTown(int x, int z, HashSet<Vector2Int> placedTowns, float minRadius)
    {
        foreach (Vector2Int town in placedTowns)
        {
            float distance = Vector2Int.Distance(new Vector2Int(x, z), town);
            if (distance < minRadius)
            {
                return true;
            }
        }
        return false;
    }

    void MarkTownAsPlaced(int x, int z, HashSet<Vector2Int> placedTowns)
    {
        Vector2Int coordinates = new Vector2Int(x, z);
        placedTowns.Add(coordinates);
    }
   
}