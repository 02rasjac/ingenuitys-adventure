using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int boostSpeed = 1000;
    public float rotationSpeed = 45f;
    [Tooltip("Delay in seconds")]
    public float delayRespawn = 1f;

    bool isDead = false;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        Boost();
        
        if (!isDead) {
            Rotate();
        }
    }

    /// <summary>method <c>Boos</c>Move player in direction of rotation</summary>
    void Boost() {
        if (Input.GetKey(KeyCode.Space) && !isDead) {
            rb.AddRelativeForce(Vector3.up * boostSpeed * Time.deltaTime);
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        } else {
            audioSource.Stop();
        }
    }

    /// <summary>method <c>Rotate</c>Rotate the player left/right</summary>
    void Rotate() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            // transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
            rb.AddTorque(-Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            // transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            rb.AddTorque(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("You bumped into a launchpad");
                break;
            case "Finish":
                Invoke("NextLevel", delayRespawn);
                break;
            default:
                Crash();
                break;
        }
    }

    void NextLevel() {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = levelIndex + 1;

        // Restart at first level, if this was the last
        if (levelIndex == SceneManager.sceneCountInBuildSettings - 1) {
            nextLevelIndex = 0;
        }

        SceneManager.LoadScene(nextLevelIndex);
    }

    void Crash() {
        isDead = true;
        rb.constraints = RigidbodyConstraints.None;
        Invoke("RestartLevel", delayRespawn);
    }

    void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
