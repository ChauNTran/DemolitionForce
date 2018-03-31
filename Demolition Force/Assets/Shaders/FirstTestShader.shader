// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.35 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.35;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:4,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32952,y:32712,varname:node_4795,prsc:2|emission-553-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:31928,y:32645,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:25abccdcbf17f4f4ba190049f99aa9f0,ntxv:0,isnm:False|UVIN-5675-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32127,y:32756,varname:node_2393,prsc:2|A-6074-RGB,B-1531-RGB;n:type:ShaderForge.SFN_Tex2d,id:1531,x:31928,y:32847,ptovrint:False,ptlb:node_1531,ptin:_node_1531,varname:node_1531,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:518f78b7ba8a46a44971da45a42d9101,ntxv:0,isnm:False|UVIN-4381-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:9627,x:31928,y:33066,ptovrint:False,ptlb:node_9627,ptin:_node_9627,varname:node_9627,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bacd252f3901bab4283ccfad5da58361,ntxv:0,isnm:False|UVIN-6860-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4955,x:32341,y:32853,varname:node_4955,prsc:2|A-2393-OUT,B-9627-RGB;n:type:ShaderForge.SFN_Multiply,id:553,x:32560,y:32794,varname:node_553,prsc:2|A-2677-OUT,B-4955-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2677,x:32359,y:32756,ptovrint:False,ptlb:node_2677,ptin:_node_2677,varname:node_2677,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Panner,id:5675,x:31712,y:32628,varname:node_5675,prsc:2,spu:0,spv:1|UVIN-6287-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6287,x:31487,y:32628,varname:node_6287,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:4381,x:31726,y:32847,varname:node_4381,prsc:2,spu:0,spv:0.5|UVIN-7075-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7075,x:31501,y:32847,varname:node_7075,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:6860,x:31726,y:33066,varname:node_6860,prsc:2,spu:0,spv:0.2|UVIN-4587-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:4587,x:31501,y:33066,varname:node_4587,prsc:2,uv:0,uaff:False;proporder:6074-1531-9627-2677;pass:END;sub:END;*/

Shader "Shader Forge/FirstTestShader" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _node_1531 ("node_1531", 2D) = "white" {}
        _node_9627 ("node_9627", 2D) = "white" {}
        _node_2677 ("node_2677", Float ) = 4
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
            Blend DstColor Zero
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _node_1531; uniform float4 _node_1531_ST;
            uniform sampler2D _node_9627; uniform float4 _node_9627_ST;
            uniform float _node_2677;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_6279 = _Time + _TimeEditor;
                float2 node_5675 = (i.uv0+node_6279.g*float2(0,1));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_5675, _MainTex));
                float2 node_4381 = (i.uv0+node_6279.g*float2(0,0.5));
                float4 _node_1531_var = tex2D(_node_1531,TRANSFORM_TEX(node_4381, _node_1531));
                float2 node_6860 = (i.uv0+node_6279.g*float2(0,0.2));
                float4 _node_9627_var = tex2D(_node_9627,TRANSFORM_TEX(node_6860, _node_9627));
                float3 emissive = (_node_2677*((_MainTex_var.rgb*_node_1531_var.rgb)*_node_9627_var.rgb));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.5,0.5,0.5,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
