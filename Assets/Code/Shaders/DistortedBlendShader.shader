Shader "Custom/DistortedBlendShader"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _BlendTex ("Blend Texture", 2D) = "white" {}
        _DistortionTex ("Distortion Texture", 2D) = "white" {} // Texture per la distorsione
        _DistortionAmount ("Distortion Amount", Range(0, 1)) = 0.1
        _BlendFactor ("Blend Factor", Range(0, 1)) = 0.5 // Fattore di blending
        _TimeSpeed ("Time Speed", Range(0.1, 10.0)) = 1.0 // Velocità dell'animazione
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _BlendTex;
            sampler2D _DistortionTex;
            float _DistortionAmount;
            float _BlendFactor;
            float _TimeSpeed;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 distortion = (tex2D(_DistortionTex, i.uv * _TimeSpeed).rg * 2.0 - 1.0) * _DistortionAmount;
                float2 uvDistorted = i.uv + distortion;

                fixed4 baseColor = tex2D(_MainTex, uvDistorted);
                fixed4 blendColor = tex2D(_BlendTex, uvDistorted);

                fixed4 finalColor = lerp(baseColor, blendColor, _BlendFactor);
                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
