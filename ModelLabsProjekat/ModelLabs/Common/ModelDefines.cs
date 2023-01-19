using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{
	
	public enum DMSType : short
	{		
		MASK_TYPE							= unchecked((short)0xFFFF),

		CURVE										= 0x0001,
		CURVEDATA									= 0x0002,
		OUTAGESCHEDULE								= 0x0003,
		GROUNDDISC									= 0x0004,
		IRRTIMEPOINT								= 0x0005,
		SWITCHINGOP									= 0x0006,
	}

    [Flags]
	public enum ModelCode : long
	{
		IDOBJ								= 0x1000000000000000,
		IDOBJ_GID							= 0x1000000000000104,
		IDOBJ_ALIASNAME						= 0x1000000000000207,
		IDOBJ_MRID							= 0x1000000000000307,
		IDOBJ_NAME							= 0x1000000000000407,	

		PSR									= 0x1100000000000000,

		BIS									= 0x1200000000000000,
		BIS_STARTTIME						= 0x1200000000000108,
		BIS_VALUE1MULTIPLIER				= 0x120000000000020a,
		BIS_VALUE1UNIT						= 0x120000000000030a,
		BIS_VALUE2MULTIPLIER				= 0x120000000000040a,
		BIS_VALUE2UNIT						= 0x120000000000050a,

		CURVE								= 0x1300000000010000,
		CURVE_CURVESTYLE					= 0x130000000001010a,
		CURVE_XMULTIPLIER					= 0x130000000001020a,
		CURVE_XUNIT							= 0x130000000001030a,
		CURVE_Y1MULTIPLIER					= 0x130000000001040a,
		CURVE_Y1UNIT						= 0x130000000001050a,
		CURVE_Y2MULTIPLIER					= 0x130000000001060a,
		CURVE_Y2UNIT						= 0x130000000001070a,
		CURVE_Y3MULTIPLIER					= 0x130000000001080a,
		CURVE_Y3UNIT						= 0x130000000001090a,
		CURVE_CURVEDATAS					= 0x1300000000011019,

		CURVEDATA							= 0x1400000000020000,
		CURVEDATA_XVALUE					= 0x1400000000020105,
		CURVEDATA_Y1VALUE					= 0x1400000000020205,
		CURVEDATA_Y2VALUE					= 0x1400000000020305,
		CURVEDATA_Y3VALUE					= 0x1400000000020405,
		CURVEDATA_CURVE						= 0x1400000000020509,


		IRRTIMEPOINT						= 0x1500000000050000,
		IRRTIMEPOINT_TIME					= 0x1500000000050109,
		IRRTIMEPOINT_VALUE1					= 0x1500000000050205,
		IRRTIMEPOINT_VALUE2					= 0x1500000000050305,
		IRRTIMEPOINT_INTERVALSCHEDULE		= 0x1500000000050409,

		SWITCHINGOP							= 0x1600000000060000,
		SWITCHINGOP_NEWSTATE				= 0x160000000006010a,
		SWITCHINGOP_OPERATIONTIME			= 0x1600000000060208,
		SWITCHINGOP_OUTAGESCHEDULE			= 0x1600000000060309,
		SWITCHINGOP_SWITCHES				= 0x1600000000060419,

		EQUIPMENT							= 0x1110000000000000,

		INTERVALSCHEDULE					= 0x1210000000000000,
		INTERVALSCHEDULE_IRRTIMEPOINTS		= 0x1210000000000119,

		CONDEQ								= 0x1111000000000000,

		OUTAGESCHEDULE						= 0x1211000000030000,
		OUTAGESCHEDULE_SWITCHINGOPS			= 0x1211000000030119,

		SWITCH								= 0x1111100000000000,
		SWITCH_SWITCHINGOP					= 0x1111100000000109,

		GROUNDDISC							= 0x1111110000040000,
	}

    [Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			 = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	 = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		  = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	  = unchecked((long)0xfffffff000000000),		
	}																		
}


