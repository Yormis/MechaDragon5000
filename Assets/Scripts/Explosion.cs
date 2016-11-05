using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    Renderer rend;

    public float dispScrollSpeedX;
    public float dispScrollSpeedY;

    public float disp2ScrollSpeedX;
    public float disp2ScrollSpeedY;

    public float mainScrollSpeedX;
    public float mainScrollSpeedY;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float d1OffsetX = Time.time * dispScrollSpeedX;
        float d1OffsetY = Time.time * dispScrollSpeedY;
        float d2OffsetX = Time.time * disp2ScrollSpeedX;
        float d2OffsetY = Time.time * disp2ScrollSpeedY;
        float mOffsetX = Time.time * mainScrollSpeedX;
        float mOffsetY = Time.time * mainScrollSpeedY;

        rend.material.SetTextureOffset("_MainTex", new Vector2(mOffsetX, mOffsetY));
        rend.material.SetTextureOffset("_DispTex", new Vector2(d1OffsetX, d1OffsetY));
        rend.material.SetTextureOffset("_DispTex2", new Vector2(d2OffsetX, d2OffsetY));

    }
}
