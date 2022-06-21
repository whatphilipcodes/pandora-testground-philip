using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TakeScreenshot : MonoBehaviour
{
    [SerializeField] private string folder;
    [SerializeField] private resFactor resolutionFactor;
    
    private enum resFactor { x1, x2, x4, x8 };
    private int resFacInt;
    private int counter;
    private string dataPath;
    

    // Start is called before the first frame update
    void Start()
    {
        if (resolutionFactor == resFactor.x1) {
            resFacInt = 1;
        } else if (resolutionFactor == resFactor.x2) {
            resFacInt = 2;
        } else if (resolutionFactor == resFactor.x4) {
            resFacInt = 4;
        } else {
            resFacInt = 8;
        }

        if (String.IsNullOrEmpty(folder))
        {
            print ("Please add folder path in inpsector.");
            this.enabled = false;
            return;
        }

        counter = 0;

        print("Screenshot function enabled. In GameView, press the 's' key to save the current frame.");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            string fileName = "0" + counter + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            dataPath = folder + "/" + fileName;
            ScreenCapture.CaptureScreenshot(dataPath, resFacInt);
            print(fileName + ".png has been saved to: " + folder);
            counter++;
        }
    }
}
