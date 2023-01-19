using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class SwitchingOperation : IdentifiedObject
    {
        private long outageSchedule = 0;
        private List<long> switches = new List<long>();
        private SwitchState newState;
        private DateTime operationTime;
        public SwitchingOperation(long globalId) : base(globalId) { }

        public long OutageSchedule { get => outageSchedule; set => outageSchedule = value; }
        public List<long> Switches { get => switches; set => switches = value; }
        public SwitchState NewState { get => newState; set => newState = value; }
        public DateTime OperationTime { get => operationTime; set => operationTime = value; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                SwitchingOperation s = (SwitchingOperation)x;
                return (s.outageSchedule == this.outageSchedule && CompareHelper.CompareLists(s.switches, this.switches) && 
                    s.newState == this.newState && s.operationTime == this.operationTime);
            }
            else
                return false;
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.SWITCHINGOP_NEWSTATE:
                case ModelCode.SWITCHINGOP_OPERATIONTIME:
                case ModelCode.SWITCHINGOP_OUTAGESCHEDULE:
                case ModelCode.SWITCHINGOP_SWITCHES:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SWITCHINGOP_NEWSTATE:
                    property.SetValue((short)newState);
                    break;
                case ModelCode.SWITCHINGOP_OPERATIONTIME:
                    property.SetValue(operationTime);
                    break;
                case ModelCode.SWITCHINGOP_OUTAGESCHEDULE:
                    property.SetValue(outageSchedule);
                    break;
                case ModelCode.SWITCHINGOP_SWITCHES:
                    property.SetValue(switches);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SWITCHINGOP_NEWSTATE:
                    newState = (SwitchState)property.AsEnum();
                    break;
                case ModelCode.SWITCHINGOP_OPERATIONTIME:
                    operationTime = property.AsDateTime();
                    break;
                case ModelCode.SWITCHINGOP_OUTAGESCHEDULE:
                    outageSchedule = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override bool IsReferenced
        {
            get
            {
                return switches.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if(outageSchedule != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.SWITCHINGOP_OUTAGESCHEDULE] = new List<long>();
                references[ModelCode.SWITCHINGOP_OUTAGESCHEDULE].Add(outageSchedule);
            }
            if(switches != null && switches.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.SWITCHINGOP_SWITCHES] = switches.GetRange(0, switches.Count);
            }
            base.GetReferences(references, refType);
        }
        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SWITCH_SWITCHINGOP:
                    switches.Add(globalId);
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
                case ModelCode.SWITCH_SWITCHINGOP:
                    if (switches.Contains(globalId))
                    {
                        switches.Remove(globalId);
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
