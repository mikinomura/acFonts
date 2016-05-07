Shader "Anclin/Experiments/Vertex Animation"
{
	Properties
	{
		_Value1( "Value 1", Float ) = 0
		_Value2( "Value 2", Float ) = 0
		_Value3( "Value 3", Float ) = 0
		_MainColor("Main Color", Color) = (1.0,1.0,1.0,1.0)
		_Rotation("Rotation", Float) = 0
		_MainTex("Base texture", 2D) = "white" {}//テクスチャ
	}
	
	SubShader
	{
		Pass
		{
			//Tags { "LightMode" = "ForwardBase" }
			Tags {"Queue"="Transparent" "RenderType" = "Transparent" 
				"LightMode" = "ForwardBase"} // for shadows
			//透明にするために必要↓
			Blend SrcAlpha OneMinusSrcAlpha

			// Render faces when looking from the inside
			Cull Off

			CGPROGRAM

			// Pragmas
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase

			#include "UnityCG.cginc"

			#include "AutoLight.cginc"

			// User defined variables
			uniform float4 _Color;
			uniform float _Value1;
			uniform float _Value2;
			uniform float _Value3;
			uniform float4 _MainColor;
			uniform float _Rotation;
			sampler2D _MainTex;

			// Base input structs
			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0; //1つ目のUV座標セマンティクス
			};

			struct fragmentInput
			{
				float4 pos : SV_POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;

				LIGHTING_COORDS(0,1)
			};

			float4 _MainTex_ST;//?

			// Vertex function
			fragmentInput vert( vertexInput i )
			{
				fragmentInput o;

				// VERTEX ANIMATION ///////////////////////////////////////////////////////////////

				// Fat mesh
				//i.vertex.xyz += i.normal * _Value1;
				
				// Waving mesh
				//i.vertex.x += sin( ( i.vertex.y + _Time * _Value3 ) * _Value2 ) * _Value1;

				// Bubbling mesh
				//i.vertex.xyz += i.normal * ( sin( (i.vertex.x + _Time * _Value3) * _Value2 ) + cos( (i.vertex.z + _Time * _Value3) * _Value2 )  ) * _Value1;
				i.vertex.xyz += i.normal * (sin((i.vertex.x + i.vertex.y + _Time) * _Value2) + cos(i.vertex.y + i.vertex.z + _Time)) * _Value3 * 0.01;
				i.vertex.xyz += i.vertex.xyz * (sin((i.vertex.x + i.vertex.y + _Time) * _Value2) + cos(i.vertex.y + i.vertex.z + _Time)) * _Value1;
				//i.vertex.xyz = pos + i.normal * ( sin( (i.vertex.x + _Time * _Value3) * _Value2 )) * _Value1;
				//i.vertex.y = sin(_Time * 20.0) * 0.2;



				//////////////////////////////////////////////////////////// EO VERTEX ANIMATION //

				// COLOR
				//o.color = i.texcoord;								// UV
				//o.color = float4(i.normal, 1 ) * 0.2 +0.5;
				o.color = float4(_MainColor.r * i.texcoord.x * 0.5 , _MainColor.g * i.texcoord.y * 0.5, _MainColor.b, 1);		// Normals
				o.uv = TRANSFORM_TEX (i.texcoord, _MainTex);
				// This line must be after the vertex manipulation
				o.pos = mul( UNITY_MATRIX_MVP, i.vertex );

				//for light
				TRANSFER_VERTEX_TO_FRAGMENT(o);
				return o;
			}

			// Fragment function
			float4 frag( fragmentInput i ) : Color
			{	
				float attenuation = LIGHT_ATTENUATION(i); //for light
				i.color.a = 0.5;
				fixed4 texcol = tex2D (_MainTex, i.uv);
				return texcol * i.color * attenuation;
			}

			ENDCG
		}
	}

	// Fallback commented out during development
	// Fallback "Diffuse"
	Fallback "VertexLit"
}
