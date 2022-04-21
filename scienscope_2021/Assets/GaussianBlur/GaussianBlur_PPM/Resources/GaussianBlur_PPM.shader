/*
GaussianBlur_PPM.shader
uses properties and the global textures to generate a blurred effect on the screen.

*/


Shader "Custom/GaussianBlur_PPM"
{
	Properties
	{

		_MainTex ("Texture", 2D) = "white" {}
        //_Magnitude ("Magnitude", Float) = 1

		 //_Iterations ("_Iterations", Range(0,1000)) = 250

		/*
		[PerRendererData] _MainTex ("_MainTex", 2D) = "white" {}
		_Lightness ("_Lightness", Range(0,2)) = 1

        _Saturation ("_Saturation", Range(-10,10)) = 1

		_TintColor ("_TintColor",Color) = (1.0,1.0,1.0,0.0)


		 // required for UI.Mask
         [HideInInspector] _StencilComp ("Stencil Comparison", Float) = 8
         [HideInInspector] _Stencil ("Stencil ID", Float) = 0
         [HideInInspector] _StencilOp ("Stencil Operation", Float) = 0
         [HideInInspector] _StencilWriteMask ("Stencil Write Mask", Float) = 255
         [HideInInspector] _StencilReadMask ("Stencil Read Mask", Float) = 255
         [HideInInspector] _ColorMask ("Color Mask", Float) = 15
		 //
		 */
	}
	SubShader
	{
		Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }
		Pass
		{
            //Blend One One
			Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            ZTest Always
        
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
                float4 color : COLOR;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
                float alpha : TEXCOORD1;
                float4 projPos : TEXCOORD2;
			};

			sampler2D _MainTex;
            sampler2D_float _CameraDepthTexture;
			float4 _MainTex_ST;
            //float _Magnitude;
			//int _Iterations;
			
			v2f vert (appdata v)
			{
				v2f o;
                
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.alpha = v.color.a;
				o.projPos = ComputeScreenPos(o.vertex);
				COMPUTE_EYEDEPTH(o.projPos.z);
                
				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
                float sceneEyeDepth = DECODE_EYEDEPTH(tex2D(_CameraDepthTexture, i.projPos.xy / i.projPos.w));
                float zCull = sceneEyeDepth > i.projPos.z;
				
				float4 this = tex2D(_MainTex, i.uv) * zCull;
				//return this * zCull * i.alpha;
				return this;
				

			}
			ENDCG
		}
	}
}

