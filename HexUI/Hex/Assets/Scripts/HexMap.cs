using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GenerateMap();
	}

	public GameObject HexPrefab;

	public void GenerateMap ()
	{
		for (int x = 0; x < 11; x++) { 
			for (int y = 0; y < 11; y++) { 
				// Instantiate a Hex
				Hex h = new Hex(x, y);

				Instantiate (HexPrefab, h.Position(), Quaternion.identity, this.transform);
			}
		}	
	}

}
