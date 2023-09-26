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
        int currentSceneBuildIndex = scene.buildIndex;
        
        SceneManager.LoadScene(currentSceneBuildIndex);
    }
}