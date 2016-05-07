//Move each vertices like drawing circle

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VerticesCircle : MonoBehaviour {

	public float verticeRadius;
	public Vector3[] baseVerticles;
	public float ThetaScale = 0.01f;
	private float Theta = 0f;

	// Use this for initialization
	void Awake () {
		//Understand the values of each verticles, uv, and triangles
		Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
		baseVerticles = new Vector3[mesh.vertices.Length];

		//Save the initial vertices
		baseVerticles = mesh.vertices;

	}
	
	// Update is called once per frame
	void Update () {
		Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
		Vector3[] newVerticles = new Vector3[mesh.vertices.Length];
		Theta += (2.0f * Mathf.PI * ThetaScale);         
		float circleX = verticeRadius * Mathf.Cos(Theta);
		float circleY = verticeRadius * Mathf.Sin(Theta);   

		for (int i = 0; i < mesh.vertices.Length; i++) {
			newVerticles [i] = new Vector3 (baseVerticles[i].x + circleX, baseVerticles[i].y + circleY, baseVerticles[i].z);
		}
			
		mesh.vertices = newVerticles;
	
	}
}
