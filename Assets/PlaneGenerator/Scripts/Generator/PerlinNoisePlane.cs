using System;
using UnityEngine;
using System.Collections;
 
public class PerlinNoisePlane : MonoBehaviour
{
    public bool OnlyOnPlayMode = true;
    public Vector2 Offset;
    public float power = 3.0f;
    public float scale = 1.0f;
 
    void Start () 
    {
        MakeNoise();
    }

    void MakeNoise()
    {
        MeshFilter mf = GetComponent<MeshFilter>(); // Ищем mesh
        Vector3[] vertices = mf.mesh.vertices; // Получаем его вершины
        
        for (int i = 0; i < vertices.Length; i++)
        {
            float x = vertices[i].x * scale; // X координата вершины
            float z = vertices[i].z * scale; // Z координата вершины
            vertices[i].y =
                (Mathf.PerlinNoise(x + Offset.x, z + Offset.y) - 0.5f) * power; // Задаём высоту для точки с вышеуказанными координатами
        }

        mf.mesh.vertices = vertices; // Присваиваем вершины
        mf.mesh.RecalculateBounds(); // Обновляем вершины
        mf.mesh.RecalculateNormals(); // Обновляем нормали
    }

    private void OnValidate()
    {
        if(OnlyOnPlayMode && !Application.isPlaying)
            return;
        MakeNoise();
    }
}