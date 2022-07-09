using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phenotype{
    Eye, Hair, Skin
}



public class AniStruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPhenotypeData(string readData, List<PhenotypeData> data = null){
        // readData -> content of read coming from device
        // PhenptypeData see top
    }
}



public struct PhenotypeData{
    public Phenotype phenotype;
    public Color color;
    public float probalility;
}
