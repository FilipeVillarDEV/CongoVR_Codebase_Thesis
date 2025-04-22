using System.Collections.Generic;
using UnityEngine;

public class ProceduralCylinder  : MonoBehaviour
{
    private Mesh _mesh;
    MeshFilter _meshFilter;
    MeshRenderer _rend;
    private GameObject _faceGameObject;
    private Material _mat;
    private Vector3[] _vertices; //these are stored to avoid recalculating every time
    private Vector2[] _uvs;
    private int[] _triangles;
    private Vector3 _firstVertice, _nextVertice;

    public Vector3 CylinderCenter;
    public float CylinderHeight;
    public int CylinderSides; //cylinder resolution
    public int CircleSides; //circle resolution
    public float CircleRadius; //cylinder thickness
    public Texture[] textures;
    public Shader shader;

    private Vector3 lastCylinderCenter;
    private int lastCylinderSides;
    private int lastCircleSides;
    private float lastCircleRadius;
    private float lastCylinderHeight;


    // Start is called before the first frame update
    void Start()
    {
        for (int k = 0; k < CylinderSides; k++)
        {
            for (int i = 0; i < CircleSides; i++)
            {
                _faceGameObject = GenerateFaceGameObject(k,i);

                _meshFilter = _faceGameObject.GetComponent<MeshFilter>();
                _rend = _faceGameObject.GetComponent<MeshRenderer>();
                _mesh = new Mesh();


                _vertices = GenerateFaceVertices(k,i);
                _triangles = new int[6] { 0, 1, 3, 0, 3, 2 };

                if (CylinderSides * CircleSides != textures.Length)
                    Debug.LogError("The number of faces isnt the same than the number of textures!!");
                _mat = new Material(shader)
                {
                    mainTexture = textures[ k * CircleSides + i]
                };

                _mesh.vertices = _vertices;
                _mesh.triangles = _triangles;
                _mesh.uv = _uvs;

                _meshFilter.mesh = _mesh;
                _rend.material = _mat;
                _firstVertice = _nextVertice;
            }
        }
    }
    private GameObject GenerateFaceGameObject(int k, int i)
    {
        GameObject faceGameObject = new GameObject();
        faceGameObject.name = "face " + (k * CircleSides + i);
        faceGameObject.AddComponent<MeshFilter>();
        faceGameObject.AddComponent<MeshRenderer>();
        faceGameObject.transform.parent = gameObject.transform;

        return faceGameObject;
    }
    private Vector3[] GenerateFaceVertices(int cylinderStep, int circleStep)
    {
        Vector3[] faceVertices = new Vector3[4];
        _uvs = new Vector2[4];

        float circumferenceProgressPerStep = (float)1 / CircleSides;
        float TAU = 2 * Mathf.PI;
        float radianProgressPerStep = circumferenceProgressPerStep * TAU;
        float currentRadian = radianProgressPerStep * (circleStep + 1);

        float heightPerStep =  -CylinderHeight / CylinderSides;
        float endPerStep = -Mathf.Sin(currentRadian) * CircleRadius;
        float startPerStep = Mathf.Cos(currentRadian) * CircleRadius;

        if (circleStep == 0)
            _firstVertice = new Vector3(Mathf.Cos(radianProgressPerStep * circleStep) * CircleRadius + CylinderCenter.x,
                                         heightPerStep * (cylinderStep) + CylinderCenter.y,
                                        -Mathf.Sin(radianProgressPerStep * circleStep) * CircleRadius + CylinderCenter.z);

        faceVertices[0] = _firstVertice;
        faceVertices[1] = new Vector3(startPerStep, _firstVertice.y, endPerStep);
        faceVertices[2] = new Vector3(_firstVertice.x, heightPerStep + _firstVertice.y, _firstVertice.z);
        faceVertices[3] = new Vector3(startPerStep, heightPerStep + _firstVertice.y, endPerStep);

        //Add Uvs to the face
        _uvs[0] = new Vector2(0,1);
        _uvs[1] = new Vector2(1,1);
        _uvs[2] = new Vector2(0,0);
        _uvs[3] = new Vector2(1,0);


        _nextVertice = new Vector3(startPerStep + CylinderCenter.x, heightPerStep * cylinderStep + CylinderCenter.y, endPerStep + CylinderCenter.z);

        if (circleStep + 1 == CircleSides)
        {
            //horizontal
            _uvs[1] = new Vector2(0.9404296875f, 1);    //this value is the result of the operation 1 - 102/2048 (=0.9404296875f), 1k = 1- 122/1024 (=0.880859375), 256 = 1- 122/256(=0.5234375)
            _uvs[3] = new Vector2(0.9404296875f, 0);    //being 102 the number of pixels in black for the 2k scenario

            _nextVertice = new Vector3(startPerStep, heightPerStep * (cylinderStep + 1), 0);
        }

        if (cylinderStep + 1 == CylinderSides) 
        {
            //vertical
            _uvs[2] = new Vector2(0, 0.3935546875f);    //this value is the result of the operation 806/2048 (= 0.3935546875f), 806/1024 (=0.787109375), 38/256 (=0.1484375) 
            _uvs[3] = new Vector2(1, 0.3935546875f);    //being 806 the number of pixels in black for the 2k scenario

            if (circleStep + 1 == CircleSides)
                _uvs[3] = new Vector2(0.9404296875f, 0.3935546875f); // for this square its necessary to adjust both x and y uvs values
                                                                     // (0.9404296875f, 0.3935546875f) for the 2k scenario
        }

        return faceVertices;
    }
}
