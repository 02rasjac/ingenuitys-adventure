using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    public bool reverse = false;
    public float propellerSpeedIdle = 45f;
    public float propellerSpeedBoostingMultiplier = 3f;
    [Range(0, 1)] public float propellerSmoothnes = 0.05f;
    public GameObject player;

    float maxSpeed;
    float t = 0f;

    void Start() {
        if (reverse) {
            propellerSpeedIdle *= -1;
        }

        maxSpeed = propellerSpeedIdle * propellerSpeedBoostingMultiplier;
    }

    void Update() {
        float currentSpeed = Mathf.Lerp(propellerSpeedIdle, maxSpeed, t);

        if (player.GetComponent<PlayerController>().isBoosting) {
            transform.Rotate(Vector3.forward * currentSpeed * Time.deltaTime);
            t += propellerSmoothnes;
        } else {
            transform.Rotate(Vector3.forward * currentSpeed * Time.deltaTime);
            t -= propellerSmoothnes;
        }

        // Clamp t between 0 and 1
        if (t <= Mathf.Epsilon) {t = 0;}
        else if (t >= 1) {t = 1;}
    }
}
