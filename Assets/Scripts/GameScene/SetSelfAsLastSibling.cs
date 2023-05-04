using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
 
public class SetSelfAsLastSibling : MonoBehaviour
{
    // Start is called before the first frame update
    public void setLast()
    {
		//　ヒエラルキーで一番下に移動し、前面に表示される
		transform.SetAsLastSibling();
	}
}