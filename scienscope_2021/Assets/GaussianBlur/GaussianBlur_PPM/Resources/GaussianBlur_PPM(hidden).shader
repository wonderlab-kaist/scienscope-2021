// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/GaussianBlur_PPM"
{

	HLSLINCLUDE
        
			// This include uses a relative path, and may need to be updated if shaders are moved or
			// a different version of Unity's PostFX stack is being used (eg. if your version of the
			// project imports the PostFX stack from Package Manager).
			//#include "../../../../../../PostProcessing/Shaders/StdLib.hlsl"
			//#include "Packages/com.unity.postprocessing/Shaders/StdLib.hlsl"
			#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

			TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
			TEXTURE2D_SAMPLER2D(_GlobalBlurTex, sampler_GlobalBlurTex);
			float4 _MainTex_TexelSize;

			int _Iterations;
			float _Lightness;
			float _Saturation;
			float4 _Tint;

			
			float3 Frag(VaryingsDefault i) : SV_Target
			{
				//float2 mag = _Magnitude * 0.5.xy;
				//float2 distortion = SAMPLE_TEXTURE2D(_GlobalDistortionTex, sampler_GlobalDistortionTex, i.texcoord).xy * mag;
				float3 color = float3(0,0,0);
				color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord).xyz;

				float3 GBT = SAMPLE_TEXTURE2D(_GlobalBlurTex, sampler_GlobalBlurTex, i.texcoord).xyz ;
				float w = (GBT.r + GBT.b + GBT.g)/3;

				float px = 1 / _ScreenParams.x;
				float py = 1 / _ScreenParams.y;


				color = float3(0,0,0);

				float d = 0.0;
				float cw = 0.0;
				float totalWeight = 0.0;

				float2 offset = float2(0, 0);

				[loop]
				for (int x = -10; x <= 10; x++)
				{
					[loop]
					for (int y = -10; y <= 10; y++)
					{
						d = sqrt(pow(x, 2) + pow(y, 2));

						if (d == 0)
						{
							cw = 0;
						}
						else
						{
							//cw = 1 / pow(d, d);

							cw = 1 / d;
						}
							
						totalWeight += cw;
						offset = float2(x * px, y * py) * (_Iterations/10.00) * w;
						color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + offset).xyz * cw;


						totalWeight += cw;
						offset = float2(x * px, y * py) * (_Iterations / 5.00) * w;
						color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + offset).xyz * cw;

						totalWeight += cw;
						offset = float2(x * px, y * py) * (_Iterations / 2.50) * w;
						color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + offset).xyz * cw;

					}
				}


				color = color / totalWeight;
				//color *= _Lightness;
				//color *= _Tint;

				color = lerp(color, color * _Lightness, w);
				color = lerp(color, color * _Tint.xyz, w);

				float3 intensity = dot(color.rgb, float3(0.299, 0.587, 0.114));
				float3 sat = lerp(intensity, color.rgb, _Saturation);

				color = lerp(color,sat, w);

				return color;


			}

	ENDHLSL

	SubShader
	{
		Cull Off
		ZWrite Off
		ZTest Always

		Pass
		{
			HLSLPROGRAM

				#pragma vertex VertDefault
				#pragma fragment Frag

			ENDHLSL
			
		}

	}
}








