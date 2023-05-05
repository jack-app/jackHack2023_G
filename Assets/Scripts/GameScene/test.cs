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
        /*メッシュや頂点情報を消去。キャッシュできるならいらない。
          パフォーマンス的にもキャッシュできるならしたほうが良い。
          ただし、毎回生成するのであれば頂点数の上限(65000)に達するので必要。*/
        vh.Clear();
        //頂点情報のstruct
        UIVertex v = UIVertex.simpleVert;
        v.color = Color.red;

        //いい感じの位置に頂点置く
        v.position = CreatePos(0, 0);
        //頂点情報登録
        vh.AddVert(v);

        v.position = CreatePos(0, 1);
        vh.AddVert(v);
        v.position = CreatePos(1, 0);
        vh.AddVert(v);

        //メッシュの三角形生成。引数は登録した頂点のインデックス。
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
