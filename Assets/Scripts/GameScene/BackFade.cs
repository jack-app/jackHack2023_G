using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackFade : MonoBehaviour
{
    [SerializeField] MeshFilter meshFilter = null!;
    private Vector3[] positions = new Vector3[720];
    private int[] meshArr = new int[2160];

    private Vector3 VectorCarculate( float rad, float y)
    {
        float radius = 53;
        float z = Mathf.Cos(rad * Mathf.PI) * radius;
        float x = -(Mathf.Sin(rad * Mathf.PI) * radius);

        return new Vector3(x, y, z);
    }

    private void Create()
    {
            positions[0] = VectorCarculate(0, -200);
            positions[1] = VectorCarculate(0, 10);

        for (int widthNum = 1; widthNum < 360; widthNum++)
        {
            positions[widthNum * 2] = VectorCarculate(2 / 360 * widthNum, -200);
            positions[widthNum * 2 + 1] = VectorCarculate(2 / 360 * widthNum, 10);

            meshArr[widthNum * 6 - 6] = (widthNum - 1) * 2;
            meshArr[widthNum * 6 - 5] = (widthNum - 1) * 2 + 1;
            meshArr[widthNum * 6 - 4] = (widthNum - 1) * 2 + 2;
            meshArr[widthNum * 6 - 3] = (widthNum - 1) * 2 + 2;
            meshArr[widthNum * 6 - 2] = (widthNum - 1) * 2 + 1;
            meshArr[widthNum * 6 - 1] = (widthNum - 1) * 2 + 3;
        }

        //decide position
        Mesh mesh = new Mesh();
        mesh.vertices = positions;
        mesh.triangles = meshArr;
        meshFilter.mesh = mesh;
    }
    // Start is called before the first frame update
    void Start()
    {
        Create();
    }

}
