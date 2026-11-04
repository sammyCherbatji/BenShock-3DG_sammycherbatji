
Shader "Custom/SpecularHighlights"
{
    Properties
    {
        _Color ("Surface Color", Color) = (1,1,1,1)
        _SpecularColor ("Specular Color", Color) = (1,1,1,1)
        _Shininess ("Shininess", Range(1, 100)) = 32
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
            #include "Lighting.cginc"

            float4 _Color;
            float4 _SpecularColor;
            float _Shininess;

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

                o.worldNormal =
                    UnityObjectToWorldNormal(v.normal);

                o.worldPosition =
                    mul(unity_ObjectToWorld, v.vertex).xyz;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 normal =
                    normalize(i.worldNormal);

                float3 lightDirection =
                    normalize(_WorldSpaceLightPos0.xyz);

                float3 viewDirection =
                    normalize(_WorldSpaceCameraPos - i.worldPosition);

                float diffuse =
                    max(dot(normal, lightDirection), 0.0);

                float3 reflectionDirection =
                    reflect(-lightDirection, normal);

                float specular =
                    pow(
                        max(dot(viewDirection, reflectionDirection), 0.0),
                        _Shininess
                    );

                float3 ambient =
                    UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;

                float3 diffuseLighting =
                    _LightColor0.rgb * _Color.rgb * diffuse;

                float3 specularLighting =
                    _LightColor0.rgb * _SpecularColor.rgb * specular;

                float3 finalColor =
                    ambient + diffuseLighting + specularLighting;

                return fixed4(finalColor, _Color.a);
            }

            ENDCG
        }
    }
}
