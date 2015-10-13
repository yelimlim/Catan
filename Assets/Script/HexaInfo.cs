using UnityEngine;
using System.Collections;

public class HexaInfo : MonoBehaviour 
{
    public Vector3[] Vertices;
    public Vector2[] uv;
    public int[] Triangles;

    public Texture texture;
    MeshFilter meshFilter = null;

	void Start () 
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();

        MeshSetUp();
	}

    void MeshSetUp()
    {
        float floorLevel = 0;
        Vertices = new Vector3[]
        {
            new Vector3(-1.0f, floorLevel, -0.5f),
            new Vector3(-1.0f, floorLevel, 0.5f),
            new Vector3(0.0f, floorLevel, 1.0f),
            new Vector3(1.0f, floorLevel, 0.5f),
            new Vector3(1.0f, floorLevel, -0.5f),
            new Vector3(0.0f, floorLevel, -1.0f)
        };

        Triangles = new int[]
        {
            1,5,0,
            1,4,5,
            1,2,4,
            2,3,4
        };

        uv = new Vector2[]
        {
            new Vector2(0.0f,0.25f),
            new Vector2(0.0f,0.75f),
            new Vector2(0.5f,1.0f),
            new Vector2(1.0f,0.75f),
            new Vector2(1.0f,0.25f),
            new Vector2(0.5f,0.0f)
        };

        Mesh mesh = new Mesh();
        mesh.vertices = Vertices;
        mesh.triangles = Triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
        GetComponent<Renderer>().material.mainTexture = texture;
    }

}
