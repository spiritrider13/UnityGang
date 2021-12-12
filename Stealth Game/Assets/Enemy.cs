using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
	GameObject player;
	GameObject playerObject;
	GameObject enemyObject;
	public string moveDirection = "right";
    public int patrolRoute;
    Vector3[][] waypoints;
    // Start is called before the first frame update
	bool resetting = false;
	int timer = 0;
    int currWaypoint;
    void Start()
    {
        this.player = GameObject.Find("PlayerBody");
		this.playerObject = GameObject.Find("Player");
		this.enemyObject = GameObject.Find("Enemy");
        //Going to use a series of waypoints
        //Need to figure out how to not hardcode this for multiple enemies
        waypoints = new Vector3[3][];
        waypoints[0] = new Vector3[2]{new Vector3(-14.8f, 1.0f, -1.2f), new Vector3(14.38f, 1.0f, -1.2f)};
		waypoints[1] = new Vector3[2]{new Vector3(-14.8f, 1.0f, 6.28f), new Vector3(14.38f, 1.0f, 6.28f)};
		waypoints[2] = new Vector3[2]{new Vector3(-14.8f, 1.0f, 15.85f), new Vector3(3.2f, 1.0f, 15.85f)};
        currWaypoint = 0;
    }

    void Update(){

		if (resetting) {
			timer += 1;
			if (timer >= 300) {
				var pos = this.GetComponent<Transform>(); 
        		pos.position = waypoints[patrolRoute][0];
				resetting = false;
				timer = 0;
                currWaypoint = 0;
			}
			return;
		}

		var transform = this.GetComponent<Transform>(); 
        var position = transform.position;
		var rotation = transform.rotation;

        //Check to see if we're at a waypoint
        if(this.transform.position == waypoints[patrolRoute][currWaypoint]){
            if(currWaypoint == waypoints[patrolRoute].Length -1)
                currWaypoint = 0;
            else
                currWaypoint++;
        }
        //Then go to the next waypoint
        Vector3 toDest = waypoints[patrolRoute][currWaypoint] - this.transform.position;
        //Figure out if we're facing it
        float angleTo = Vector3.SignedAngle(toDest.normalized, this.transform.forward, Vector3.up);
        if(System.Math.Abs(angleTo) >=  0.1){
            //Need to turn to it, but not overturn
            //Standard is 120 degrees a second
            //Check if we can move the full distance we want
            if(System.Math.Abs(angleTo) > 120 * Time.deltaTime){
                //If so, do it
                this.transform.Rotate(new Vector3(0.0f, 120.0f * System.Math.Sign(angleTo) * Time.deltaTime * -1.0f, 0.0f));
            }
            //Otherwise, just rotate directly to it
            else
                this.transform.Rotate(new Vector3(0.0f, angleTo, 0.0f));
        }
        //Once we're done rotating, move
        else{
            //If we can get there immediately, do it
            if(toDest.magnitude <= 10.0f * Time.deltaTime){
                this.transform.position = waypoints[patrolRoute][currWaypoint];
            }
            //Otherwise, move the best we can

            else
                this.transform.Translate(toDest.normalized * 10.0f * Time.deltaTime, Space.World);
        }
		//if (moveDirection == "right") {
		//	if (position.x > -8) {
		//		position.x -= 0.01f;
		//		position.z += 0.01f;
		//		rotation.y = 135.0f;
		//	}
		//	else {
		//		moveDirection = "left";
		//	}
		//}
		//else {
		//	if (position.x < 0) {
		//		position.x += 0.01f;
		//		position.z -= 0.01f;
		//		rotation.y = -135.0f;
		//	}
		//	else {
		//		moveDirection = "right";
		//	}
		//}
//
		//transform.position = position;
		//transform.rotation = rotation;
		
    	//Need to check to see if the player is roughly in front
    	//We're doing it from the player's height
    	Vector3 toPlayer = this.player.transform.position - new Vector3(this.transform.position.x, this.player.transform.position.y, this.transform.position.z);
    	//Figure out the angle, to see if it's in the vision cone
        //Debug.Log(Vector3.Angle(toPlayer, this.transform.forward));
        //Also check distance
    	if(Vector3.Angle(toPlayer, this.transform.forward) < 40 && toPlayer.magnitude < 10){
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
    		if(hit) {
    			Debug.Log("Spot");
				playerObject.SendMessage("reset");
				enemyObject.SendMessage("reset");
			}


    	}
    }

	void reset() {
		resetting = true;
    }
}