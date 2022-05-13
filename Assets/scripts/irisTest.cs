using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class irisTest : MonoBehaviour
{
    // data input
    [SerializeField] irisData iris_data;
    [SerializeField] float debugRadius = 0.01f;

    private void OnDrawGizmos()
    {
        List<Vector3> vertices = ReturnCircle(iris_data.center, 1f, 8);
        vertices.AddRange(ReturnCircle(new Vector3(0,0,4), 1f, 16));

        Gizmos.color = Color.red;
        for (int i = 0; i < vertices.Count; i++)
        {
            Gizmos.DrawWireCube(vertices[i], new Vector3(debugRadius,debugRadius,debugRadius));
        }
    }

    // Custom Functions
    List<Vector3> ReturnCircle(Vector3 position, float radius, int resolution)
    {
        List<Vector3> circle = new List<Vector3>();
        float angle = 0;
        float step = 2 * Mathf.PI / resolution;
        for (int i = 0; i < resolution; i++)
        {
            circle.Add(new Vector3(position.x + radius * Mathf.Cos(angle), position.y + radius * Mathf.Sin(angle), 0));
            angle += step;
        }
        return circle;
    }

    Mesh TubeSegment()
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        Vector3 position = Vector3.zero;
        Vector3 position2 = new Vector3 (0,0,1);
        
        vertices.AddRange(ReturnCircle(position, iris_data.tube_radius, iris_data.tube_resolution));
        vertices.AddRange(ReturnCircle(position, iris_data.tube_radius, iris_data.tube_resolution));

        return mesh;
    }

    void UploadMesh(Mesh mesh)
    {
        // load mesh into scriptable object
        iris_data.mesh = mesh;
    }
}
