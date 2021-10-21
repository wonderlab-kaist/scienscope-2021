// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Mirza Beig/Standard/Terrain Rain"
{
	Properties
	{
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		[HideInInspector]_Control("Control", 2D) = "white" {}
		[HideInInspector]_Splat0("Splat0", 2D) = "white" {}
		[HideInInspector]_Splat1("Splat1", 2D) = "white" {}
		[HideInInspector]_Splat2("Splat2", 2D) = "white" {}
		[HideInInspector]_Splat3("Splat3", 2D) = "white" {}
		[HideInInspector]_Normal0("Normal0", 2D) = "white" {}
		[HideInInspector]_Normal1("Normal1", 2D) = "white" {}
		[HideInInspector]_Normal2("Normal2", 2D) = "white" {}
		[HideInInspector]_Normal3("Normal3", 2D) = "white" {}
		_TextureNoiseAmount("Texture Noise Amount", Range( 0 , 1)) = 1
		_TextureNoiseSpeed("Texture Noise Speed", Range( 0 , 10)) = 1
		_TextureNoiseTiling("Texture Noise Tiling", Float) = 10
		_TextureNoiseOffset("Texture Noise Offset", Float) = 0
		_NormalNoiseAmount("Normal Noise Amount", Range( 0 , 1)) = 1
		_NormalNoiseSpeed("Normal Noise Speed", Range( 0 , 10)) = 1
		_NormalNoiseTiling("Normal Noise Tiling", Float) = 10
		_NormalNoiseOffset("Normal Noise Offset", Float) = 0
		[HideInInspector]_Float12("Float 12", Range( 0 , 1)) = 1
		[HideInInspector]_Float10("Float 10", Range( 0 , 1)) = 1
		[HideInInspector]_Float7("Float 7", Range( 0 , 1)) = 1
		[HideInInspector]_Float9("Float 9", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry-100" "SplatCount"="4" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Control;
		uniform float4 _Control_ST;
		uniform sampler2D _Normal0;
		uniform sampler2D _Splat0;
		uniform float4 _Splat0_ST;
		uniform float _NormalNoiseTiling;
		uniform float _NormalNoiseOffset;
		uniform float _NormalNoiseSpeed;
		uniform float _NormalNoiseAmount;
		uniform sampler2D _Normal1;
		uniform sampler2D _Splat1;
		uniform float4 _Splat1_ST;
		uniform sampler2D _Normal2;
		uniform sampler2D _Splat2;
		uniform float4 _Splat2_ST;
		uniform sampler2D _Normal3;
		uniform sampler2D _Splat3;
		uniform float4 _Splat3_ST;
		uniform float _Float7;
		uniform float _TextureNoiseTiling;
		uniform float _TextureNoiseOffset;
		uniform float _TextureNoiseSpeed;
		uniform float _TextureNoiseAmount;
		uniform float _Float10;
		uniform float _Float9;
		uniform float _Float12;
		uniform float _Metallic;
		uniform float _Smoothness;


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


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float localCalculateTangentsStandard16_g2 = ( 0.0 );
			v.tangent.xyz = cross ( v.normal, float3( 0, 0, 1 ) );
			v.tangent.w = -1;
			float3 temp_cast_0 = (localCalculateTangentsStandard16_g2).xxx;
			v.vertex.xyz += temp_cast_0;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Control = i.uv_texcoord * _Control_ST.xy + _Control_ST.zw;
			float4 tex2DNode14 = tex2D( _Control, uv_Control );
			float dotResult10 = dot( tex2DNode14 , float4(1,1,1,1) );
			float SplatWeight12 = dotResult10;
			float localSplatClip15 = ( SplatWeight12 );
			float SplatWeight15 = SplatWeight12;
			#if !defined(SHADER_API_MOBILE) && defined(TERRAIN_SPLAT_ADDPASS)
				clip(SplatWeight15 == 0.0f ? -1 : 1);
			#endif
			float4 temp_output_11_0 = ( tex2DNode14 / ( localSplatClip15 + 0.001 ) );
			float2 uv_Splat0 = i.uv_texcoord * _Splat0_ST.xy + _Splat0_ST.zw;
			float2 temp_cast_1 = (_NormalNoiseTiling).xx;
			float temp_output_135_0 = ( ( _Time.y + _NormalNoiseOffset ) * _NormalNoiseSpeed );
			float2 appendResult134 = (float2(temp_output_135_0 , 0.0));
			float2 uv_TexCoord132 = i.uv_texcoord * temp_cast_1 + appendResult134;
			float simplePerlin2D126 = snoise( uv_TexCoord132 );
			float2 temp_cast_2 = (_NormalNoiseTiling).xx;
			float2 appendResult130 = (float2(0.0 , temp_output_135_0));
			float2 uv_TexCoord128 = i.uv_texcoord * temp_cast_2 + appendResult130;
			float simplePerlin2D133 = snoise( uv_TexCoord128 );
			float2 temp_cast_3 = (( ( simplePerlin2D126 + simplePerlin2D133 ) * _NormalNoiseAmount )).xx;
			float2 uv_TexCoord137 = i.uv_texcoord + temp_cast_3;
			float2 NormalNoise138 = uv_TexCoord137;
			float2 uv_Splat1 = i.uv_texcoord * _Splat1_ST.xy + _Splat1_ST.zw;
			float2 uv_Splat2 = i.uv_texcoord * _Splat2_ST.xy + _Splat2_ST.zw;
			float2 uv_Splat3 = i.uv_texcoord * _Splat3_ST.xy + _Splat3_ST.zw;
			float4 weightedBlendVar46 = temp_output_11_0;
			float4 weightedBlend46 = ( weightedBlendVar46.x*tex2D( _Normal0, ( uv_Splat0 + NormalNoise138 ) ) + weightedBlendVar46.y*tex2D( _Normal1, ( uv_Splat1 + NormalNoise138 ) ) + weightedBlendVar46.z*tex2D( _Normal2, ( uv_Splat2 + NormalNoise138 ) ) + weightedBlendVar46.w*tex2D( _Normal3, ( uv_Splat3 + NormalNoise138 ) ) );
			o.Normal = UnpackNormal( weightedBlend46 );
			float4 appendResult29 = (float4(1.0 , 1.0 , 1.0 , _Float7));
			float2 temp_cast_5 = (_TextureNoiseTiling).xx;
			float temp_output_122_0 = ( ( _Time.y + _TextureNoiseOffset ) * _TextureNoiseSpeed );
			float2 appendResult115 = (float2(temp_output_122_0 , 0.0));
			float2 uv_TexCoord106 = i.uv_texcoord * temp_cast_5 + appendResult115;
			float simplePerlin2D101 = snoise( uv_TexCoord106 );
			float2 temp_cast_6 = (_TextureNoiseTiling).xx;
			float2 appendResult123 = (float2(0.0 , temp_output_122_0));
			float2 uv_TexCoord118 = i.uv_texcoord * temp_cast_6 + appendResult123;
			float simplePerlin2D120 = snoise( uv_TexCoord118 );
			float2 temp_cast_7 = (( ( simplePerlin2D101 + simplePerlin2D120 ) * _TextureNoiseAmount )).xx;
			float2 uv_TexCoord103 = i.uv_texcoord + temp_cast_7;
			float2 TextureNoise125 = uv_TexCoord103;
			float4 appendResult31 = (float4(1.0 , 1.0 , 1.0 , _Float10));
			float4 appendResult24 = (float4(1.0 , 1.0 , 1.0 , _Float9));
			float4 appendResult38 = (float4(1.0 , 1.0 , 1.0 , _Float12));
			float4 weightedBlendVar4 = temp_output_11_0;
			float4 weightedBlend4 = ( weightedBlendVar4.x*( appendResult29 * tex2D( _Splat0, ( uv_Splat0 + TextureNoise125 ) ) ) + weightedBlendVar4.y*( appendResult31 * tex2D( _Splat1, ( uv_Splat1 + TextureNoise125 ) ) ) + weightedBlendVar4.z*( appendResult24 * tex2D( _Splat2, ( uv_Splat2 + TextureNoise125 ) ) ) + weightedBlendVar4.w*( appendResult38 * tex2D( _Splat3, ( uv_Splat3 + TextureNoise125 ) ) ) );
			o.Albedo = weightedBlend4.xyz;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}

	Dependency "BaseMapShader"="ASESampleShaders/SimpleTerrainBase"
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15600
0;770;1446;608;6690.124;1401.31;5.55057;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;129;-7471.628,21.77243;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;141;-7494.934,109.7757;Float;False;Property;_NormalNoiseOffset;Normal Noise Offset;36;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;144;-7509.553,-585.5964;Float;False;Property;_TextureNoiseOffset;Texture Noise Offset;32;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;105;-7504.721,-674.1309;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;143;-7220.43,47.3656;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;131;-7446.333,231.7021;Float;False;Property;_NormalNoiseSpeed;Normal Noise Speed;34;0;Create;True;0;0;False;0;1;4;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;142;-7175.969,-674.3203;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;121;-7394.55,-445.3706;Float;False;Property;_TextureNoiseSpeed;Texture Noise Speed;30;0;Create;True;0;0;False;0;1;0.5;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;135;-7044.375,157.4075;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;122;-6996.109,-574.4559;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;134;-6808.651,15.3326;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;127;-7251.102,-83.3231;Float;False;Property;_NormalNoiseTiling;Normal Noise Tiling;35;0;Create;True;0;0;False;0;10;1000;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;130;-6810.371,253.6071;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;108;-7204.257,-809.4669;Float;False;Property;_TextureNoiseTiling;Texture Noise Tiling;31;0;Create;True;0;0;False;0;10;250;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;123;-6763.526,-472.5367;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;115;-6761.806,-710.8112;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;128;-6618.449,201.855;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;132;-6613.284,-96.00179;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;118;-6571.604,-524.2888;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;106;-6566.439,-822.1456;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;133;-6287.992,176.5024;Float;True;Simplex2D;1;0;FLOAT2;0.1,0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;126;-6282.827,-114.8544;Float;True;Simplex2D;1;0;FLOAT2;0.1,0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;120;-6241.147,-549.6414;Float;True;Simplex2D;1;0;FLOAT2;0.1,0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;101;-6235.982,-840.9983;Float;True;Simplex2D;1;0;FLOAT2;0.1,0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;140;-5831.533,-611.7635;Float;False;Property;_TextureNoiseAmount;Texture Noise Amount;29;0;Create;True;0;0;False;0;1;0.004;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;146;-5850.132,95.97482;Float;False;Property;_NormalNoiseAmount;Normal Noise Amount;33;0;Create;True;0;0;False;0;1;0.002;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;124;-5833.89,-755.7466;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;13;-2141.61,-751.5558;Float;False;Constant;_Vector0;Vector 0;9;0;Create;True;0;0;False;0;1,1,1,1;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;136;-5857.272,-53.06477;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;14;-2149.648,-949.3322;Float;True;Property;_Control;Control;20;1;[HideInInspector];Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DotProductOpNode;10;-1844.263,-768.8707;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;139;-5434.66,-736.778;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;145;-5495.856,-33.06624;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;103;-5259,-818.4539;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;12;-1712.746,-772.1589;Float;False;SplatWeight;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;137;-5243.87,-125.9942;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;125;-4883.074,-790.7476;Float;False;TextureNoise;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;88;-2419.586,1766.464;Float;False;138;NormalNoise;0;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;91;-2385.352,1961.185;Float;False;0;26;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CustomExpressionNode;15;-1492.762,-810.7764;Float;False;#if !defined(SHADER_API_MOBILE) && defined(TERRAIN_SPLAT_ADDPASS)$	clip(SplatWeight == 0.0f ? -1 : 1)@$#endif;1;True;1;True;SplatWeight;FLOAT;0;In;;SplatClip;False;False;0;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;64;-3758.658,17.64989;Float;False;0;36;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;70;-3766.463,926.4154;Float;False;0;26;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;87;-2472.454,1622.499;Float;False;0;27;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;63;-3762.895,-323.8749;Float;False;125;TextureNoise;0;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;96;-2472.946,915.1071;Float;False;0;30;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;90;-2379.028,2097.373;Float;False;138;NormalNoise;0;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;67;-3699.95,448.3355;Float;False;0;27;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;65;-3732.661,199.2358;Float;False;125;TextureNoise;0;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;86;-2426.473,1437.09;Float;False;138;NormalNoise;0;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;59;-3807.485,-465.1586;Float;False;0;30;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;68;-3649.738,594.9572;Float;False;125;TextureNoise;0;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;97;-2437.356,1051.39;Float;False;138;NormalNoise;0;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;71;-3760.139,1062.603;Float;False;125;TextureNoise;0;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;89;-2455.07,1254.204;Float;False;0;36;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;-1595.641,-680.2318;Float;False;Constant;_Float0;Float 0;9;0;Create;True;0;0;False;0;0.001;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;138;-4864.721,-59.63226;Float;False;NormalNoise;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;94;-2117.381,1258.049;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;93;-2173.809,1636.263;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;69;-3401.305,462.1;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-3282.209,804.824;Float;False;Property;_Float12;Float 12;37;1;[HideInInspector];Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;66;-3423.814,61.32093;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;72;-3488.867,952.978;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;61;-3453.11,-412.0594;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;95;-2118.571,968.2064;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-3129.56,-171.091;Float;False;Constant;_Float6;Float 6;13;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-3161.91,-599.2739;Float;False;Constant;_Float13;Float 13;13;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-3280.636,345.7909;Float;False;Property;_Float9;Float 9;40;1;[HideInInspector];Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-3171.401,254.108;Float;False;Constant;_Float8;Float 8;13;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-1258.437,-757.4415;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-3253.703,-89.69812;Float;False;Property;_Float10;Float 10;38;1;[HideInInspector];Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-3168.254,676.8918;Float;False;Constant;_Float11;Float 11;13;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-3305.142,-521.385;Float;False;Property;_Float7;Float 7;39;1;[HideInInspector];Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;92;-2107.756,1987.748;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;42;-1805.033,1190.938;Float;True;Property;_Normal1;Normal1;26;1;[HideInInspector];Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;31;-2975.39,-186.8491;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;11;-1163.641,-920.2318;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;36;-3160.473,3.58699;Float;True;Property;_Splat1;Splat1;22;1;[HideInInspector];Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;24;-3009.014,257.491;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;44;-1813.567,1524.107;Float;True;Property;_Normal2;Normal2;27;1;[HideInInspector];Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;30;-3148.037,-446.142;Float;True;Property;_Splat0;Splat0;21;1;[HideInInspector];Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;38;-2991.254,717.8919;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;26;-3140.183,910.5693;Float;True;Property;_Splat3;Splat3;24;1;[HideInInspector];Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;41;-1799.713,985.0127;Float;True;Property;_Normal0;Normal0;25;1;[HideInInspector];Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;47;-1799.343,1860.121;Float;True;Property;_Normal3;Normal3;28;1;[HideInInspector];Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;29;-3014.295,-595.5459;Float;False;FLOAT4;4;0;FLOAT;1;False;1;FLOAT;1;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;27;-3151.398,426.2226;Float;True;Property;_Splat2;Splat2;23;1;[HideInInspector];Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-2773.515,-142.5411;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-2836.815,401.3831;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-2829.269,810.7999;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SummedBlendNode;46;-1343.458,1229.648;Float;False;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-2780.422,-543.762;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-918.7034,205.9721;Float;False;Property;_Metallic;Metallic;18;0;Create;True;0;0;False;0;0;0.474;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SummedBlendNode;4;-581.8003,-155.7626;Float;False;5;0;COLOR;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT4;0,0,0,0;False;3;FLOAT4;0,0,0,0;False;4;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.UnpackScaleNormalNode;45;-1122.523,1135.265;Float;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;17;-916.7034,293.9719;Float;False;Property;_Smoothness;Smoothness;19;0;Create;True;0;0;False;0;0;0.815;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;49;-286.9666,2.18631;Float;False;Four Splats First Pass Terrain;0;;2;37452fdfb732e1443b7e39720d05b708;0;6;59;FLOAT4;0,0,0,0;False;60;FLOAT4;0,0,0,0;False;61;FLOAT3;0,0,0;False;57;FLOAT;0;False;58;FLOAT;0;False;62;FLOAT;0;False;6;FLOAT4;0;FLOAT3;14;FLOAT;56;FLOAT;45;FLOAT;19;FLOAT;17
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;188,-29;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Mirza Beig/Standard/Terrain Rain;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;-100;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;1;SplatCount=4;False;1;BaseMapShader=ASESampleShaders/SimpleTerrainBase;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;143;0;129;0
WireConnection;143;1;141;0
WireConnection;142;0;105;0
WireConnection;142;1;144;0
WireConnection;135;0;143;0
WireConnection;135;1;131;0
WireConnection;122;0;142;0
WireConnection;122;1;121;0
WireConnection;134;0;135;0
WireConnection;130;1;135;0
WireConnection;123;1;122;0
WireConnection;115;0;122;0
WireConnection;128;0;127;0
WireConnection;128;1;130;0
WireConnection;132;0;127;0
WireConnection;132;1;134;0
WireConnection;118;0;108;0
WireConnection;118;1;123;0
WireConnection;106;0;108;0
WireConnection;106;1;115;0
WireConnection;133;0;128;0
WireConnection;126;0;132;0
WireConnection;120;0;118;0
WireConnection;101;0;106;0
WireConnection;124;0;101;0
WireConnection;124;1;120;0
WireConnection;136;0;126;0
WireConnection;136;1;133;0
WireConnection;10;0;14;0
WireConnection;10;1;13;0
WireConnection;139;0;124;0
WireConnection;139;1;140;0
WireConnection;145;0;136;0
WireConnection;145;1;146;0
WireConnection;103;1;139;0
WireConnection;12;0;10;0
WireConnection;137;1;145;0
WireConnection;125;0;103;0
WireConnection;15;0;12;0
WireConnection;15;1;12;0
WireConnection;138;0;137;0
WireConnection;94;0;89;0
WireConnection;94;1;86;0
WireConnection;93;0;87;0
WireConnection;93;1;88;0
WireConnection;69;0;67;0
WireConnection;69;1;68;0
WireConnection;66;0;64;0
WireConnection;66;1;65;0
WireConnection;72;0;70;0
WireConnection;72;1;71;0
WireConnection;61;0;59;0
WireConnection;61;1;63;0
WireConnection;95;0;96;0
WireConnection;95;1;97;0
WireConnection;16;0;15;0
WireConnection;16;1;9;0
WireConnection;92;0;91;0
WireConnection;92;1;90;0
WireConnection;42;1;94;0
WireConnection;31;0;19;0
WireConnection;31;1;19;0
WireConnection;31;2;19;0
WireConnection;31;3;23;0
WireConnection;11;0;14;0
WireConnection;11;1;16;0
WireConnection;36;1;66;0
WireConnection;24;0;21;0
WireConnection;24;1;21;0
WireConnection;24;2;21;0
WireConnection;24;3;22;0
WireConnection;44;1;93;0
WireConnection;30;1;61;0
WireConnection;38;0;25;0
WireConnection;38;1;25;0
WireConnection;38;2;25;0
WireConnection;38;3;28;0
WireConnection;26;1;72;0
WireConnection;41;1;95;0
WireConnection;47;1;92;0
WireConnection;29;0;34;0
WireConnection;29;1;34;0
WireConnection;29;2;34;0
WireConnection;29;3;20;0
WireConnection;27;1;69;0
WireConnection;32;0;31;0
WireConnection;32;1;36;0
WireConnection;37;0;24;0
WireConnection;37;1;27;0
WireConnection;35;0;38;0
WireConnection;35;1;26;0
WireConnection;46;0;11;0
WireConnection;46;1;41;0
WireConnection;46;2;42;0
WireConnection;46;3;44;0
WireConnection;46;4;47;0
WireConnection;33;0;29;0
WireConnection;33;1;30;0
WireConnection;4;0;11;0
WireConnection;4;1;33;0
WireConnection;4;2;32;0
WireConnection;4;3;37;0
WireConnection;4;4;35;0
WireConnection;45;0;46;0
WireConnection;49;60;4;0
WireConnection;49;61;45;0
WireConnection;49;57;18;0
WireConnection;49;58;17;0
WireConnection;0;0;49;0
WireConnection;0;1;49;14
WireConnection;0;3;49;56
WireConnection;0;4;49;45
WireConnection;0;11;49;17
ASEEND*/
//CHKSM=CF262CF2EAF963EC8C7E335C4FF2B31852A34D43