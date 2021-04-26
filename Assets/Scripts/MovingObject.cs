using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public Vector3 moveVector;
    [Tooltip("Lower == faster")]
    public float period = 2f;

    float moveFactor;
    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Prevent x / 0 -> error
        if (period <= Mathf.Epsilon) {return;}

        // Calculate the offset to move based on a sin-wave
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSin = Mathf.Sin(cycles * tau);
        moveFactor = (rawSin + 1f) / 2;
        Vector3 offset = moveVector * moveFactor;

        // Set position
        transform.position = startPosition + offset;
    }
}
