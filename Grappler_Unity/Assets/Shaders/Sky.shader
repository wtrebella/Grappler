// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:34156,y:32723,varname:node_3138,prsc:2|emission-4194-RGB,voffset-2223-OUT;n:type:ShaderForge.SFN_Tex2d,id:4194,x:33686,y:32767,varname:node_4194,prsc:2,tex:1f2dda1120ca1447d982e449ee9182fe,ntxv:0,isnm:False|UVIN-4530-OUT,TEX-5346-TEX;n:type:ShaderForge.SFN_Time,id:4872,x:32407,y:32770,varname:node_4872,prsc:2;n:type:ShaderForge.SFN_Tex2dAsset,id:5346,x:33686,y:32940,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_5346,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1f2dda1120ca1447d982e449ee9182fe,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Fmod,id:2120,x:33111,y:32826,varname:node_2120,prsc:2|A-5367-OUT,B-4410-OUT;n:type:ShaderForge.SFN_Multiply,id:9044,x:32601,y:32791,varname:node_9044,prsc:2|A-4872-T,B-6917-OUT,C-812-OUT;n:type:ShaderForge.SFN_If,id:4410,x:32925,y:32837,varname:node_4410,prsc:2|A-5367-OUT,B-9039-OUT,GT-2588-OUT,EQ-2588-OUT,LT-5426-OUT;n:type:ShaderForge.SFN_Vector1,id:2588,x:32755,y:32892,varname:node_2588,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:5426,x:32755,y:32951,varname:node_5426,prsc:2,v1:-1;n:type:ShaderForge.SFN_Vector1,id:9039,x:32755,y:32835,varname:node_9039,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:4530,x:33491,y:32767,varname:node_4530,prsc:2|A-9173-OUT,B-1718-OUT;n:type:ShaderForge.SFN_Vector1,id:9173,x:33304,y:32767,varname:node_9173,prsc:2,v1:0;n:type:ShaderForge.SFN_TexCoord,id:6918,x:32601,y:32630,varname:node_6918,prsc:2,uv:0;n:type:ShaderForge.SFN_If,id:1718,x:33304,y:32826,varname:node_1718,prsc:2|A-2120-OUT,B-9488-OUT,GT-2120-OUT,EQ-2120-OUT,LT-6597-OUT;n:type:ShaderForge.SFN_Vector1,id:9488,x:33111,y:32767,varname:node_9488,prsc:2,v1:0;n:type:ShaderForge.SFN_Abs,id:4556,x:33221,y:32967,varname:node_4556,prsc:2|IN-2120-OUT;n:type:ShaderForge.SFN_OneMinus,id:6597,x:33384,y:32967,varname:node_6597,prsc:2|IN-4556-OUT;n:type:ShaderForge.SFN_Add,id:5367,x:32826,y:32675,varname:node_5367,prsc:2|A-6918-V,B-9044-OUT;n:type:ShaderForge.SFN_Slider,id:6917,x:32250,y:32920,ptovrint:False,ptlb:Pan Speed,ptin:_PanSpeed,varname:node_6917,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.2478638,max:1;n:type:ShaderForge.SFN_Vector1,id:812,x:32407,y:33001,varname:node_812,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Sin,id:8959,x:33777,y:33215,varname:node_8959,prsc:2|IN-760-OUT;n:type:ShaderForge.SFN_Time,id:174,x:33088,y:33301,varname:node_174,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:282,x:33111,y:33123,varname:node_282,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:9516,x:33299,y:33134,varname:node_9516,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-282-U;n:type:ShaderForge.SFN_Add,id:8413,x:33477,y:33228,varname:node_8413,prsc:2|A-9516-OUT,B-8962-OUT;n:type:ShaderForge.SFN_Append,id:2223,x:34073,y:33297,varname:node_2223,prsc:2|A-213-OUT,B-213-OUT,C-2581-OUT;n:type:ShaderForge.SFN_Vector1,id:213,x:34003,y:33227,varname:node_213,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:760,x:33651,y:33348,varname:node_760,prsc:2|A-8413-OUT,B-8487-OUT;n:type:ShaderForge.SFN_Tau,id:8487,x:33493,y:33413,varname:node_8487,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8962,x:33293,y:33423,varname:node_8962,prsc:2|A-174-T,B-4451-OUT;n:type:ShaderForge.SFN_Slider,id:4451,x:32981,y:33482,ptovrint:False,ptlb:Wave Speed,ptin:_WaveSpeed,varname:node_4451,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:0.5231037,max:2;n:type:ShaderForge.SFN_Slider,id:2579,x:33412,y:33596,ptovrint:False,ptlb:Wave Intensity,ptin:_WaveIntensity,varname:node_2579,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4000072,max:1;n:type:ShaderForge.SFN_Multiply,id:2581,x:33936,y:33427,varname:node_2581,prsc:2|A-8959-OUT,B-2579-OUT;proporder:5346-6917-4451-2579;pass:END;sub:END;*/

Shader "Shader Forge/Sky" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _PanSpeed ("Pan Speed", Range(-1, 1)) = 0.2478638
        _WaveSpeed ("Wave Speed", Range(-2, 2)) = 0.5231037
        _WaveIntensity ("Wave Intensity", Range(0, 1)) = 0.4000072
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _PanSpeed;
            uniform float _WaveSpeed;
            uniform float _WaveIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float node_213 = 0.0;
                float4 node_174 = _Time + _TimeEditor;
                float node_8959 = sin(((o.uv0.r.r+(node_174.g*_WaveSpeed))*6.28318530718));
                v.vertex.xyz += float3(node_213,node_213,(node_8959*_WaveIntensity));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 node_4872 = _Time + _TimeEditor;
                float node_5367 = (i.uv0.g+(node_4872.g*_PanSpeed*0.5));
                float node_4410_if_leA = step(node_5367,0.0);
                float node_4410_if_leB = step(0.0,node_5367);
                float node_2588 = 1.0;
                float node_2120 = fmod(node_5367,lerp((node_4410_if_leA*(-1.0))+(node_4410_if_leB*node_2588),node_2588,node_4410_if_leA*node_4410_if_leB));
                float node_1718_if_leA = step(node_2120,0.0);
                float node_1718_if_leB = step(0.0,node_2120);
                float2 node_4530 = float2(0.0,lerp((node_1718_if_leA*(1.0 - abs(node_2120)))+(node_1718_if_leB*node_2120),node_2120,node_1718_if_leA*node_1718_if_leB));
                float4 node_4194 = tex2D(_MainTex,TRANSFORM_TEX(node_4530, _MainTex));
                float3 emissive = node_4194.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _WaveSpeed;
            uniform float _WaveIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float node_213 = 0.0;
                float4 node_174 = _Time + _TimeEditor;
                float node_8959 = sin(((o.uv0.r.r+(node_174.g*_WaveSpeed))*6.28318530718));
                v.vertex.xyz += float3(node_213,node_213,(node_8959*_WaveIntensity));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
