using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Boost();
        Rotate();
    }

    /**
     * Move player in direction of rotation
     */
    void Boost() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            Debug.Log("Rotating left");
        }
    }

    /**
     * Rotate the player left/right
     */
    void Rotate() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            Debug.Log("Rotating right");
        }
        if (Input.GetKey(KeyCode.Space)) {
            Debug.Log("Boosting");
        }
    }
}
