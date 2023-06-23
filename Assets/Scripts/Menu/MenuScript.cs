using UnityEngine;
namespace Menu {
    public class MenuScript : MonoBehaviour {
        public void SwitchToGallery() {
            SceneTransition.SwitchToScene("Gallery");
        }
    }
}