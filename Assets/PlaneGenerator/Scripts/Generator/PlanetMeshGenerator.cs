using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class PlanetMeshGenerator : MonoBehaviour
{
    public Mesh SourceMesh;
    public bool GenerateOnlyOnPlayMode = true;
    public float PlanetRadius = 1f;
    public float HeightScale = 1f;
    public float HeightOffset;
    public float PerlinScale = 1f;
    public Vector2 PerlinOffset = new Vector2(100f,100f);
    private MeshFilter meshFilter
    {
        get
        {
            if (_meshFilter == null)
                _meshFilter = GetComponent<MeshFilter>();
            return _meshFilter;
        }
    }
    private MeshFilter _meshFilter;

    [NaughtyAttributes.Button()]
    private void Generate()
    {
        var vert = SourceMesh.vertices;
        
        for (int i = 0; i < vert.Length; i++)
        {
            SphereCoords sp = SphereCoords.Cartesian2Sphere(vert[i]);
            var perlinNoise = Mathf.PerlinNoise(sp.L * PerlinScale + PerlinOffset.x,sp.W * PerlinScale + PerlinOffset.y) + HeightOffset;
            perlinNoise = perlinNoise < 0f ? 0f : perlinNoise;
            sp.R *=  perlinNoise * HeightScale + PlanetRadius;
            vert[i] = sp.cartesian;
        }
        
        
        meshFilter.mesh.vertices = vert; // Присваиваем вершины
        meshFilter.mesh.RecalculateBounds(); // Обновляем вершины
        meshFilter.mesh.RecalculateNormals(); // Обновляем нормали
    }

    private void OnValidate()
    {       
        if(GenerateOnlyOnPlayMode && !Application.isPlaying)
            return;

        Generate();
    }
}
