using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateCamera : MonoBehaviour
{
    public Scene currentScene;

    public string titleScene;
    public string gameScene;

    [SerializeField] float rotationSpeed;

    void Start()
    {
        // reference to the current scene loaded
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        // if it's on the title screen, rotate the camera constantly for title screen animation
        if (currentScene.name == titleScene)
        {
            RotateIntro();
        }
        // if the game has started, allow the player to control screen rotation
        else if (currentScene.name == gameScene)
        {
            CameraInput();
        }
    }

    // rotate the camera with the left and right arrow keys
    void CameraInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }

    // rotate the camera at a constant speed
    void RotateIntro()
    {
        transform.Rotate(Vector3.up * (rotationSpeed / 10) * Time.deltaTime);
    }

    // loads the next scene, attached to the START button in the title screen UI
    public void LoadNextScene()
    {
        SceneManager.LoadScene(gameScene);
    }
}
