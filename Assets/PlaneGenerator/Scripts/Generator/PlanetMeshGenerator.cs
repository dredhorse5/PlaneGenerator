using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class PlanetMeshGenerator : MonoBehaviour
{
    public Mesh SourceMesh;
    public bool GenerateOnlyOnPlayMode = true;
    public float PlanetRadius = 1f;
    
    [Header("Height")]
    public float HeightScale = 1f;
    public float HeightOffset;
    public float MaxHeight = 1f;
    
    [Header("Perlin")]
    public Vector2 PerlinOffset = new Vector2(100f,100f);
    public float PerlinScale = 1f;

    [Header("Texture")]
    public Color Color1 = Color.green;
    public Color Color2 = Color.magenta;
    
    
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
            
            float noice_X = sp.L * PerlinScale + PerlinOffset.x;
            float noice_Y = Mathf.Sin(sp.W * Mathf.Deg2Rad) * sp.W * PerlinScale + PerlinOffset.y;
            
            var perlinNoise = Mathf.PerlinNoise(noice_X,noice_Y) + HeightOffset;
            
            perlinNoise = perlinNoise < 0f ? 0f : perlinNoise;
            perlinNoise = perlinNoise > MaxHeight ? MaxHeight : perlinNoise;  
            
            sp.R *=  perlinNoise * HeightScale + PlanetRadius;
            vert[i] = sp.cartesian;
        }
        
        
        meshFilter.mesh.vertices = vert;
        meshFilter.mesh.RecalculateBounds();
        meshFilter.mesh.RecalculateNormals();
    }

    public void GenerateTexture()
    {
        Color1
    }
    
    private void OnValidate()
    {       
        if(GenerateOnlyOnPlayMode && !Application.isPlaying)
            return;

        Generate();
    }
}
