using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class ProceduralMeshExample : MonoBehaviour
{
    private Mesh mesh;
    public int width = 1;
    public int height = 1;
    public Vector3[] vertex;
    public Color[] vertexColor;
    
    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DrawParallel());
    }

    IEnumerator DrawParallel()
    {
        DrawMesh();
        yield return new WaitForEndOfFrame();
        StartCoroutine(DrawParallel());

          
    }
    void GenerateMesh()
    {
        mesh = new Mesh();
        GenerateTriangles();
        DrawMesh();
    }


    void GenerateTriangles()
    {

   vertex = new Vector3[(width+1)*(height+1)];
 

        for (int i=0,y = 0; y <= height; y++)
          for (int x = 0; x <= width; x++,i++)
            {
                 
                vertex[i] = new Vector3(x, y);
                 
          }

        int[] triangles = new int[6 * width];
        for (int ti = 0,vi=0,x=0; x < width; vi++,x++,ti+=6)
        {
            

                triangles[ti] = vi;
                triangles[ti+3] = triangles[ti+2]=vi+1;
                triangles[ti + 4] = triangles[ti + 1] = vi  +width+1;
            triangles[ti + 5] =   vi + width + 2;


        }



        mesh.vertices = vertex;
          mesh.triangles = triangles;

        vertexColor = new Color[mesh.colors.Length];
        for (int i = 0; i < vertexColor.Length; i++)
        {
            mesh.colors[i] = new Color(Random.value, Random.value, Random.value);

        }

        //mesh.uv
        mesh.RecalculateNormals();
    }

    void DrawMesh()
    {
        GetComponent<MeshFilter>().mesh = mesh;

    }

     
}
