using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    private Scene scene;
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }
    
    public void ReloadCurrentScene()
    {
        // Get the current scene's build index
        int currentSceneBuildIndex = scene.buildIndex;

        // Load the current scene again by its build index
        SceneManager.LoadScene(currentSceneBuildIndex);
    }
}