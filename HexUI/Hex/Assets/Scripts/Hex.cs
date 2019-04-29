using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex{

	public Hex (int q, int r){
		this.Q = q;
		this.R = r;
		this.S = -(q + r);
	}


	// Q + R + S = 0
	// S = -(Q + R)

	public readonly int Q; // x
	public readonly int R;	// y
	public readonly int S;

	static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;


	public Vector3 Position(){
		float radius = 0.513f;
		float height = radius * 2;
		float width = WIDTH_MULTIPLIER * height;

		float vert = height * 0.75f;
		float horiz = width;

		return new Vector3(horiz * (this.Q - this.R/2f), vert * this.R, 0);
	}


}


