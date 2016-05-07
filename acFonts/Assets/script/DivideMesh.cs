/*Devide Mesh*/

using UnityEngine;
using System.Collections;

public class DivideMesh : MonoBehaviour {

	public int resolution;
	private Mesh mesh;
	private int masterResolution;
	public int[] triangles;

	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter>().mesh;
		masterResolution = mesh.vertices.Length;
		print (masterResolution);

		triangles = mesh.triangles;
		mesh.SetIndices(MakeIndices(), MeshTopology.Lines, 0);
		//DivideMesh ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void DevideMesh(){
		//変更後の頂点
		Vector3[] vertices = new Vector3[masterResolution + resolution * 10];

		/*
		mesh.vertices[0]
		mesh.vertices[1]
		vertices[2] = (mesh.vertices[0] + mesh.vertices[1])/2
			Vertices[3] = mesh.VerticesCircle[2] 
			Vertices[4] = mesh.[3]
			Vertices[5] = 2 + 3 /2
			6 = 4 
		*/
	}

	public int[] MakeIndices()
	{
		int[] indices = new int[2 * triangles.Length];
		int i = 0;
		for( int t = 0; t < triangles.Length; t+=3 )
		{
			indices[i++] = triangles[t];        //start
			indices[i++] = triangles[t + 1];   //end
			indices[i++] = triangles[t + 1];   //start
			indices[i++] = triangles[t + 2];   //end
			indices[i++] = triangles[t + 2];   //start
			indices[i++] = triangles[t];        //end
		}
		return indices;
	}
}
