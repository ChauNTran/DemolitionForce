// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.35 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.35;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-3031-OUT,alpha-8508-OUT;n:type:ShaderForge.SFN_Tex2d,id:6599,x:31512,y:33178,ptovrint:False,ptlb:node_6599,ptin:_node_6599,varname:node_6599,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:62dfff5ec08304b4ea4a595729267660,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9846,x:31558,y:32269,ptovrint:False,ptlb:node_9846,ptin:_node_9846,varname:node_9846,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1e1ed2e30c5dea5449ef0d056016bd17,ntxv:0,isnm:False|UVIN-4897-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:8626,x:31558,y:32538,ptovrint:False,ptlb:node_8626,ptin:_node_8626,varname:node_8626,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:72ce053bcd6f8d9469a2c416e9febc54,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2628,x:31512,y:33369,ptovrint:False,ptlb:node_6599_copy,ptin:_node_6599_copy,varname:_node_6599_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:62dfff5ec08304b4ea4a595729267660,ntxv:0,isnm:False|UVIN-52-UVOUT;n:type:ShaderForge.SFN_Panner,id:52,x:31317,y:33369,varname:node_52,prsc:2,spu:0.013,spv:-0.1456|UVIN-3638-OUT;n:type:ShaderForge.SFN_TexCoord,id:8942,x:30646,y:33368,varname:node_8942,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:6889,x:31512,y:33610,ptovrint:False,ptlb:node_6599_copy_copy,ptin:_node_6599_copy_copy,varname:_node_6599_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:62dfff5ec08304b4ea4a595729267660,ntxv:0,isnm:False|UVIN-3344-UVOUT;n:type:ShaderForge.SFN_Panner,id:3344,x:31317,y:33561,varname:node_3344,prsc:2,spu:-0.213,spv:-0.321|UVIN-3976-OUT;n:type:ShaderForge.SFN_TexCoord,id:2509,x:30898,y:33559,varname:node_2509,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:1608,x:32147,y:33173,varname:node_1608,prsc:2|A-6599-R,B-7561-OUT,C-8529-OUT;n:type:ShaderForge.SFN_Multiply,id:7561,x:31695,y:33496,varname:node_7561,prsc:2|A-2628-G,B-6889-B,C-1861-OUT;n:type:ShaderForge.SFN_Vector1,id:1861,x:31512,y:33530,varname:node_1861,prsc:2,v1:2;n:type:ShaderForge.SFN_Add,id:4406,x:31885,y:33381,varname:node_4406,prsc:2|A-2628-G,B-7561-OUT;n:type:ShaderForge.SFN_Multiply,id:662,x:32011,y:32427,varname:node_662,prsc:2|A-27-OUT,B-8626-RGB,C-1027-OUT,D-5300-RGB;n:type:ShaderForge.SFN_Panner,id:4897,x:31360,y:32269,varname:node_4897,prsc:2,spu:0.213,spv:-0.456|UVIN-234-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:234,x:31151,y:32269,varname:node_234,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector1,id:1027,x:31558,y:32448,varname:node_1027,prsc:2,v1:2;n:type:ShaderForge.SFN_Add,id:4783,x:31949,y:33237,varname:node_4783,prsc:2|A-4406-OUT,B-6889-B;n:type:ShaderForge.SFN_Multiply,id:8508,x:32469,y:33028,varname:node_8508,prsc:2|A-6139-A,B-1608-OUT;n:type:ShaderForge.SFN_UVTile,id:2380,x:30854,y:33368,varname:node_2380,prsc:2|UVIN-8942-UVOUT,WDT-8699-OUT,HGT-8699-OUT,TILE-8699-OUT;n:type:ShaderForge.SFN_Vector1,id:8699,x:30646,y:33527,varname:node_8699,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:3638,x:31096,y:33368,varname:node_3638,prsc:2|A-6139-R,B-2380-UVOUT;n:type:ShaderForge.SFN_VertexColor,id:6139,x:30467,y:32973,varname:node_6139,prsc:2;n:type:ShaderForge.SFN_Add,id:3976,x:31121,y:33559,varname:node_3976,prsc:2|A-6139-R,B-2509-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:5300,x:31558,y:32082,ptovrint:False,ptlb:node_9846_copy,ptin:_node_9846_copy,varname:_node_9846_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1e1ed2e30c5dea5449ef0d056016bd17,ntxv:0,isnm:False|UVIN-8826-UVOUT;n:type:ShaderForge.SFN_Panner,id:8826,x:31360,y:32082,varname:node_8826,prsc:2,spu:-0.213,spv:-0.3456|UVIN-2904-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2904,x:31151,y:32082,varname:node_2904,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector1,id:8529,x:31932,y:33127,varname:node_8529,prsc:2,v1:2;n:type:ShaderForge.SFN_Add,id:27,x:31818,y:32392,varname:node_27,prsc:2|A-9846-RGB,B-8626-RGB;n:type:ShaderForge.SFN_Color,id:3260,x:32449,y:32418,ptovrint:False,ptlb:node_3260,ptin:_node_3260,varname:node_3260,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:3031,x:32500,y:32704,varname:node_3031,prsc:2|A-3260-RGB,B-662-OUT;proporder:8626-6599-2628-6889-9846-5300-3260;pass:END;sub:END;*/

Shader "Shader Forge/Flame" {
    Properties {
        _node_8626 ("node_8626", 2D) = "white" {}
        _node_6599 ("node_6599", 2D) = "white" {}
        _node_6599_copy ("node_6599_copy", 2D) = "white" {}
        _node_6599_copy_copy ("node_6599_copy_copy", 2D) = "white" {}
        _node_9846 ("node_9846", 2D) = "white" {}
        _node_9846_copy ("node_9846_copy", 2D) = "white" {}
        [HDR]_node_3260 ("node_3260", Color) = (0.5,0.5,0.5,1)
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
            uniform sampler2D _node_6599; uniform float4 _node_6599_ST;
            uniform sampler2D _node_9846; uniform float4 _node_9846_ST;
            uniform sampler2D _node_8626; uniform float4 _node_8626_ST;
            uniform sampler2D _node_6599_copy; uniform float4 _node_6599_copy_ST;
            uniform sampler2D _node_6599_copy_copy; uniform float4 _node_6599_copy_copy_ST;
            uniform sampler2D _node_9846_copy; uniform float4 _node_9846_copy_ST;
            uniform float4 _node_3260;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_4715 = _Time + _TimeEditor;
                float2 node_4897 = (i.uv0+node_4715.g*float2(0.213,-0.456));
                float4 _node_9846_var = tex2D(_node_9846,TRANSFORM_TEX(node_4897, _node_9846));
                float4 _node_8626_var = tex2D(_node_8626,TRANSFORM_TEX(i.uv0, _node_8626));
                float2 node_8826 = (i.uv0+node_4715.g*float2(-0.213,-0.3456));
                float4 _node_9846_copy_var = tex2D(_node_9846_copy,TRANSFORM_TEX(node_8826, _node_9846_copy));
                float3 emissive = (_node_3260.rgb*((_node_9846_var.rgb+_node_8626_var.rgb)*_node_8626_var.rgb*2.0*_node_9846_copy_var.rgb));
                float3 finalColor = emissive;
                float4 _node_6599_var = tex2D(_node_6599,TRANSFORM_TEX(i.uv0, _node_6599));
                float node_8699 = 1.0;
                float2 node_2380_tc_rcp = float2(1.0,1.0)/float2( node_8699, node_8699 );
                float node_2380_ty = floor(node_8699 * node_2380_tc_rcp.x);
                float node_2380_tx = node_8699 - node_8699 * node_2380_ty;
                float2 node_2380 = (i.uv0 + float2(node_2380_tx, node_2380_ty)) * node_2380_tc_rcp;
                float2 node_52 = ((i.vertexColor.r+node_2380)+node_4715.g*float2(0.013,-0.1456));
                float4 _node_6599_copy_var = tex2D(_node_6599_copy,TRANSFORM_TEX(node_52, _node_6599_copy));
                float2 node_3344 = ((i.vertexColor.r+i.uv0)+node_4715.g*float2(-0.213,-0.321));
                float4 _node_6599_copy_copy_var = tex2D(_node_6599_copy_copy,TRANSFORM_TEX(node_3344, _node_6599_copy_copy));
                float node_7561 = (_node_6599_copy_var.g*_node_6599_copy_copy_var.b*2.0);
                return fixed4(finalColor,(i.vertexColor.a*(_node_6599_var.r*node_7561*2.0)));
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
