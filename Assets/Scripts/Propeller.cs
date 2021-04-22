using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    public bool reverse = false;
    public float propellerSpeedIdle = 45f;
    public float propellerSpeedBoostingMultiplier = 3f;

    public GameObject player;

    void Start() {
        if (reverse) {
            propellerSpeedIdle *= -1;
        }
    }

    void Update() {
        if (player.GetComponent<PlayerController>().isBoosting) {
            transform.Rotate(Vector3.forward * propellerSpeedIdle * propellerSpeedBoostingMultiplier * Time.deltaTime);
        } else {
            transform.Rotate(Vector3.forward * propellerSpeedIdle * Time.deltaTime);
        }
    }
}
