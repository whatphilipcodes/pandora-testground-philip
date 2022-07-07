using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.VFX;

public class VFXManager : MonoBehaviour
{
    [SerializeField] VisualEffect vfx;
    [SerializeField] MeshCarrier meshCar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!vfx || !meshCar) throw new System.Exception("VFXManager is missing references");

        if (meshCar.mesh != null)
        {
            vfx.SetMesh("inputMesh", meshCar.mesh);
        }
    }
}
