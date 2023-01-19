using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class IrregularIntervalSchedule : BasicIntervalSchedule
    {
        private List<long> irregularTimePoints = new List<long>();
        public IrregularIntervalSchedule(long globalId) : base(globalId)
        {

        }

        public List<long> IrregularTimePoints { get => irregularTimePoints; set => irregularTimePoints = value; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                IrregularIntervalSchedule i = (IrregularIntervalSchedule)x;
                return (CompareHelper.CompareLists(i.irregularTimePoints, this.irregularTimePoints));
            }
            else
                return false;
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.INTERVALSCHEDULE_IRRTIMEPOINTS:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.INTERVALSCHEDULE_IRRTIMEPOINTS:
                    property.SetValue(irregularTimePoints);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }
        public override void SetProperty(Property property)
        {
            base.SetProperty(property);
        }

        public override bool IsReferenced
        {
            get
            {
                return irregularTimePoints.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if(irregularTimePoints != null && irregularTimePoints.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.INTERVALSCHEDULE_IRRTIMEPOINTS] = irregularTimePoints.GetRange(0, irregularTimePoints.Count);
            }
            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.IRRTIMEPOINT_INTERVALSCHEDULE:
                    irregularTimePoints.Add(globalId);
                    break;
                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.IRRTIMEPOINT_INTERVALSCHEDULE:
                    if (irregularTimePoints.Contains(globalId))
                    {
                        irregularTimePoints.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }
                    break;
                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }
    }
}
