using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("PlayerBody");
    }

    void Update(){
    	//Need to check to see if the player is roughly in front
    	//We're doing it from the player's height
    	Vector3 toPlayer = this.player.transform.position - new Vector3(this.transform.position.x, this.player.transform.position.y, this.transform.position.z);
    	//Figure out the angle, to see if it's in the vision cone
        //Debug.Log(Vector3.Angle(toPlayer, this.transform.forward));
    	if(Vector3.Angle(toPlayer, this.transform.forward) < 40){
    		Debug.Log("Potential spot");
    		//We're going to try raycasting now to the left, middle, and right bits of the player
    		//If any of them don't hit a wall, we found it
    		//Specify wall via layermask stuff
    		int wallMask = LayerMask.GetMask("Wall");
            //why
    		//And the mask to get the player
    		int playerMask = LayerMask.GetMask("Player");
    		//use the ever so fancy technique of swapping x and z and negating 1 to get perpendicular
    		Vector3 playerNorm = toPlayer.normalized;
    		Vector3 playerSide = new Vector3(-playerNorm.z, 0, playerNorm.x);
    		//Now check left middle right
    		bool hit = false;
    		for (int i = -1; i < 2; i++){
    			Vector3 dir = playerSide * i * 0.5f + toPlayer;
                //Debug.Log(dir);
    			if(!Physics.Raycast(this.transform.position, dir.normalized, dir.magnitude, wallMask) && 
    				Physics.Raycast(this.transform.position, dir.normalized, dir.magnitude, playerMask)){
    				hit = true;
    				break;
    			}
    		}
    		if(hit)
    			Debug.Log("Spot");
    	}
    }
}