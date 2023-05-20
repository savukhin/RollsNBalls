Shader "Custom/Ground Clip Shader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _ClipDistance ("Clip Distance", Range(0,100)) = 10
        _IlluminationDistance ("Illumination Distance", Range(0,5)) = 10
        [HDR] _IlluminationColor ("Illumination Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        half _ClipDistance;
        half _IlluminationDistance;
        fixed4 _IlluminationColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        half mean(half a, half b)
        {
            return (a + b) / 2;
        }
        fixed4 mean(fixed4 a, fixed4 b)
        {
            return fixed4(
                mean(a.r, b.r),
                mean(a.g, b.g),
                mean(a.b, b.b),
                mean(a.a, b.a)
            );
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            clip (_ClipDistance - IN.worldPos.z);
            // clip (frac((IN.worldPos.y+IN.worldPos.z*0.1) * 5) - 0.5);

            // from 0 to 1
            float illumination_force = max(_IlluminationDistance - (_ClipDistance - IN.worldPos.z), 0);

            // Albedo comes from a texture tinted by color
            fixed4 c_source = tex2D (_MainTex, IN.uv_MainTex) * _Color * (1 - illumination_force);
            fixed4 c_illimination = tex2D (_MainTex, IN.uv_MainTex) * _IlluminationColor * illumination_force;
            // fixed4 c = fixed4( c_source.r, c_source.g, c_source.b, 1);
            fixed4 c = mean(c_source, c_illimination);

            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
