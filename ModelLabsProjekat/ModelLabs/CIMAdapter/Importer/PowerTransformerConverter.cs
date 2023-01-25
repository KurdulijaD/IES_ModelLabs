namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;
	using System;
	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
				}
			}
		}

		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPSR, ResourceDescription rd)
		{
			if ((cimPSR != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPSR, rd);
			}
		}

		public static void PopulateBasicIntervalScheduleProperties(FTN.BasicIntervalSchedule cimBIS, ResourceDescription rd)
		{
			if ((cimBIS != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBIS, rd);

				if (cimBIS.StartTimeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BIS_STARTTIME, cimBIS.StartTime));
				}
				if (cimBIS.Value1UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BIS_VALUE1UNIT, (short)GetDMSUnitSymbol(cimBIS.Value1Unit)));
				}
				if (cimBIS.Value2UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BIS_VALUE2UNIT, (short)GetDMSUnitSymbol(cimBIS.Value2Unit)));
				}
				if (cimBIS.Value1MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BIS_VALUE1MULTIPLIER, (short)GetDMSUnitMultiplier(cimBIS.Value1Multiplier)));
				}
				if (cimBIS.Value2MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BIS_VALUE2MULTIPLIER, (short)GetDMSUnitMultiplier(cimBIS.Value2Multiplier)));
				}
			}
		}

		public static void PopulateIrregularTimePointProperties(FTN.IrregularTimePoint irregularTimePoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((irregularTimePoint != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(irregularTimePoint, rd);

				if (irregularTimePoint.TimeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IRRTIMEPOINT_TIME, irregularTimePoint.Time));
				}
				if (irregularTimePoint.Value1HasValue)
				{
					rd.AddProperty(new Property(ModelCode.IRRTIMEPOINT_VALUE1, irregularTimePoint.Value1));
				}
				if (irregularTimePoint.Value2HasValue)
				{
					rd.AddProperty(new Property(ModelCode.IRRTIMEPOINT_VALUE2, irregularTimePoint.Value2));
				}
				if (irregularTimePoint.IntervalScheduleHasValue)
				{
					long gid = importHelper.GetMappedGID(irregularTimePoint.IntervalSchedule.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(irregularTimePoint.GetType().ToString()).Append(" rdfID = \"").Append(irregularTimePoint.ID);
						report.Report.Append("\" - Failed to set reference to IntervalScedule: rdfID \"").Append(irregularTimePoint.IntervalSchedule.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.IRRTIMEPOINT_INTERVALSCHEDULE, gid));
				}
			}
		}

		public static void PopulateSwitchingOperationProperties(FTN.SwitchingOperation cimSwitchingOperation, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimSwitchingOperation != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimSwitchingOperation, rd);

				if (cimSwitchingOperation.NewStateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCHINGOP_NEWSTATE, (short)GetDMSSwitchState(cimSwitchingOperation.NewState)));
				}
				if (cimSwitchingOperation.OperationTimeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCHINGOP_OPERATIONTIME, cimSwitchingOperation.OperationTime));
				}
				if (cimSwitchingOperation.OutageScheduleHasValue)
				{
					long gid = importHelper.GetMappedGID(cimSwitchingOperation.OutageSchedule.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimSwitchingOperation.GetType().ToString()).Append(" rdfID = \"").Append(cimSwitchingOperation.ID);
						report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimSwitchingOperation.OutageSchedule.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.SWITCHINGOP_OUTAGESCHEDULE, gid));
				}
			}
		}

		public static void PopulateCurveProperties(FTN.Curve cimCurve, ResourceDescription rd)
		{
			if ((cimCurve != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimCurve, rd);

				if (cimCurve.CurveStyleHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_CURVESTYLE, (short)GetDMSCurveStyle(cimCurve.CurveStyle)));
				}
				if (cimCurve.XUnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_XUNIT, (short)GetDMSUnitSymbol(cimCurve.XUnit)));
				}
				if (cimCurve.Y1UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_Y1UNIT, (short)GetDMSUnitSymbol(cimCurve.Y1Unit)));
				}
				if (cimCurve.Y2UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_Y2UNIT, (short)GetDMSUnitSymbol(cimCurve.Y2Unit)));
				}
				if (cimCurve.Y3UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_Y3UNIT, (short)GetDMSUnitSymbol(cimCurve.Y3Unit)));
				}
				if (cimCurve.XMultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_XMULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.XMultiplier)));
				}
				if (cimCurve.Y1MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_Y1MULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.Y1Multiplier)));
				}
				if (cimCurve.Y2MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_Y2MULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.Y2Multiplier)));
				}
				if (cimCurve.Y3MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_Y3MULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.Y3Multiplier)));
				}
			}
		}

		public static void PopulateCurveDataProperties(FTN.CurveData cimCurveData, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimCurveData != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimCurveData, rd);

				if (cimCurveData.XvalueHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVEDATA_XVALUE, cimCurveData.Xvalue));
				}
				if (cimCurveData.Y1valueHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVEDATA_Y1VALUE, cimCurveData.Y1value));
				}
				if (cimCurveData.Y2valueHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVEDATA_Y2VALUE, cimCurveData.Y2value));
				}
				if (cimCurveData.Y3valueHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVEDATA_Y3VALUE, cimCurveData.Y3value));
				}
				if (cimCurveData.CurveHasValue)
				{
					long gid = importHelper.GetMappedGID(cimCurveData.Curve.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimCurveData.GetType().ToString()).Append(" rdfID = \"").Append(cimCurveData.ID);
						report.Report.Append("\" - Failed to set reference to Curve: rdfID \"").Append(cimCurveData.Curve.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.CURVEDATA_CURVE, gid));
				}
			}
		}

		public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd)
		{
			if ((cimEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd);

				if (cimEquipment.NormallyInServiceHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORMALLYINSERVICE, cimEquipment.NormallyInService));
				}
				if (cimEquipment.AggregateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGREGATE, cimEquipment.Aggregate));
				}
			}
		}

		public static void PopulateIrregularIntervalScheduleProperties(FTN.IrregularIntervalSchedule cimIntervalSchedule, ResourceDescription rd)
		{
			if ((cimIntervalSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateBasicIntervalScheduleProperties(cimIntervalSchedule, rd);
			}
		}
		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd);
			}
		}

		public static void PopulateOutageScheduleProperties(FTN.OutageSchedule cimOutageSchedule, ResourceDescription rd)
		{
			if ((cimOutageSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIrregularIntervalScheduleProperties(cimOutageSchedule, rd);
			}
		}

		public static void PopulateSwitchProperties(FTN.Switch cimSwitch, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimSwitch != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateConductingEquipmentProperties(cimSwitch, rd);

				if (cimSwitch.SwitchingOperationsHasValue)
				{
					long gid = importHelper.GetMappedGID(cimSwitch.SwitchingOperations.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimSwitch.GetType().ToString()).Append(" rdfID = \"").Append(cimSwitch.ID);
						report.Report.Append("\" - Failed to set reference to Switching Operation: rdfID \"").Append(cimSwitch.SwitchingOperations.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHINGOP, gid));
				}
				if (cimSwitch.RatedCurrentHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_RATEDCURRENT, cimSwitch.RatedCurrent));
				}
				if (cimSwitch.RetainedHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_RETAINED, cimSwitch.Retained));
				}
				if (cimSwitch.NormalOpenHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_NORMALOPEN, cimSwitch.NormalOpen));
				}
				if (cimSwitch.SwitchOnCountHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHONCOUNT, cimSwitch.SwitchOnCount));
				}
				if (cimSwitch.SwitchOnDateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHONDATE, cimSwitch.SwitchOnDate));
				}
			}
			
		}

		public static void PopulateGroundDisconnectorProperties(FTN.GroundDisconnector cimGroundDisc, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimGroundDisc != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateSwitchProperties(cimGroundDisc, rd, importHelper, report);
			}
		}

		#endregion Populate ResourceDescription

		#region Enums convert
		public static UnitMultiplier GetDMSUnitMultiplier(FTN.UnitMultiplier unitMultiplier)
		{
			switch (unitMultiplier)
			{
				case FTN.UnitMultiplier.c:
					return UnitMultiplier.c;
				case FTN.UnitMultiplier.d:
					return UnitMultiplier.d;
				case FTN.UnitMultiplier.G:
					return UnitMultiplier.G;
				case FTN.UnitMultiplier.k:
					return UnitMultiplier.k;
				case FTN.UnitMultiplier.m:
					return UnitMultiplier.m;
				case FTN.UnitMultiplier.M:
					return UnitMultiplier.M;
				case FTN.UnitMultiplier.micro:
					return UnitMultiplier.micro;
				case FTN.UnitMultiplier.n:
					return UnitMultiplier.n;
				case FTN.UnitMultiplier.p:
					return UnitMultiplier.p;
				case FTN.UnitMultiplier.T:
					return UnitMultiplier.T;
				default: 
					return UnitMultiplier.none;
			}
		}

		public static UnitSymbol GetDMSUnitSymbol(FTN.UnitSymbol unitSymbol)
		{
			switch (unitSymbol)
			{
				case FTN.UnitSymbol.A:
					return UnitSymbol.A;
				case FTN.UnitSymbol.F:
					return UnitSymbol.F;
				case FTN.UnitSymbol.H:
					return UnitSymbol.H;
				case FTN.UnitSymbol.Hz:
					return UnitSymbol.Hz;
				case FTN.UnitSymbol.J:
					return UnitSymbol.J;
				case FTN.UnitSymbol.N:
					return UnitSymbol.N;
				case FTN.UnitSymbol.Pa:
					return UnitSymbol.Pa;
				case FTN.UnitSymbol.S:
					return UnitSymbol.S;
				case FTN.UnitSymbol.V:
					return UnitSymbol.V;
				case FTN.UnitSymbol.VA:
					return UnitSymbol.VA;
				case FTN.UnitSymbol.VAh:
					return UnitSymbol.VAh;
				case FTN.UnitSymbol.VAr:
					return UnitSymbol.VAr;
				case FTN.UnitSymbol.VArh:
					return UnitSymbol.VArh;
				case FTN.UnitSymbol.W:
					return UnitSymbol.W;
				case FTN.UnitSymbol.Wh:
					return UnitSymbol.Wh;
				case FTN.UnitSymbol.deg:
					return UnitSymbol.deg;
				case FTN.UnitSymbol.degC:
					return UnitSymbol.degC;
				case FTN.UnitSymbol.g:
					return UnitSymbol.g;
				case FTN.UnitSymbol.h:
					return UnitSymbol.h;
				case FTN.UnitSymbol.m:
					return UnitSymbol.m;
				case FTN.UnitSymbol.m2:
					return UnitSymbol.m2;
				case FTN.UnitSymbol.m3:
					return UnitSymbol.m3;
				case FTN.UnitSymbol.min:
					return UnitSymbol.min;
				case FTN.UnitSymbol.ohm:
					return UnitSymbol.ohm;
				case FTN.UnitSymbol.rad:
					return UnitSymbol.rad;
				case FTN.UnitSymbol.s:
					return UnitSymbol.s;
				default:
					return UnitSymbol.none;
			}
		}

		public static SwitchState GetDMSSwitchState(FTN.SwitchState switchState)
		{
			switch (switchState)
			{
				case FTN.SwitchState.open:
					return SwitchState.open;
				case FTN.SwitchState.close:
					return SwitchState.close;
				default:
					return SwitchState.open;
			}
		}

		public static CurveStyle GetDMSCurveStyle(FTN.CurveStyle curveStyle)
		{
			switch (curveStyle)
			{
				case FTN.CurveStyle.constantYValue:
					return CurveStyle.constantYValue;
				case FTN.CurveStyle.formula:
					return CurveStyle.formula;
				case FTN.CurveStyle.rampYValue:
					return CurveStyle.rampYValue;
				case FTN.CurveStyle.straightLineYValues:
					return CurveStyle.straightLineYValues;
				default:
					return CurveStyle.constantYValue;
			}
		}
		#endregion Enums convert
	}
}
