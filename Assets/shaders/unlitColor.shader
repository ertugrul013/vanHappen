Shader "Unlit/unlitColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Lighting off

            SetTexture [_MainTex]{
                constantColor [_Color]
                combine constant * texture
            }
        }
    }
}
