using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>var <c>boostSpeed</c> indicates the ammount of force to be applied in local y-axis</summary>
    public int boostSpeed = 10;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        Boost();
        Rotate();
    }

    /// <summary>method <c>Boos</c>Move player in direction of rotation</summary>
    void Boost() {
        if (Input.GetKey(KeyCode.Space)) {
            rb.AddRelativeForce(Vector3.up * boostSpeed);
        }
    }

    /// <summary>method <c>Rotate</c>Rotate the player left/right</summary>
    void Rotate() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            Debug.Log("Rotating right");
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            Debug.Log("Rotating left");
        }
    }
}
