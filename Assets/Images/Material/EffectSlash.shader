Shader "Custom/EffectSlash"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MaskTex ("Mask", 2D) = "black" { }
        _NoiseTex ("Noise", 2D) = "black" { }
        _NoiseInfluence ("Noise Influence", Range(0, 2)) = 1
        _VelocityOffset ("Scale.xy Offset.zw", Vector) = (1,1,0,0)
        [Toggle(MULTIPLY_ON)] _isMultiplicative ("Multiplicative Noise", Float) = 0
        [Header(Scroll)] _NoiseScroll ("Noise - Scroll", Vector) = (0,0,0,0)
        _InnerColor ("Color", Color) = (0,0,0,0)
        _GlowRange ("GlowRange - Smooth", Range(0, 1)) = 0.5
        [Header(Soft Particle)] [Toggle(_USESOFTPARTICLE)] _isSoftParticle ("Soft Particle", Float) = 0
        _InvFade ("Soft Particles Factor", Range(0.01, 5)) = 1
        [Header(Blending)] [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend ("BlendSource", Float) = 5
        [Enum(UnityEngine.Rendering.BlendMode)] _DstBlend ("BlendDestination", Float) = 10
        [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 0
        [Enum(Off,0,On,1)] _ZWrite ("ZWrite", Float) = 1
        [Separator] [Header(Stencil)] _StencilRef ("Stencil Ref", Float) = 1
        [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("Stencil Comparison", Float) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilOp ("Stencil Pass", Float) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilFail ("Stencil Fail", Float) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilZFail ("Stencil ZFail", Float) = 0
        [Header(Shadows)] [Toggle(_CASTSHADOW)] _CastShadow ("Cast Shadow", Float) = 1
        _offsetX ("Offset X", Float) = 0
        _offsetY ("Offset Y", Float) = 0
        [Separator] _dummy ("DummyValue", Float) = 0
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
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
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
