using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets
{
    public class LoadingScreenManager : MonoBehaviour
    {
        [Header("Loading Visuals")]
        public Image loadingIcon;
        public Image loadingDoneIcon;
        public Text loadingText;
        public Image progressBar;
        public Image fadeOverlay;

        [Header("Timing Settings")]
        public float waitOnLoadEnd = 0.25f;
        public float fadeDuration = 0.25f;

        [Header("Loading Settings")]
        public LoadSceneMode loadSceneMode = LoadSceneMode.Single;
        public ThreadPriority loadThreadPriority;

        [Header("Other")]
        // If loading additive, link to the cameras audio listener, to avoid multiple active audio listeners
        public AudioListener audioListener;

        AsyncOperation operation;
        Scene currentScene;

        // This is the build index of your loading scene
        static int loadingSceneIndex = 2;
        public static int sceneToLoad = -1;

        public static void LoadScene(int levelNum)
        {
            Application.backgroundLoadingPriority = ThreadPriority.High;
            sceneToLoad = levelNum;
            SceneManager.LoadScene(loadingSceneIndex);
        }

        void Start()
        {
            if (sceneToLoad < 0)
                return;

            this.fadeOverlay.gameObject.SetActive(true); // Making sure it's on so that we can crossfade Alpha
            this.currentScene = SceneManager.GetActiveScene();
            this.StartCoroutine(this.LoadAsync(sceneToLoad));
        }

        private IEnumerator LoadAsync(int levelNum)
        {
            this.ShowLoadingVisuals();

            yield return null;

            this.FadeIn();
            this.StartOperation(levelNum);

            float lastProgress = 0f;

            // operation does not auto-activate scene, so it's stuck at 0.9
            while (this.DoneLoading() == false)
            {
                yield return null;

                if (Mathf.Approximately(this.operation.progress, lastProgress) == false)
                {
                    this.progressBar.fillAmount = this.operation.progress;
                    lastProgress = this.operation.progress;
                }
            }

            if (this.loadSceneMode == LoadSceneMode.Additive)
                this.audioListener.enabled = false;

            this.ShowCompletionVisuals();

            yield return new WaitForSeconds(this.waitOnLoadEnd);

            this.FadeOut();

            yield return new WaitForSeconds(this.fadeDuration);

            if (this.loadSceneMode == LoadSceneMode.Additive)
                SceneManager.UnloadScene(this.currentScene.name);
            else
                this.operation.allowSceneActivation = true;
        }

        private void StartOperation(int levelNum)
        {
            Application.backgroundLoadingPriority = this.loadThreadPriority;
            this.operation = SceneManager.LoadSceneAsync(levelNum, this.loadSceneMode);


            if (this.loadSceneMode == LoadSceneMode.Single)
                this.operation.allowSceneActivation = false;
        }

        private bool DoneLoading()
        {
            return (this.loadSceneMode == LoadSceneMode.Additive && this.operation.isDone) || (this.loadSceneMode == LoadSceneMode.Single && this.operation.progress >= 0.9f);
        }

        void FadeIn()
        {
            this.fadeOverlay.CrossFadeAlpha(0, this.fadeDuration, true);
        }

        void FadeOut()
        {
            this.fadeOverlay.CrossFadeAlpha(1, this.fadeDuration, true);
        }

        void ShowLoadingVisuals()
        {
            this.loadingIcon.gameObject.SetActive(true);
            this.loadingDoneIcon.gameObject.SetActive(false);

            this.progressBar.fillAmount = 0f;
            this.loadingText.text = "LOADING...";
        }

        void ShowCompletionVisuals()
        {
            this.loadingIcon.gameObject.SetActive(false);
            this.loadingDoneIcon.gameObject.SetActive(true);

            this.progressBar.fillAmount = 1f;
            this.loadingText.text = "LOADING DONE";
        }
    }
}