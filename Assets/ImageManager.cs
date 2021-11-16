using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VREasy;

public class ImageManager : MonoBehaviour
{
    public VRPanoramaView panoramaView;
    // Start is called before the first frame update
    public void LoadImageOnPanorama(Texture2D imageToLoad){
        panoramaView.Image = imageToLoad;
    }

}
