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

    public AudioClip audioBoost;
    public AudioClip audioCrash;
    public AudioClip audioFinish;
    public ParticleSystem particleCrash;
    public ParticleSystem particleSuccess;
    public ParticleSystem particleBoost;

    Rigidbody rb;
    AudioSource audioSource;

    [HideInInspector]
    public bool isBoosting = false;
    bool isTransitioning = false;
    bool disableCollision = false;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioBoost;
    }

    void Update() {
        // Restart with "R"
        if (Input.GetKeyDown(KeyCode.R)) {
            RestartLevel();
        }

        DebugInput();
    }

    void FixedUpdate() {
        if (!isTransitioning) {
            Boost();
            Rotate();
        }
    }

    /// <summary>method <c>Boos</c>Move player in direction of rotation</summary>
    void Boost() {
        if (Input.GetKey(KeyCode.Space)) {
            rb.AddRelativeForce(Vector3.up * boostSpeed * Time.deltaTime);

            if (!isBoosting) {
                audioSource.PlayOneShot(audioBoost);
                particleBoost.Play();
            }

            isBoosting = true;
        } else {
            audioSource.Stop();
            particleBoost.Stop();
            isBoosting = false;
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
        if (!isTransitioning && !disableCollision) {
            switch (other.gameObject.tag) {
                case "Friendly":
                    break;
                case "Finish":
                    Finish();
                    break;
                default:
                    Crash();
                    break;
            }
        }
    }

    void Finish() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(audioFinish);
        particleBoost.Stop();
        particleSuccess.Play();

        Invoke("NextLevel", delayRespawn);
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
        isTransitioning = true;
        rb.constraints = RigidbodyConstraints.None;
        audioSource.Stop();
        audioSource.PlayOneShot(audioCrash, 0.5f);
        particleBoost.Stop();
        particleCrash.Play();

        Invoke("RestartLevel", delayRespawn);
    }

    void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void DebugInput() {
        if (Input.GetKeyDown(KeyCode.L)) {
            NextLevel();
        } 
        else if (Input.GetKeyDown(KeyCode.C)) {
            disableCollision = !disableCollision;
        }

        
    }
}
