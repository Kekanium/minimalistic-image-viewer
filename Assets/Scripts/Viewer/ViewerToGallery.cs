using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewerToGallery : MonoBehaviour
{
    public void TransitionViewerToGallery() {
        SceneTransition.SwitchToScene("Gallery", LoadSceneMode.Single);
        //SceneManager.LoadScene("Gallery");
    }
}
