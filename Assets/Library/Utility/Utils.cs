using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PandoraUtils
{
    public class util
    {
        public static Vector3 RandomVector (float range)
        {
            Vector3 vec = new Vector3
            (
                Random.Range(-range, range),
                Random.Range(-range, range),
                Random.Range(-range, range)
            );
            return vec;
        }
    }
}
