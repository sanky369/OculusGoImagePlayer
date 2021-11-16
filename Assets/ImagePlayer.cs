using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VREasy;
using UnityEngine.UIElements;
using TMPro;

public class ImagePlayer : MonoBehaviour
{
    public VRPanoramaView panoramaViewer;
    public List<Texture2D> Images;
    public TextMeshProUGUI roomText;
    // Start is called before the first frame update
    int currentImage = 0;
    

    void UpdateImage(int imageID){
        panoramaViewer.Image = Images[imageID];
        roomText.text = Images[imageID].name;
    }
    void OnEnable()
    {
        currentImage = 0;
        UpdateImage(currentImage);
    }

    public void ShowNext(){
        currentImage++;
        UpdateImage(currentImage);
    }

    public void ShowPrev(){
        currentImage--;
        UpdateImage(currentImage);
    }

    private void Update() {
        if(currentImage < 0){
            currentImage = 0;
            UpdateImage(currentImage);
            return;
        }

        if(currentImage > Images.Count){
            currentImage = Images.Count - 1;
            UpdateImage(currentImage);
            return;
        }
    }

    public void ResetUI(){
        roomText.text = "";
        panoramaViewer.Image = null;
    }

}
