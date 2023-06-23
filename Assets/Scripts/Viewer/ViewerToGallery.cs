using UnityEngine;
namespace Viewer {
    public class ViewerToGallery : MonoBehaviour {
        public void TransitionViewerToGallery() {
            SceneTransition.SwitchToScene("Gallery");
        }
    }
}