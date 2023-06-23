using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneTransition : MonoBehaviour {

    private static SceneTransition _instance;
    private static bool _shouldPlayOpeningAnimation;
    private static readonly int SceneOpeningTrigger = Animator.StringToHash("SceneOpeningTrigger");
    private static readonly int SceneClosingTrigger = Animator.StringToHash("SceneClosingTrigger");

    public Image loadingProgressBar;

    private Animator _componentAnimator;
    private AsyncOperation _loadingSceneOperation;

    private Sprite _viewerImage;


    private void Start() {
        _instance = this;

        _componentAnimator = GetComponent<Animator>();


        if (_shouldPlayOpeningAnimation)
        {
            _componentAnimator.SetTrigger(SceneOpeningTrigger);
            _instance.loadingProgressBar.fillAmount = 1;

            _shouldPlayOpeningAnimation = false;
        }
    }

    private void Update() {
        if (_loadingSceneOperation != null)
        {

            loadingProgressBar.fillAmount = Mathf.Lerp(loadingProgressBar.fillAmount, _loadingSceneOperation.progress,
                Time.deltaTime * 5);
        }
    }

    public static void SwitchToScene(string sceneName) {
        _instance._componentAnimator.SetTrigger(SceneClosingTrigger);

        _instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);

        _instance._loadingSceneOperation.allowSceneActivation = false;

        _instance.loadingProgressBar.fillAmount = 0;
    }

    public void OnClosingAnimationOver() {
        _shouldPlayOpeningAnimation = true;
        _loadingSceneOperation.allowSceneActivation = true;

    }
}