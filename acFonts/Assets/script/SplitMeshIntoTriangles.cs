// C#
// SplitMeshIntoTriangles.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SplitMeshIntoTriangles : MonoBehaviour
{
	public int numberOfIndices;
	public Vector3 center;
	public Vector3 thePosition;

	IEnumerator SplitMesh ()
	{
		MeshFilter MF = GetComponent<MeshFilter>();
		MeshRenderer MR = GetComponent<MeshRenderer>();
		Mesh M = MF.mesh;
		Vector3[] verts = M.vertices;
		Vector3[] normals = M.normals;
		Vector2[] uvs = M.uv;
		for (int submesh = 0; submesh < M.subMeshCount; submesh++)
		{
			int[] indices = M.GetTriangles(submesh);
			numberOfIndices = (int)(indices.Length / 3);
			for (int i = 0; i < indices.Length; i += 3)
			{
				Vector3[] newVerts = new Vector3[3];
				Vector3[] newNormals = new Vector3[3];
				Vector2[] newUvs = new Vector2[3];
				for (int n = 0; n < 3; n++)
				{
					int index = indices[i + n];
					newVerts[n] = verts[index];
					print ("Verts[index]: " + verts [index]);
					newUvs[n] = uvs[index];
					newNormals[n] = normals[index];
				}
				Mesh mesh = new Mesh();
				mesh.vertices = newVerts;
				mesh.normals = newNormals;
				mesh.uv = newUvs;

				mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

				GameObject GO = new GameObject("Triangle " + (i / 3));
				GO.transform.position = transform.position;
				GO.transform.rotation = transform.rotation;
				GO.AddComponent<MeshRenderer>().material = MR.materials[submesh];
				GO.AddComponent<MeshFilter>().mesh = mesh;
				GO.AddComponent<BoxCollider>();

				//add Material to each game object
				//Material newMaterial = new Material(Shader.Find("vertex animation"));
				//AssetDatabase.CreateAsset(material, "Assets/" + Selection.activeGameObject.name + ".mat");

				//Destroy(GO, 100 + Random.Range(0.0f, 100.0f));
			}
		}
		MR.enabled = false;

		Time.timeScale = 0.2f;
		yield return new WaitForSeconds(0.8f);
		Time.timeScale = 1.0f;
		Destroy(gameObject);
	}

	void Start()
	{
		StartCoroutine(SplitMesh());
	}

	void Update(){
		
		GameObject test = GameObject.Find ("Triangle 1");
		test.transform.position += test.transform.position;
		//test.transform.rotation = 
		Mesh mesh = test.GetComponent<MeshFilter>().mesh;

		for(int i = 0; i < mesh.vertices.Length; i++)
		{
			print("vertices[" + i + "] : " + mesh.vertices[i]);

		}
			
	}

	//calculate the center of the letter
	void CalculateCenter(){
		Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
		float xMax = mesh.vertices.Max (v => v.x);
		float xMin = mesh.vertices.Min (v => v.x);
		float yMax = mesh.vertices.Max (v => v.y);
		float yMin = mesh.vertices.Min (v => v.y);
		float zMax = mesh.vertices.Max (v => v.z);
		float zMin = mesh.vertices.Min (v => v.z);
		float xCenter = (xMax + xMin) / 2;
		float yCenter = (yMax + yMin) / 2;
		float zCenter = (zMax + zMin) / 2;

		center = new Vector3 (xCenter, yCenter, zCenter);
		print (center);

	}
}