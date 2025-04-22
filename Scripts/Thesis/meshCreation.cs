using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshCreation : MonoBehaviour
{
    [SerializeField] Vector3[] newVertices;
    Vector2[] newUV;
    [SerializeField] int[] newTriangles;

    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;
    }
}
