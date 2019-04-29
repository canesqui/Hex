using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour {

	public Color[]colors; // allows input of material colors in a set sized araay
	public SpriteRenderer rend;  // what are we rendering? the hex

	public enum Player {ONE, TWO};
	public static Player currentPlayer = Player.ONE;

	//private int index = 1; //initialize at 1, otherwise you have to press the ball twice to change color


	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> (); // gives functionality for the renderer
	}

	void NextPlayer() {

       if( currentPlayer == Player.ONE ) {
          currentPlayer = Player.TWO;
       }
       else if( currentPlayer == Player.TWO) {
          currentPlayer = Player.ONE;
       }
    }
	
	// Update is called once per frame
	void OnMouseDown () {
	// if there are no colors present nothing happens
		if (colors.Length == 0){
		return;
		}
	
	//		index += 1; // when mouse is pressed down we incrememnt up to the next index location
			// when it reaches the end of the colors it stars over
	//		if (index == colors.Length +1){
	//			index = 1;
	//		}
	//		print (index); // used for debugging
	//		rend.color = colors [index - 1]; // this sets the material color values inside the index

		if (currentPlayer == Player.ONE)
			rend.color = colors[0];
		else if (currentPlayer == Player.TWO)
			rend.color = colors[1];
			
			NextPlayer();
	}





}
