#define DynamicRadialMasks_HEIGHTFIELD_4_ADVANCED_NORMALIZED_ID1_LOCAL_LOOP_COUNT 4


float4 DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_Position[DynamicRadialMasks_HEIGHTFIELD_4_ADVANCED_NORMALIZED_ID1_LOCAL_LOOP_COUNT];	
float  DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_Radius[DynamicRadialMasks_HEIGHTFIELD_4_ADVANCED_NORMALIZED_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_Intensity[DynamicRadialMasks_HEIGHTFIELD_4_ADVANCED_NORMALIZED_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_NoiseStrength[DynamicRadialMasks_HEIGHTFIELD_4_ADVANCED_NORMALIZED_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_EdgeSize[DynamicRadialMasks_HEIGHTFIELD_4_ADVANCED_NORMALIZED_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_Smooth[DynamicRadialMasks_HEIGHTFIELD_4_ADVANCED_NORMALIZED_ID1_LOCAL_LOOP_COUNT];


#include "../../Core/Core.cginc"



////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                                Main Method                                 //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
float DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local(float3 positionWS, float noise)
{
    float retValue = 1; 

	int i = 0;
	for(i = 0; i < DynamicRadialMasks_HEIGHTFIELD_4_ADVANCED_NORMALIZED_ID1_LOCAL_LOOP_COUNT; i++)
	{
		retValue *= 1 - ShaderExtensions_DynamicRadialMasks_HeightField_Advanced(positionWS,
																		noise,
																		DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_Position[i].xyz, 
																		DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_Radius[i],
																		DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_Intensity[i],
																		DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_NoiseStrength[i],	
																		DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_EdgeSize[i],	
																		DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_Smooth[i]);
	}		

    return 1 - retValue;
}

////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                               Helper Methods                               //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
void DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_float(float3 positionWS, float noise, out float retValue)
{
    retValue = DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local(positionWS, noise); 		
}

void DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_half(half3 positionWS, half noise, out half retValue)
{
    retValue = DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local(positionWS, noise); 		
}
