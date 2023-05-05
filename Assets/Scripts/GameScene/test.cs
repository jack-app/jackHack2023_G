using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(RectTransform))]
public class Sample : Graphic
{
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        /*���b�V���Ⓒ�_���������B�L���b�V���ł���Ȃ炢��Ȃ��B
          �p�t�H�[�}���X�I�ɂ��L���b�V���ł���Ȃ炵���ق����ǂ��B
          �������A���񐶐�����̂ł���Β��_���̏��(65000)�ɒB����̂ŕK�v�B*/
        vh.Clear();
        //���_����struct
        UIVertex v = UIVertex.simpleVert;
        v.color = Color.red;

        //���������̈ʒu�ɒ��_�u��
        v.position = CreatePos(0, 0);
        //���_���o�^
        vh.AddVert(v);

        v.position = CreatePos(0, 1);
        vh.AddVert(v);
        v.position = CreatePos(1, 0);
        vh.AddVert(v);

        //���b�V���̎O�p�`�����B�����͓o�^�������_�̃C���f�b�N�X�B
        vh.AddTriangle(0, 1, 2);
    }
    private Vector2 CreatePos(float x, float y)
    {
        Vector2 p = Vector2.zero;
        p.x -= rectTransform.pivot.x;
        p.y -= rectTransform.pivot.y;
        p.x += x;
        p.y += y;
        p.x *= rectTransform.rect.width;
        p.y *= rectTransform.rect.height;
        return p;
    }
}
