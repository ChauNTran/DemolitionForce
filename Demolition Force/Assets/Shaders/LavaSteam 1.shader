// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.35 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.35;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33153,y:32801,varname:node_3138,prsc:2|emission-2125-OUT,alpha-2414-OUT;n:type:ShaderForge.SFN_Tex2d,id:9303,x:32065,y:32815,ptovrint:False,ptlb:node_9303,ptin:_node_9303,varname:node_9303,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7c8dae1644b84164ba4c4723d4091bdb,ntxv:0,isnm:False|UVIN-2741-UVOUT;n:type:ShaderForge.SFN_Panner,id:2741,x:31895,y:32815,varname:node_2741,prsc:2,spu:0.0023,spv:-0.0161|UVIN-6896-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6896,x:31709,y:32815,varname:node_6896,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:53,x:32065,y:33010,ptovrint:False,ptlb:node_9303_copy,ptin:_node_9303_copy,varname:_node_9303_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7c8dae1644b84164ba4c4723d4091bdb,ntxv:0,isnm:False|UVIN-4775-UVOUT;n:type:ShaderForge.SFN_Panner,id:4775,x:31895,y:33010,varname:node_4775,prsc:2,spu:-0.008,spv:-0.0896|UVIN-6010-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6010,x:31709,y:33010,varname:node_6010,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:1698,x:32065,y:33207,ptovrint:False,ptlb:node_9303_copy_copy,ptin:_node_9303_copy_copy,varname:_node_9303_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7c8dae1644b84164ba4c4723d4091bdb,ntxv:0,isnm:False|UVIN-2351-UVOUT;n:type:ShaderForge.SFN_Panner,id:2351,x:31895,y:33207,varname:node_2351,prsc:2,spu:0,spv:0|UVIN-3884-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:3884,x:31709,y:33207,varname:node_3884,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:6216,x:32261,y:32925,varname:node_6216,prsc:2|A-9303-R,B-53-G;n:type:ShaderForge.SFN_Multiply,id:5134,x:32457,y:33011,varname:node_5134,prsc:2|A-6216-OUT,B-1698-B,C-1464-OUT;n:type:ShaderForge.SFN_Vector1,id:1464,x:32303,y:33090,varname:node_1464,prsc:2,v1:2;n:type:ShaderForge.SFN_Color,id:7048,x:32442,y:32815,ptovrint:False,ptlb:node_7048,ptin:_node_7048,varname:node_7048,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2279412,c2:0.2279412,c3:0.2279412,c4:1;n:type:ShaderForge.SFN_Add,id:2125,x:32659,y:32926,varname:node_2125,prsc:2|A-7048-RGB,B-5134-OUT;n:type:ShaderForge.SFN_OneMinus,id:7730,x:32689,y:33091,varname:node_7730,prsc:2|IN-5134-OUT;n:type:ShaderForge.SFN_Tex2d,id:665,x:32065,y:33417,ptovrint:False,ptlb:node_9303_copy_copy_copy,ptin:_node_9303_copy_copy_copy,varname:_node_9303_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1a40845590ceb8c479fb5b44abc21a96,ntxv:0,isnm:False|UVIN-8596-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2414,x:32861,y:33218,varname:node_2414,prsc:2|A-7730-OUT,B-665-R,C-6216-OUT,D-1125-OUT;n:type:ShaderForge.SFN_TexCoord,id:8596,x:31876,y:33417,varname:node_8596,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector1,id:1125,x:32654,y:33367,varname:node_1125,prsc:2,v1:2;proporder:9303-53-1698-7048-665;pass:END;sub:END;*/

Shader "Shader Forge/LavaSteam1" {
    Properties {
        _node_9303 ("node_9303", 2D) = "white" {}
        _node_9303_copy ("node_9303_copy", 2D) = "white" {}
        _node_9303_copy_copy ("node_9303_copy_copy", 2D) = "white" {}
        _node_7048 ("node_7048", Color) = (0.2279412,0.2279412,0.2279412,1)
        _node_9303_copy_copy_copy ("node_9303_copy_copy_copy", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_9303; uniform float4 _node_9303_ST;
            uniform sampler2D _node_9303_copy; uniform float4 _node_9303_copy_ST;
            uniform sampler2D _node_9303_copy_copy; uniform float4 _node_9303_copy_copy_ST;
            uniform float4 _node_7048;
            uniform sampler2D _node_9303_copy_copy_copy; uniform float4 _node_9303_copy_copy_copy_ST;
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
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_7145 = _Time + _TimeEditor;
                float2 node_2741 = (i.uv0+node_7145.g*float2(0.0023,-0.0161));
                float4 _node_9303_var = tex2D(_node_9303,TRANSFORM_TEX(node_2741, _node_9303));
                float2 node_4775 = (i.uv0+node_7145.g*float2(-0.008,-0.0896));
                float4 _node_9303_copy_var = tex2D(_node_9303_copy,TRANSFORM_TEX(node_4775, _node_9303_copy));
                float node_6216 = (_node_9303_var.r*_node_9303_copy_var.g);
                float2 node_2351 = (i.uv0+node_7145.g*float2(0,0));
                float4 _node_9303_copy_copy_var = tex2D(_node_9303_copy_copy,TRANSFORM_TEX(node_2351, _node_9303_copy_copy));
                float node_5134 = (node_6216*_node_9303_copy_copy_var.b*2.0);
                float3 emissive = (_node_7048.rgb+node_5134);
                float3 finalColor = emissive;
                float4 _node_9303_copy_copy_copy_var = tex2D(_node_9303_copy_copy_copy,TRANSFORM_TEX(i.uv0, _node_9303_copy_copy_copy));
                return fixed4(finalColor,((1.0 - node_5134)*_node_9303_copy_copy_copy_var.r*node_6216*2.0));
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
