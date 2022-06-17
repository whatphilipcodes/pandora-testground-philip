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

        public static Vector3 RandomCircleCoordinate (float radius)
        {
            float angle = Random.Range(0f,360f);
            Vector3 vec = new Vector3
            (
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius,
                0
            );
            return vec;
        }
    }
}
