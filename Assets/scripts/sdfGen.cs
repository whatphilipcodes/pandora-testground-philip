using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.SDF;

public class sdfGen : MonoBehaviour
{
    [SerializeField] irisSettings config;
    [SerializeField] VisualEffect vfx;
    bool done = false;
    MeshToSDFBaker meshBaker;

    // Update is called once per frame
    void Update()
    {
        if (!done && config.mesh != null)
        {
            print("Baking mesh");
            meshBaker = new MeshToSDFBaker(config.sizeBox, config.center, config.maxResolution, config.mesh, config.signPassCount, config.threshold);
            meshBaker.BakeSDF();
            vfx.SetTexture("SDF", meshBaker.SdfTexture);
            done = true;
            print("Done");
        }
    }
    void OnDestroy()
    {
        if (meshBaker != null)
        {
            meshBaker.Dispose();
        }
    }
}
