using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
   
    public Image LoadingProgressBar;
    public Transform ProgressBarTransform;
    public Transform ImageFadeTranform;
    
    private static SceneTransition instance;
    private static bool shouldPlayOpeningAnimation = false; 
    
    private Animator componentAnimator;
    private AsyncOperation loadingSceneOperation;

    public static void SwitchToScene(string sceneName)
    {
        instance.componentAnimator.SetTrigger("SceneClosingTrigger");

        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        
        instance.loadingSceneOperation.allowSceneActivation = false;
        
        instance.LoadingProgressBar.fillAmount = 0;
    }
    
    private void Start()
    {
        instance = this;
        
        componentAnimator = GetComponent<Animator>();

        
        if (shouldPlayOpeningAnimation) 
        {
            componentAnimator.SetTrigger("SceneOpeningTrigger");
            instance.LoadingProgressBar.fillAmount = 1;
          
            shouldPlayOpeningAnimation = false; 
        }
    }

    private void Update()
    {
        if (loadingSceneOperation != null)
        {
           
            LoadingProgressBar.fillAmount = Mathf.Lerp(LoadingProgressBar.fillAmount, loadingSceneOperation.progress,
                Time.deltaTime * 5);
        }
    }
    
    public void OnClosingAnimationOver()
    {
      
        
            shouldPlayOpeningAnimation = true;

            loadingSceneOperation.allowSceneActivation = true;
        
    }

}