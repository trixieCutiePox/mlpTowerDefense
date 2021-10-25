Shader "Hidden/ColorTransform"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
          "Queue"="Transparent"
          "IgnoreProjector"="True"
          "RenderType"="Transparent"
          "PreviewType"="Plane"
          "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ PIXELSNAP_ON

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.uv = IN.uv;
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif

                return OUT;
            }

            sampler2D _MainTex;
            float4x4 linearTransform;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float4 colorNonlinear = float4(col.rgb, sin(col.r * col.g * col.b));
                float4 result = mul(linearTransform, colorNonlinear);

                col.r = result.r + result.a;
                col.g = result.g + result.a;
                col.b = result.b + result.a;

                col.rgb *= col.a;

                return col;
            }
            ENDCG
        }
    }
}
