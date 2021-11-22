// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Screen Rain"
{
	Properties
	{
		_MainTex ( "Screen", 2D ) = "black" {}
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_Tiling("Tiling", Float) = 1
		_Amount("Amount", Range( 0 , 1)) = 0
		_Speed("Speed", Float) = 0
		_NoiseAmount("Noise Amount", Range( 0 , 1)) = 1
		_NoiseSpeed("Noise Speed", Float) = 0
		_NoiseTiling("Noise Tiling", Float) = 4
		_TimeNoiseAmount("Time Noise Amount", Range( 0 , 1)) = 1
		_TimeNoiseSpeed("Time Noise Speed", Float) = 0
		_TimeNoiseTiling("Time Noise Tiling", Float) = 4
	}

	SubShader
	{
		
		
		ZTest Always
		Cull Off
		ZWrite Off

		
		Pass
		{ 
			CGPROGRAM 

			#pragma vertex vert_img_custom 
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata_img_custom
			{
				float4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
				
			};

			struct v2f_img_custom
			{
				float4 pos : SV_POSITION;
				half2 uv   : TEXCOORD0;
				half2 stereoUV : TEXCOORD2;
		#if UNITY_UV_STARTS_AT_TOP
				half4 uv2 : TEXCOORD1;
				half4 stereoUV2 : TEXCOORD3;
		#endif
				
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			uniform sampler2D _TextureSample1;
			uniform float _Tiling;
			uniform float _Speed;
			uniform float _TimeNoiseTiling;
			uniform float _TimeNoiseSpeed;
			uniform float _TimeNoiseAmount;
			uniform float _NoiseTiling;
			uniform float _NoiseSpeed;
			uniform float _NoiseAmount;
			uniform float _Amount;
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			

			v2f_img_custom vert_img_custom ( appdata_img_custom v  )
			{
				v2f_img_custom o;
				
				o.pos = UnityObjectToClipPos ( v.vertex );
				o.uv = float4( v.texcoord.xy, 1, 1 );

				#if UNITY_UV_STARTS_AT_TOP
					o.uv2 = float4( v.texcoord.xy, 1, 1 );
					o.stereoUV2 = UnityStereoScreenSpaceUVAdjust ( o.uv2, _MainTex_ST );

					if ( _MainTex_TexelSize.y < 0.0 )
						o.uv.y = 1.0 - o.uv.y;
				#endif
				o.stereoUV = UnityStereoScreenSpaceUVAdjust ( o.uv, _MainTex_ST );
				return o;
			}

			half4 frag ( v2f_img_custom i ) : SV_Target
			{
				#ifdef UNITY_UV_STARTS_AT_TOP
					half2 uv = i.uv2;
					half2 stereoUV = i.stereoUV2;
				#else
					half2 uv = i.uv;
					half2 stereoUV = i.stereoUV;
				#endif	
				
				half4 finalColor;

				// ase common template code
				float2 uv19 = i.uv.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult14 = (float2(( _ScreenParams.x / _ScreenParams.y ) , 1.0));
				float2 temp_cast_0 = (_TimeNoiseTiling).xx;
				float2 appendResult49 = (float2(0.0 , ( _Time.y * _TimeNoiseSpeed )));
				float2 uv41 = i.uv.xy * temp_cast_0 + appendResult49;
				float simplePerlin2D40 = snoise( uv41 );
				float clampResult44 = clamp( simplePerlin2D40 , 0.0 , 1.0 );
				float2 appendResult25 = (float2(0.0 , ( ( _Time.y * _Speed ) + ( clampResult44 * _TimeNoiseAmount ) )));
				float2 temp_cast_1 = (_NoiseTiling).xx;
				float2 appendResult69 = (float2(0.0 , ( _Time.y * _NoiseSpeed )));
				float2 uv71 = i.uv.xy * temp_cast_1 + ( appendResult69 + float2( 10,10 ) );
				float simplePerlin2D72 = snoise( uv71 );
				float2 appendResult77 = (float2(( simplePerlin2D72 * _NoiseAmount ) , 0.0));
				float2 uv8 = i.uv.xy * ( _Tiling * appendResult14 ) + ( appendResult25 + appendResult77 );
				float2 temp_cast_2 = (tex2D( _TextureSample1, uv8 ).r).xx;
				float2 uv17 = i.uv.xy * float2( 1,1 ) + temp_cast_2;
				float2 lerpResult18 = lerp( uv19 , uv17 , _Amount);
				

				finalColor = tex2D( _MainTex, lerpResult18 );

				return finalColor;
			} 
			ENDCG 
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=15600
0;549;1446;829;2457.079;1358.123;2.139815;True;False
Node;AmplifyShaderEditor.RangedFloatNode;47;-5504.688,1293.482;Float;False;Property;_TimeNoiseSpeed;Time Noise Speed;11;0;Create;True;0;0;False;0;0;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;45;-5478.621,1185.736;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-5210.995,1189.212;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;66;-5576.911,1960.721;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;67;-5602.978,2068.467;Float;False;Property;_NoiseSpeed;Noise Speed;8;0;Create;True;0;0;False;0;0;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-5159.417,1017.042;Float;False;Property;_TimeNoiseTiling;Time Noise Tiling;12;0;Create;True;0;0;False;0;4;8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;49;-4983.056,1155.771;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-5309.285,1964.197;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;41;-4757.389,1116.306;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;4,4;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;69;-5081.346,1930.756;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;79;-5103.598,2151.431;Float;False;Constant;_NoiseOffset;Noise Offset;13;0;Create;True;0;0;False;0;10,10;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.NoiseGeneratorNode;40;-4426.934,1097.454;Float;True;Simplex2D;1;0;FLOAT2;0.1,0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;80;-4865.777,1994.024;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;70;-5002.777,1776.629;Float;False;Property;_NoiseTiling;Noise Tiling;9;0;Create;True;0;0;False;0;4;8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;51;-3849.282,1178.938;Float;False;Property;_TimeNoiseAmount;Time Noise Amount;10;0;Create;True;0;0;False;0;1;0.192;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;44;-4152.909,1084.277;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-3697.219,881.5978;Float;False;Property;_Speed;Speed;3;0;Create;True;0;0;False;0;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;23;-3533.457,789.3453;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;71;-4600.749,1875.893;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;4,4;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;72;-4270.294,1857.041;Float;True;Simplex2D;1;0;FLOAT2;0.1,0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;73;-3923.611,1963.953;Float;False;Property;_NoiseAmount;Noise Amount;7;0;Create;True;0;0;False;0;1;0.015;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenParams;12;-3452.492,448.903;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-3336.261,840.67;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;-3525.552,1084.666;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;42;-2949.238,756.7897;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;16;-3208.842,428.3325;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-3546.908,1854.848;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-3078.593,274.0028;Float;False;Property;_Tiling;Tiling;1;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;25;-2737.748,694.1118;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;14;-2963.693,413.4029;Float;False;FLOAT2;4;0;FLOAT;1;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;77;-3240.708,1794.292;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;81;-2510.714,728.8383;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-2799.094,327.3027;Float;False;2;2;0;FLOAT;0;False;1;FLOAT2;1,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-2539.519,428.962;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;5,5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-2197.389,383.8124;Float;True;Property;_TextureSample1;Texture Sample 1;0;0;Create;True;0;0;False;0;None;657476f93a9e6fb42a70a6dbc6f47438;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-1352.281,-287.3864;Float;False;Property;_Amount;Amount;2;0;Create;True;0;0;False;0;0;0.125;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;19;-1658.126,-614.0152;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;17;-1645.814,-345.3368;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;18;-978.1186,-531.8642;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;5;-679.3896,-619.5148;Float;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;30;-4239.978,-941.2803;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;38;-3305.154,-816.6014;Float;True;Simplex2D;1;0;FLOAT2;0.1,0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-4268.266,-1076.427;Float;False;Property;_TextureNoiseTiling;Texture Noise Tiling;5;0;Create;True;0;0;False;0;10;250;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;39;-3299.989,-1107.958;Float;True;Simplex2D;1;0;FLOAT2;0.1,0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;35;-3825.814,-977.7712;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-4458.559,-712.3306;Float;False;Property;_TextureNoiseSpeed;Texture Noise Speed;4;0;Create;True;0;0;False;0;1;0.5;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-479.9745,-589.463;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;36;-3635.611,-791.2488;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;37;-3630.446,-1089.106;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;34;-3827.534,-739.4967;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-4573.562,-852.5563;Float;False;Property;_TextureNoiseOffset;Texture Noise Offset;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-4060.117,-841.4158;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;29;-4568.73,-941.0909;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;335.853,-226.0791;Float;False;True;2;Float;ASEMaterialInspector;0;2;Screen Rain;c71b220b631b6344493ea3cf87110c93;0;0;SubShader 0 Pass 0;1;False;False;True;2;False;-1;False;False;True;2;False;-1;True;7;False;-1;False;True;0;False;0;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;1;0;FLOAT4;0,0,0,0;False;0
WireConnection;46;0;45;0
WireConnection;46;1;47;0
WireConnection;49;1;46;0
WireConnection;68;0;66;0
WireConnection;68;1;67;0
WireConnection;41;0;48;0
WireConnection;41;1;49;0
WireConnection;69;1;68;0
WireConnection;40;0;41;0
WireConnection;80;0;69;0
WireConnection;80;1;79;0
WireConnection;44;0;40;0
WireConnection;71;0;70;0
WireConnection;71;1;80;0
WireConnection;72;0;71;0
WireConnection;27;0;23;0
WireConnection;27;1;26;0
WireConnection;50;0;44;0
WireConnection;50;1;51;0
WireConnection;42;0;27;0
WireConnection;42;1;50;0
WireConnection;16;0;12;1
WireConnection;16;1;12;2
WireConnection;75;0;72;0
WireConnection;75;1;73;0
WireConnection;25;1;42;0
WireConnection;14;0;16;0
WireConnection;77;0;75;0
WireConnection;81;0;25;0
WireConnection;81;1;77;0
WireConnection;15;0;10;0
WireConnection;15;1;14;0
WireConnection;8;0;15;0
WireConnection;8;1;81;0
WireConnection;6;1;8;0
WireConnection;17;1;6;1
WireConnection;18;0;19;0
WireConnection;18;1;17;0
WireConnection;18;2;20;0
WireConnection;30;0;29;0
WireConnection;30;1;28;0
WireConnection;38;0;36;0
WireConnection;39;0;37;0
WireConnection;35;0;32;0
WireConnection;4;0;5;0
WireConnection;4;1;18;0
WireConnection;36;0;33;0
WireConnection;36;1;34;0
WireConnection;37;0;33;0
WireConnection;37;1;35;0
WireConnection;34;1;32;0
WireConnection;32;0;30;0
WireConnection;32;1;31;0
WireConnection;1;0;4;0
ASEEND*/
//CHKSM=90A07009407E2404EEA2268D47740E0EF1CEFD36