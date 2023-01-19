using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class OutageSchedule : IrregularIntervalSchedule
    {
        private List<long> switchingOps = new List<long>();
        public OutageSchedule(long globalId) : base(globalId) { }

        public List<long> SwitchingOps { get => switchingOps; set => switchingOps = value; }

        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                OutageSchedule s = (OutageSchedule)x;
                return (CompareHelper.CompareLists(s.switchingOps, this.switchingOps));
            }
            else
                return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.OUTAGESCHEDULE_SWITCHINGOPS:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.OUTAGESCHEDULE_SWITCHINGOPS:
                    property.SetValue(switchingOps);
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
                return switchingOps.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (switchingOps != null && switchingOps.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.OUTAGESCHEDULE_SWITCHINGOPS] = switchingOps.GetRange(0, switchingOps.Count);
            }
            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SWITCHINGOP_OUTAGESCHEDULE:
                    switchingOps.Add(globalId);
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
                case ModelCode.SWITCHINGOP_OUTAGESCHEDULE:
                    if (switchingOps.Contains(globalId))
                    {
                        switchingOps.Remove(globalId);
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
