using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 1.0f;
    public bool canMove = true;

    int timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       
        if (!canMove) {
            timer += 1;
            if (timer >= 300) {
                var pos = this.GetComponent<Transform>(); 
                pos.position = new Vector3(12.72f, 1.0f, -14.9f);
                canMove = true;
                timer = 0;
            }
            return;
        }

        var delta = new Vector3(
            Input.GetAxis("Horizontal") * this.speed,
            0.0f,
            Input.GetAxis("Vertical") * this.speed
        );

        this.GetComponent<CharacterController>().SimpleMove(delta);
        if(this.transform.position.z >= 18)
        	reset();
        	timer = 250;
    }

    void reset() {
        canMove = false;
    }

}