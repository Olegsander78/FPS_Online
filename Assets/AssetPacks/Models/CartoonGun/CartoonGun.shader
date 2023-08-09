// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|custl-6135-OUT,olwid-4543-OUT,olcol-2106-RGB;n:type:ShaderForge.SFN_Tex2d,id:4781,x:32740,y:32765,ptovrint:False,ptlb:Textures,ptin:_Textures,varname:node_4781,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:33b9992adc276b640a63b728ded81b44,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Fresnel,id:9324,x:32356,y:32814,varname:node_9324,prsc:2|NRM-7156-OUT;n:type:ShaderForge.SFN_Add,id:6135,x:32985,y:32853,varname:node_6135,prsc:2|A-4781-RGB,B-3561-OUT;n:type:ShaderForge.SFN_NormalVector,id:7156,x:32125,y:32725,prsc:2,pt:False;n:type:ShaderForge.SFN_ValueProperty,id:4998,x:32149,y:33051,ptovrint:False,ptlb:Fresnel Strength,ptin:_FresnelStrength,varname:node_4998,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Color,id:6473,x:32529,y:33072,ptovrint:False,ptlb:Fresnel Color,ptin:_FresnelColor,varname:node_6473,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:0.509;n:type:ShaderForge.SFN_Power,id:6948,x:32550,y:32852,varname:node_6948,prsc:2|VAL-9324-OUT,EXP-4166-OUT;n:type:ShaderForge.SFN_Exp,id:4166,x:32356,y:33051,varname:node_4166,prsc:2,et:0|IN-4998-OUT;n:type:ShaderForge.SFN_Multiply,id:3561,x:32740,y:32969,varname:node_3561,prsc:2|A-6948-OUT,B-6473-RGB,C-6473-A;n:type:ShaderForge.SFN_Color,id:2106,x:32748,y:33261,ptovrint:False,ptlb:Outline Color,ptin:_OutlineColor,varname:node_2106,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:380,x:32826,y:33137,ptovrint:False,ptlb:Outline Width,ptin:_OutlineWidth,varname:node_380,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Vector1,id:3125,x:32932,y:33408,varname:node_3125,prsc:2,v1:200;n:type:ShaderForge.SFN_Divide,id:4543,x:33082,y:33244,varname:node_4543,prsc:2|A-380-OUT,B-3125-OUT;n:type:ShaderForge.SFN_Color,id:6938,x:32662,y:34001,ptovrint:False,ptlb:node_8307_copy_copy_copy_copy_copy,ptin:_node_8307_copy_copy_copy_copy_copy,varname:_node_8307_copy_copy_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.462069,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:8271,x:32726,y:34065,ptovrint:False,ptlb:node_8307_copy_copy_copy_copy_copy_copy,ptin:_node_8307_copy_copy_copy_copy_copy_copy,varname:_node_8307_copy_copy_copy_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.462069,c3:1,c4:1;proporder:4781-6473-4998-2106-380;pass:END;sub:END;*/

Shader "Custom/CartoonGun" {
    Properties {
        _Textures ("Textures", 2D) = "white" {}
        _FresnelColor ("Fresnel Color", Color) = (1,1,1,0.509)
        _FresnelStrength ("Fresnel Strength", Float ) = 2
        _OutlineColor ("Outline Color", Color) = (0.5,0.5,0.5,1)
        _OutlineWidth ("Outline Width", Float ) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _OutlineColor;
            uniform float _OutlineWidth;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_FOG_COORDS(0)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos(float4(v.vertex.xyz + v.normal*(_OutlineWidth/200.0),1) );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                return fixed4(_OutlineColor.rgb,0);
            }
            ENDCG
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
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Textures; uniform float4 _Textures_ST;
            uniform float _FresnelStrength;
            uniform float4 _FresnelColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
                float4 _Textures_var = tex2D(_Textures,TRANSFORM_TEX(i.uv0, _Textures));
                float3 finalColor = (_Textures_var.rgb+(pow((1.0-max(0,dot(i.normalDir, viewDirection))),exp(_FresnelStrength))*_FresnelColor.rgb*_FresnelColor.a));
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
