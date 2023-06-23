using UnityEngine;
public static class StateGalleryToViewer {

    static StateGalleryToViewer() {
        CurrentPosition = 0f;
    }

    public static float CurrentPosition { get; set; }

    public static Texture2D PickedTexture2D { get; set; }
}