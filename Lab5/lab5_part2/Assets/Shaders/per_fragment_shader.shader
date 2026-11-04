
Shader "Custom/per_fragment_shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _AmbientColor ("Ambient Color", Color) = (0.2,0.2,0.2,1)

        _DirectionalLightDirection ("Directional Light Direction", Vector) = (0,-1,0,0)
        _DirectionalLightColor ("Directional Light Color", Color) = (1,1,1,1)

        _PointLightPosition ("Point Light Position", Vector) = (2,2,2,1)
        _PointLightColor ("Point Light Color", Color) = (1,1,1,1)
        _PointLightRange ("Point Light Range", Float) = 10

        _SpecularColor ("Specular Color", Color) = (1,1,1,1)
        _Shininess ("Shininess", Range(1,100)) = 32
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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _AmbientColor;

            float4 _DirectionalLightDirection;
            float4 _DirectionalLightColor;

            float4 _PointLightPosition;
            float4 _PointLightColor;
            float _PointLightRange;

            float4 _SpecularColor;
            float _Shininess;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float3 worldPosition : TEXCOORD2;
            };

            v2f vert(appdata v)
            {
                v2f o;

                o.position =
                    UnityObjectToClipPos(v.vertex);

                o.worldNormal =
                    UnityObjectToWorldNormal(v.normal);

                o.worldPosition =
                    mul(unity_ObjectToWorld, v.vertex).xyz;

                o.uv =
                    TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 normal =
                    normalize(i.worldNormal);

                float3 viewDirection =
                    normalize(_WorldSpaceCameraPos - i.worldPosition);

                float3 ambient =
                    _AmbientColor.rgb;

                float3 directionalDirection =
                    normalize(-_DirectionalLightDirection.xyz);

                float directionalDiffuse =
                    max(dot(normal, directionalDirection), 0.0);

                float3 directionalReflection =
                    reflect(-directionalDirection, normal);

                float directionalSpecular =
                    pow(
                        max(dot(viewDirection, directionalReflection), 0.0),
                        _Shininess
                    );

                float3 directionalLighting =
                    _DirectionalLightColor.rgb *
                    (
                        directionalDiffuse +
                        directionalSpecular * _SpecularColor.rgb
                    );

                float3 pointDirection =
                    _PointLightPosition.xyz - i.worldPosition;

                float distanceToPointLight =
                    length(pointDirection);

                pointDirection =
                    normalize(pointDirection);

                float pointDiffuse =
                    max(dot(normal, pointDirection), 0.0);

                float3 pointReflection =
                    reflect(-pointDirection, normal);

                float pointSpecular =
                    pow(
                        max(dot(viewDirection, pointReflection), 0.0),
                        _Shininess
                    );

                float attenuation =
                    saturate(
                        1.0 - distanceToPointLight / _PointLightRange
                    );

                float3 pointLighting =
                    _PointLightColor.rgb *
                    (
                        pointDiffuse +
                        pointSpecular * _SpecularColor.rgb
                    ) * attenuation;

                float3 lighting =
                    ambient +
                    directionalLighting +
                    pointLighting;

                fixed4 textureColor =
                    tex2D(_MainTex, i.uv);

                float3 finalColor =
                    textureColor.rgb * lighting;

                return fixed4(finalColor, textureColor.a);
            }

            ENDCG
        }
    }
}
