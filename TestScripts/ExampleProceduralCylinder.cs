//Adapted from a script from ZeroKelvinTutorials about donut Generation
//tutorial video at https://youtu.be/mgjIoyvVvyM

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class ExampleProceduralCylinder : MonoBehaviour
{
    private Mesh _mesh;
    Vector3[] _vertices; //these are stored to avoid recalculating every time
    Vector2[] _uvs;
    int[] _triangles;

    public Vector3 CylinderCenter;
    public float CylinderHeight;
    public int CylinderSides; //cylinder resolution
    public int CircleSides; //circle resolution
    public float CircleRadius; //cylinder thickness
    public Texture[] textures;
    public Shader shader;
    //public Vector3 CylinderRotation;

    //cache previous variables
    //need to find a cleaner way to do this
    //maybe a generic that holds two states?    
    Vector3 lastCylinderCenter;
    int lastCylinderSides;
    int lastCircleSides;
    float lastCircleRadius;
    float lastCylinderHeight;
    private int counter;
    //Vector3 lastCylinderRotation;

    void Start()
    {

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Renderer rend = GetComponent<Renderer>();
        Material[] mats = rend.materials;
        _mesh = new Mesh();
        
        _vertices = GenerateCylinderVertices();

        _triangles = GenerateCylinderTriangles(_vertices);
        //_uvs = GenerateCylinderUvs(_vertices);

        //_rotatedvertices = RotateVertices(_vertices, CylinderRotation);
        Debug.Log("vertices " + _vertices.Length);
        Debug.Log("Uvs " + _uvs.Length);

        _mesh.vertices = _vertices;
        _mesh.uv = _uvs;    
        _mesh.triangles = _triangles;

        counter = 0;
        for (int i = 0; i < textures.Length; i++)
        {
            counter += 6;
            //Renderer rend = gameObject.transform.GetChild(i).
            mats[i] = new Material(shader)
            {
                mainTexture = textures[i]
            };
            Debug.Log("before " + _mesh.subMeshCount);
            _mesh.subMeshCount = textures.Length;
            Debug.Log("after " + _mesh.subMeshCount);
            _mesh.SetTriangles(_triangles, counter, 6, i);
        }
        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
            
        
        meshFilter.mesh = _mesh;
        rend.materials = mats;
    }

    void Update()
    {
        //ideally handle origin shift separately as well
        OverwriteLastVariables(out bool variablesChanged, out bool trianglesChanged/*, out bool rotationChanged*/);
        if (variablesChanged)
        {
            _vertices = GenerateCylinderVertices();
            _mesh.vertices = _vertices;
        }
        if (trianglesChanged)
        {
            _mesh.subMeshCount = lastCircleSides * lastCylinderSides;
            // Debug.Log(_mesh.subMeshCount);
            _triangles = GenerateCylinderTriangles(_vertices); //triangles only change if CircleSides/cylinderSides changes
            _mesh.triangles = _triangles;
        }
        /*if (variablesChanged || rotationChanged)
        {
            _rotatedvertices = RotateVertices(_vertices, CylinderRotation);
            _mesh.vertices = _rotatedvertices;
        }*/
        if (trianglesChanged)
        {
            _mesh.triangles = _triangles;
        }
    }

    void OverwriteLastVariables(out bool variablesChanged, out bool trianglesChanged/*,out bool rotationChanged*/)
    {
        variablesChanged = false;
        trianglesChanged = false;
        //rotationChanged = false;
        if (lastCylinderSides != CylinderSides || lastCircleSides != CircleSides || lastCylinderHeight != CylinderHeight)
        {
            variablesChanged = true;
            trianglesChanged = true;
            //rotationChanged = true;
            lastCylinderSides = CylinderSides;
            lastCircleSides = CircleSides;
            lastCylinderHeight = CylinderHeight;
        }
        if (lastCylinderCenter != CylinderCenter || lastCircleRadius != CircleRadius)
        {
            variablesChanged = true;
            lastCylinderCenter = CylinderCenter;
            lastCircleRadius = CircleRadius;
        }
        /*if (lastCylinderRotation != CylinderRotation)
        {
            rotationChanged = true;
            lastCylinderRotation = CylinderRotation;
        }*/
    }
    Vector3[] GenerateCylinderVertices()
    {
        //make a circle shifted _cylinderRadius amount away from _cylinderCenter to the right 
        //this makes it so that the circle faces are perpendicular to the line between it and cylinder center
        Vector3[] circleVertices = GenerateCircleVertices(/*Vector3.right * DonutRadius + */CylinderCenter);

        //make a copy of the circle rotated/positioned for each cylinder step and add it to a vertices list
        List<Vector3> cylinderVertices = new List<Vector3>();
        //float degreesPerStep = (float)360 / CylinderSides;
        float heightPerStep = CylinderHeight / CylinderSides;
        bool a, b = false;
        for (int i = 0; i < CylinderSides; i++)
        {
            //float currentDegree = degreesPerStep * i;
            float currentHeight = heightPerStep *i;
            Debug.Log(currentHeight);
            foreach (Vector3 vertex in circleVertices)
            {   
                //Vector3 rotationVector = Vector3.forward * currentHeight;
                //Quaternion rotationQuaternion = Quaternion.Euler(rotationVector);

                //rotate the vertices of the circle template
                Vector3 newVertex = vertex;//(vertex - CylinderCenter) + CylinderCenter;
                newVertex.z += currentHeight;
                cylinderVertices.Add(newVertex);
            }
        }
        return cylinderVertices.ToArray();
    }

    //generates an n-polygon of n = _circleSides and radius = _circleRadius centered at circleCenter
    Vector3[] GenerateCircleVertices(Vector3 circleCenter)
    {
        Vector3[] vertices = new Vector3[CircleSides];
        float circumferenceProgressPerStep = (float)1 / CircleSides;
        float TAU = 2 * Mathf.PI;
        float radianProgressPerStep = circumferenceProgressPerStep * TAU;

        for (int i = 0; i < CircleSides; i++)
        {
            float currentRadian = radianProgressPerStep * i;
            float x = circleCenter.x + Mathf.Cos(currentRadian) * CircleRadius;
            float y = circleCenter.y + Mathf.Sin(currentRadian) * CircleRadius;
            float z = circleCenter.z;
            vertices[i] = new Vector3(x, y, z);
        }
        return vertices;
    }

    int[] GenerateCylinderTriangles(Vector3[] vertices)
    {
        List<int> triangleIndexes = new List<int>();
        Vector2[] uvs = new Vector2[vertices.Length];
        int n = CircleSides;
        for (int circle = 0; circle < CylinderSides; circle++)
        {
            for (int i = 0; i < n; i++)
            {
                //calculate indexes of first circle
                int firstOfFirstCircle = circle * n;
                int firstIndex = firstOfFirstCircle + i;
                int secondIndex = firstOfFirstCircle + (i + 1) % n;

                //calculate indexes of second circle
                int firstOfSecondCircle = ((circle + 1) % CylinderSides) * n;
                int thirdIndex = firstOfSecondCircle + i;
                int fourthIndex = firstOfSecondCircle + ((i + 1) % n);

                //triangle 1
                triangleIndexes.Add(firstIndex);    // 4|\   
                triangleIndexes.Add(thirdIndex);    //  | \
                triangleIndexes.Add(fourthIndex);   // 3|__\1

                //triangle 2
                triangleIndexes.Add(firstIndex);    // 4\--|2
                triangleIndexes.Add(fourthIndex);   //   \ |
                triangleIndexes.Add(secondIndex);   //    \|1

                uvs[thirdIndex] = new Vector2(0, 0);
                uvs[firstIndex] = new Vector2(0, 1);
                uvs[fourthIndex] = new Vector2(1, 0);
                uvs[secondIndex] = new Vector2(1, 1);

            }
        }
        _uvs = uvs;
        return triangleIndexes.ToArray();
    }
    Vector2[] GenerateCylinderUvs(Vector3[] vertices)
    {
        List<Vector2> uvs = new List<Vector2>();
        for (int i = 0; i < vertices.Length; i+=4)
        {
            uvs.Add(new Vector2(0,0));
            uvs.Add(new Vector2(1, 0));
            uvs.Add(new Vector2(0, 1));
            uvs.Add(new Vector2(1, 1));
        }
             
        return uvs.ToArray();
    }
    /*Vector3[] RotateVertices(Vector3[] vertices, Vector3 cylinderRotation)
    {
        Vector3[] rotatedVertices = vertices.Clone() as Vector3[];
        Quaternion cylinderRotationQuaternion = Quaternion.Euler(cylinderRotation);
        for (int i = 0; i < vertices.Length; i++)
        {
            rotatedVertices[i] = cylinderRotationQuaternion * (vertices[i] - CylinderCenter) + CylinderCenter;
        }
        return rotatedVertices;
    }*/
}