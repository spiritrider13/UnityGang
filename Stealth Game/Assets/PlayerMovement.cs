using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       
        var transform = this.GetComponent<Transform>(); 
        var position = transform.position;
        position.x += Input.GetAxis("Horizontal") * this.speed * Time.deltaTime;
        position.z += Input.GetAxis("Vertical") * this.speed * Time.deltaTime;
        transform.position = position;
        //var delta = new Vector3(
            //Input.GetAxis("Horizontal") * this.speed,
            //0.0f,
            //Input.GetAxis("Vertical") * this.speed
        //);

        //this.GetComponent<CharacterController>().SimpleMove(delta);

    }
}