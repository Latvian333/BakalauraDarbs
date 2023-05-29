using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMeshGenerator : MonoBehaviour
{
    public float roadWidth = 2f;    // The width of the road
    public float roadThickness = 0.1f;  // The thickness of the road
    public float roadHeight = 0.1f; // The height of the road
    public Material roadMaterial;   // The material to use for the road

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        // Get or add the MeshFilter component
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }

        // Get or add the MeshRenderer component
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }

        // Create the road mesh
        meshFilter.mesh = CreateRoadMesh();
        meshRenderer.material = roadMaterial;
    }

    Mesh CreateRoadMesh()
    {
        Mesh mesh = new Mesh();

        // Vertices
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(-roadWidth / 2f, roadHeight, 0f);
        vertices[1] = new Vector3(-roadWidth / 2f, 0f, 0f);
        vertices[2] = new Vector3(roadWidth / 2f, 0f, 0f);
        vertices[3] = new Vector3(roadWidth / 2f, roadHeight, 0f);

        // Triangles
        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        // Normals
        Vector3[] normals = new Vector3[4];
        normals[0] = Vector3.up;
        normals[1] = Vector3.up;
        normals[2] = Vector3.up;
        normals[3] = Vector3.up;

        // UVs
        Vector2[] uv = new Vector2[4];
        uv[0] = new Vector2(0f, 1f);
        uv[1] = new Vector2(0f, 0f);
        uv[2] = new Vector2(1f, 0f);
        uv[3] = new Vector2(1f, 1f);

        // Assign the arrays to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        // Scale the mesh to the correct size
        mesh.RecalculateBounds();
        Vector3 scale = new Vector3(1f, roadThickness, 1f);
        mesh.bounds = new Bounds(mesh.bounds.center, Vector3.Scale(mesh.bounds.size, scale));

        return mesh;
    }
}
