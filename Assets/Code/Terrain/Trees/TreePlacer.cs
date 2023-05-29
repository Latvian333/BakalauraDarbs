using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class TreePlacer : MonoBehaviour
{
    public Terrain terrain;
    public Texture2D vegetationMap;
    public GameObject[] treePrefabs;
    public float minTreeHeight;
    public float maxTreeHeight;
    public float treeProbability;
    public float offset;

    




    void Awake()
    {
        float[,] heightmap = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);
        Color[] pixelData = vegetationMap.GetPixels();
        float scaleX = (float)terrain.terrainData.heightmapResolution / (float)vegetationMap.width;
        float scaleZ = (float)terrain.terrainData.heightmapResolution / (float)vegetationMap.height;
        Transform parent = transform;

        for (int x = 0; x < terrain.terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrain.terrainData.heightmapResolution; z++)
            {
                Color pixelColor = pixelData[(int)(x / scaleX) + (int)(z / scaleZ) * vegetationMap.width];
                if (pixelColor.g > pixelColor.r && pixelColor.g > pixelColor.b)
                {
                        float vegetationValue = pixelColor.g;
                        float treeHeight = Mathf.Lerp(minTreeHeight, maxTreeHeight, vegetationValue);
                        float probability = UnityEngine.Random.Range(0f, 1f);
                        if (probability < treeProbability)
                        {
                            GameObject treePrefab = treePrefabs[UnityEngine.Random.Range(0, treePrefabs.Length)];
                            Vector3 position = new Vector3(x, 0f, z);
                            float terrainHeight = terrain.SampleHeight(position);
                            position.y = terrainHeight + treeHeight / 2f - offset;
                            if (position.y > terrainHeight + 0.1f)
                            {
                                    GameObject tree = Instantiate(treePrefab, position, Quaternion.identity, parent);
                            }

                        }
                    
                }
            }
        }
    }


    void Start()
    {
        // Set the radius of the overlap sphere
        float radius = 1f;

        // Check if the object colli es with any Water objects in the specified radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Water"))
            {
                Destroy(gameObject);
            }

            if (hitCollider.gameObject.CompareTag("Roads"))
            {
                UnityEngine.Debug.Log("Hit A Road");
                Destroy(gameObject);
            }
        }
    }
}
