using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void SwitchToGallery() {
        SceneTransition.SwitchToScene("Gallery");
        //SceneManager.LoadScene("Gallery");
    }
}
