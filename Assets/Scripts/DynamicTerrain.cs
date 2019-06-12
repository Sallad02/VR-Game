
//Simon

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTerrain : MonoBehaviour {

	public int HeightMapResolution = 513;
	public int BaseMapResolution = 513;
	public int MaxHeight = 513;

	// Use this for initialization
	void Start () 
	{
		BuildTerrain ();
	}
	
	void BuildTerrain()
	{
		Terrain terrain = gameObject.AddComponent<Terrain> ();
		TerrainCollider terrainCollider = gameObject.AddComponent<TerrainCollider> (); //Adds terrain and terraincollider components to gameObject

		TerrainData terrainData = new TerrainData();

		BuildTerrainData (terrainData);

		terrain.terrainData = terrainData;
		terrainCollider.terrainData = terrainData;
	}

	public Texture2D heightMapTexture;
	public Texture2D TerrainTexture;

	void BuildTerrainData(TerrainData terrainData)
	{

		terrainData.heightmapResolution = HeightMapResolution; //amount of steps in height between the lowest and highest height
		terrainData.baseMapResolution = BaseMapResolution;
		terrainData.SetDetailResolution (1024, 32);

		terrainData.size = new Vector3 (heightMapTexture.width, MaxHeight, heightMapTexture.height);

		float[,] heights = terrainData.GetHeights (0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight); //Gets every int value in the terrain for height

		Color[] heightMapPixels = heightMapTexture.GetPixels (); //Gets every pixel in the image

		for (int x = 0; x < terrainData.heightmapWidth; x++)
		{
			for (int y = 0; y < terrainData.heightmapHeight; y++)
			{
				float xPos = (float)y / (float)terrainData.heightmapWidth;
				float yPos = (float)x / (float)terrainData.heightmapHeight;

				Color pix = heightMapPixels [
					Mathf.FloorToInt (xPos * heightMapTexture.width) +
					Mathf.FloorToInt (yPos * heightMapTexture.height) * heightMapTexture.width
				];

				heights [x, y] = pix.grayscale; //Changes every heights value to match the heightmaps values
			}
		}

		terrainData.SetHeights (0, 0, heights); //Sets the heights of the terrain to the new values
		SplatPrototype[] terrainTexture = new SplatPrototype[1];
		terrainTexture [0] = new SplatPrototype ();
		terrainTexture [0].texture = TerrainTexture;
		terrainTexture [0].tileSize = new Vector2(heightMapTexture.width, heightMapTexture.height); //Changes the size of the texture to stretch over the entire terrain
		terrainData.splatPrototypes = terrainTexture;
	}
}
