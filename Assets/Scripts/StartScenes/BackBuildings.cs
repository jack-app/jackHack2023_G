using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartBackBuildings
{
    public class BackBuildings : MonoBehaviour
    {
        public MeshFilter meshFilter = null!;
        public Material frontMate = null!;
        public Material backMate = null!;
        public MeshRenderer meshRenderer = null!;
        private System.Random r = new System.Random();
        private bool _front; //Žè‘O‚È‚çtrue,‰œ‚È‚çfalse
        private float height;
        private float width;
        private Vector3[] meshArr = new Vector3[4];
        private int[] triangle = new int[6] { 0, 1, 2, 2, 1, 3 };

        public float Create(bool front)
        {
            _front = front;
            if (_front)
            {
                height = r.Next(80, 200);
                width = r.Next(40, 100);
                meshRenderer.material = frontMate;
                meshArr[0] = new Vector3(0, 0, 500);
                meshArr[1] = new Vector3(0, height, 500);
                meshArr[2] = new Vector3(width, 0, 500);
                meshArr[3] = new Vector3(width, height, 500);
            }
            else
            {
                height = r.Next(100, 300);
                width = r.Next(40, 100);
                meshRenderer.material = backMate;
                meshArr[0] = new Vector3(0, 0, 502);
                meshArr[1] = new Vector3(0, height, 502);
                meshArr[2] = new Vector3(width, 0, 502);
                meshArr[3] = new Vector3(width, height, 502);
            }
            Mesh mesh = new Mesh();
            mesh.vertices = meshArr;
            mesh.triangles = triangle;
            meshFilter.mesh = mesh;

            return width;
        }

        public float Move()
        {
            float x = transform.position.x;
            if (_front) x = x - 1.5f;
            else x = x - 0.7f;
            transform.position = new Vector3(x, -150f, 0f);
            return x + width;
        }

        public void Remove()
        {
            Destroy(this.gameObject.transform.GetChild(0).gameObject);
            Destroy(this.gameObject);
        }
    }
}