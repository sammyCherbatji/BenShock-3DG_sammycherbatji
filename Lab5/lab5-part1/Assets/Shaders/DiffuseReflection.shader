
Shader "Custom/DiffuseReflection"
{
    Properties
    {
        _Color ("Surface Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldPosition : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;

                o.position = UnityObjectToClipPos(v.vertex);

                o.worldNormal = UnityObjectToWorldNormal(v.normal);

                o.worldPosition = mul(unity_ObjectToWorld, v.vertex).xyz;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 normal = normalize(i.worldNormal);

                float3 lightDirection = normalize(
                    _WorldSpaceLightPos0.xyz
                );

                float diffuse = max(
                    dot(normal, lightDirection),
                    0.0
                );

                fixed4 finalColor = _Color * diffuse;

                return finalColor;
            }

            ENDCG
        }
    }
}
