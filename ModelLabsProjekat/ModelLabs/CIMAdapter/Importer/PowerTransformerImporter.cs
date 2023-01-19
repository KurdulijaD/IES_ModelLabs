using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

			//// import all concrete model types (DMSType enum)
			ImportCurve();
			ImportCurveData();
			ImportOutageSchedule();
			ImportIrregularTimePoint();
			ImportSwitchingOperation();
			ImportGroundDisconnector();

			LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

		#region Import
		private void ImportCurve()
		{
			SortedDictionary<string, object> cimCurves = concreteModel.GetAllObjectsOfType("FTN.Curve");
			if (cimCurves != null)
			{
				foreach (KeyValuePair<string, object> cimCurvePair in cimCurves)
				{
					FTN.Curve cimCurve = cimCurvePair.Value as FTN.Curve;

					ResourceDescription rd = CreateCurveResourceDescription(cimCurve);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("Curve ID = ").Append(cimCurve.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("Curve ID = ").Append(cimCurve.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateCurveResourceDescription(FTN.Curve cimCurve)
		{
			ResourceDescription rd = null;
			if (cimCurve != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CURVE, importHelper.CheckOutIndexForDMSType(DMSType.CURVE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimCurve.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateCurveProperties(cimCurve, rd);
			}
			return rd;
		}
		
		private void ImportCurveData()
		{
			SortedDictionary<string, object> cimCurveDatas = concreteModel.GetAllObjectsOfType("FTN.CurveData");
			if (cimCurveDatas != null)
			{
				foreach (KeyValuePair<string, object> cimCurveDataPair in cimCurveDatas)
				{
					FTN.CurveData cimCurveData = cimCurveDataPair.Value as FTN.CurveData;

					ResourceDescription rd = CreateCurveDataResourceDescription(cimCurveData);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("CurveData ID = ").Append(cimCurveData.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("CurveData ID = ").Append(cimCurveData.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateCurveDataResourceDescription(FTN.CurveData cimCurveData)
		{
			ResourceDescription rd = null;
			if (cimCurveData != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CURVEDATA, importHelper.CheckOutIndexForDMSType(DMSType.CURVEDATA));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimCurveData.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateCurveDataProperties(cimCurveData, rd, importHelper, report);
			}
			return rd;
		}

		private void ImportIrregularTimePoint()
		{
			SortedDictionary<string, object> cimTimePoints = concreteModel.GetAllObjectsOfType("FTN.IrregularTimePoint");
			if (cimTimePoints != null)
			{
				foreach (KeyValuePair<string, object> cimTimePointPair in cimTimePoints)
				{
					FTN.IrregularTimePoint cimTimePoint = cimTimePointPair.Value as FTN.IrregularTimePoint;

					ResourceDescription rd = CreateIrregularTimePointDescription(cimTimePoint);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("IrregularTimePoints ID = ").Append(cimTimePoint.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("IrregularTimePoints ID = ").Append(cimTimePoint.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateIrregularTimePointDescription(FTN.IrregularTimePoint cimTimePoint)
		{
			ResourceDescription rd = null;
			if (cimTimePoint != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.IRRTIMEPOINT, importHelper.CheckOutIndexForDMSType(DMSType.IRRTIMEPOINT));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimTimePoint.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateIrregularTimePointProperties(cimTimePoint, rd, importHelper, report);
			}
			return rd;
		}

		private void ImportOutageSchedule()
		{
			SortedDictionary<string, object> cimOutageSchedules = concreteModel.GetAllObjectsOfType("FTN.OutageSchedule");
			if (cimOutageSchedules != null)
			{
				foreach (KeyValuePair<string, object> cimOutageSchedulePair in cimOutageSchedules)
				{
					FTN.OutageSchedule cimOutageSchedule = cimOutageSchedulePair.Value as FTN.OutageSchedule;

					ResourceDescription rd = CreateOutageScheduleResourceDescription(cimOutageSchedule);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("OutageSchedule ID = ").Append(cimOutageSchedule.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("OutageSchedule ID = ").Append(cimOutageSchedule.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateOutageScheduleResourceDescription(FTN.OutageSchedule cimOutageSchedule)
		{
			ResourceDescription rd = null;
			if (cimOutageSchedule != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.OUTAGESCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.OUTAGESCHEDULE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimOutageSchedule.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateOutageScheduleProperties(cimOutageSchedule, rd);
			}
			return rd;
		}

		private void ImportSwitchingOperation()
		{
			SortedDictionary<string, object> cimSWOperations = concreteModel.GetAllObjectsOfType("FTN.SwitchingOperation");
			if (cimSWOperations != null)
			{
				foreach (KeyValuePair<string, object> cimSWOperationPair in cimSWOperations)
				{
					FTN.SwitchingOperation cimSWOperation = cimSWOperationPair.Value as FTN.SwitchingOperation;

					ResourceDescription rd = CreateSwitchingOperationsResourceDescription(cimSWOperation);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("SwitchingOperations ID = ").Append(cimSWOperation.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("SwitchingOperations ID = ").Append(cimSWOperation.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateSwitchingOperationsResourceDescription(FTN.SwitchingOperation cimSWOperation)
		{
			ResourceDescription rd = null;
			if (cimSWOperation != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SWITCHINGOP, importHelper.CheckOutIndexForDMSType(DMSType.SWITCHINGOP));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimSWOperation.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateSwitchingOperationProperties(cimSWOperation, rd, importHelper, report);
			}
			return rd;
		}

		private void ImportGroundDisconnector()
		{
			SortedDictionary<string, object> cimGroundDiscs = concreteModel.GetAllObjectsOfType("FTN.GroundDisconnector");
			if (cimGroundDiscs != null)
			{
				foreach (KeyValuePair<string, object> cimGroundDiscPair in cimGroundDiscs)
				{
					FTN.GroundDisconnector cimGroundDisc = cimGroundDiscPair.Value as FTN.GroundDisconnector;

					ResourceDescription rd = CreateGroundDisconnectorResourceDescription(cimGroundDisc);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("GroundDisconector ID = ").Append(cimGroundDisc.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("GroundDisconector ID = ").Append(cimGroundDisc.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}
		private ResourceDescription CreateGroundDisconnectorResourceDescription(FTN.GroundDisconnector cimGroundDisc)
		{
			ResourceDescription rd = null;
			if (cimGroundDisc != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.GROUNDDISC, importHelper.CheckOutIndexForDMSType(DMSType.GROUNDDISC));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimGroundDisc.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateGroundDisconnectorProperties(cimGroundDisc, rd, importHelper, report);
			}
			return rd;
		}
		#endregion Import
	}
}

