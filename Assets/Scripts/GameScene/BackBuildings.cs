using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace PlayBackBuildings
{
    public class BackBuildings : MonoBehaviour
    {
        /// <summary>
        /// í∏ì_èÓïÒ
        /// </summary>
        [SerializeField] MeshFilter meshFilter = null!;
        [SerializeField] Material frontMate = null!;
        [SerializeField] Material backMate = null!;
        [SerializeField] MeshRenderer meshRenderer = null!;
        private System.Random r = new System.Random();
        private const float RADIUS = 10f; //îºåa
        private bool _front;
        private float width;
        private float height;
        private float _rotate = 0;
        //private float radian = 0; //íÜêSÇ©ÇÁç∂í[Ç‹Ç≈ÇÃäpìx
        private Vector3[] positions;
        private int[] meshArr;

        private Vector3 VectorCarculate(bool front, float rad, float y)
        {
            float radius;
            if (front) radius = 50;
            else radius = 55;
            float z = Mathf.Cos(rad * Mathf.PI) * radius;
            float x = -(Mathf.Sin(rad * Mathf.PI) * radius);

            return new Vector3(x, y, z);
        }

        public float Create(bool front, float rotate)
        {
            _rotate = rotate;
            _front = front;
            if (_front)
            {
                height = r.Next(5, 20);
                width = r.Next(20, 30) / 100f;
                positions = new Vector3[(int)(width * 100 + 1) * 2];
                meshArr = new int[(int)(width * 100) * 6];
                meshRenderer.material = frontMate;
                positions[0] = VectorCarculate(true, 0, -200);
                positions[1] = VectorCarculate(true, 0, height);
            }
            else
            {
                height = r.Next(5, 20);
                width = r.Next(20, 30) / 100f;
                positions = new Vector3[(int)(width * 100 + 1) * 2];
                meshArr = new int[(int)(width * 100) * 6];
                meshRenderer.material = backMate;
                positions[0] = VectorCarculate(false, 0, -200);
                positions[1] = VectorCarculate(false, 0, height);
            }

            for(int widthNum = 1; widthNum < width * 100; widthNum++)
            {
                positions[widthNum * 2] = VectorCarculate(_front, -width / 100 * widthNum, -200);
                positions[widthNum * 2 + 1] = VectorCarculate(_front, -width / 100 * widthNum, height);

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

            return width * 180;
        }

        public float Move()
        {
            if (_front)
            {
                _rotate -= 0.12f;
            }
            else
            {
                _rotate -= 0.08f;
            }
            this.gameObject.transform.eulerAngles = new Vector3(0, _rotate, 0);

            return _rotate + width * 180;
        }

        public void Remove()
        {
            Destroy(this.gameObject.transform.GetChild(0).gameObject);
            Destroy(this.gameObject);
        }
    }
}