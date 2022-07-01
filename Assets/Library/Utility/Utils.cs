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

         public static Vector3 RandomVector2D (float range)
        {
            Vector3 vec = new Vector3
            (
                Random.Range(-range, range),
                Random.Range(-range, range),
                0
            );
            return vec;
        }

        public static Vector3 CircleCoordinate (float radius, float angle)
        {
            Vector3 vec = new Vector3
            (
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius,
                0
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

        // courtesy of jvo3dc: https://forum.unity.com/threads/random-number-without-repeat.497923/ (modified)
        public static List<int> RandomIDs(int count, List<int> available)
        {
            List<int> result = new List<int>();
            for (int i = 0;i < count;i++)
            {
                int selected = available[Random.Range(0, available.Count)];
                available.Remove(selected);
                result.Add(selected);
            }
            return result;
        }

        /*
        public List<int> RandomList(int fromInclusive, int toExclusive, int count)
        {
            List<int> available = new List<int>();
            List<int> result = new List<int>();
            for (int i = 0;i < count;i++)
            {
                if (available.Count == 0)
                {
                    for (int index = fromInclusive;index < toExclusive;index++) available.Add(index);
                }
                int selected = available[Random.Range(0, available.Count)];
                available.Remove(selected);
                result.Add(selected);
            }
            return result;
        }
        */
    }
}
