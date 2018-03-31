using UnityEngine;
using System.Collections;

public class MaterialScroll : MonoBehaviour
{
    public float scrollSpeed1 = 0.5F;
    public float scrollSpeed2 = 0.5F;
    public float scrollSpeed3 = 0.5F;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        float offset1 = Time.time * scrollSpeed1;
        float offset2 = Time.time * scrollSpeed2;
        float offset3 = Time.time * scrollSpeed3;
        rend.material.SetTextureOffset("_MainTex1", new Vector2(offset1, 0));
        rend.material.SetTextureOffset("_MainTex2", new Vector2(offset2, 0));
        rend.material.SetTextureOffset("_MainTex3", new Vector2(offset3, 0));
    }
}