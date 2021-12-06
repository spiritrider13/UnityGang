using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var transform = this.GetComponent<Transform>(); 
        var rotation = transform.rotation;
       // if (Input.GetAxis("Mouse X") < 0) {
           // Debug.Log(Input.GetAxis("Mouse X"));
           // rotation.y -= 0.01f;
      //  }
      //  if (Input.GetAxis("Mouse X") > 0) {
      //       Debug.Log(Input.GetAxis("Mouse X"));
      //      rotation.y += 0.01f;
      //  }
     //   transform.rotation = rotation;
    }
}
