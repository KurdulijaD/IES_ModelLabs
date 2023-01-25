using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Switch : ConductingEquipment
    {
        public Switch(long globalId) : base(globalId)
        {
        }

        private long switchingOperation = 0;
        private float ratedCurrent;
        private bool retained;
        private int switchOnCount;
        private DateTime switchOnDate;
        private bool normalOpen;

        public long SwitchingOperation { get => switchingOperation; set => switchingOperation = value; }
        public float RatedCurrent { get => ratedCurrent; set => ratedCurrent = value; }
        public bool Retained { get => retained; set => retained = value; }
        public int SwitchOnCount { get => switchOnCount; set => switchOnCount = value; }
        public DateTime SwitchOnDate { get => switchOnDate; set => switchOnDate = value; }
        public bool NormalOpen { get => normalOpen; set => normalOpen = value; }

        public override bool HasProperty(ModelCode property)
        {
            switch(property)
            {
                case ModelCode.SWITCH_SWITCHINGOP:
                    return true;
                case ModelCode.SWITCH_RATEDCURRENT:
                    return true;
                case ModelCode.SWITCH_RETAINED:
                    return true;
                case ModelCode.SWITCH_SWITCHONCOUNT:
                    return true;
                case ModelCode.SWITCH_SWITCHONDATE:
                    return true;
                case ModelCode.SWITCH_NORMALOPEN:
                    return true;
                default:
                    return base.HasProperty(property);
            }           
        }

        public override void GetProperty(Property property)
        {
            switch(property.Id)
            {
                case ModelCode.SWITCH_SWITCHINGOP:
                    property.SetValue(switchingOperation);
                    break;
                case ModelCode.SWITCH_RATEDCURRENT:
                    property.SetValue(ratedCurrent);
                    break;
                case ModelCode.SWITCH_RETAINED:
                    property.SetValue(retained);
                    break;
                case ModelCode.SWITCH_SWITCHONCOUNT:
                    property.SetValue(switchOnCount);
                    break;
                case ModelCode.SWITCH_SWITCHONDATE:
                    property.SetValue(switchOnDate);
                    break;
                case ModelCode.SWITCH_NORMALOPEN:
                    property.SetValue(normalOpen);
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
                case ModelCode.SWITCH_SWITCHINGOP:
                    switchingOperation = property.AsReference();
                    break;
                case ModelCode.SWITCH_RATEDCURRENT:
                    ratedCurrent = property.AsFloat();
                    break;
                case ModelCode.SWITCH_RETAINED:
                    retained = property.AsBool();
                    break;
                case ModelCode.SWITCH_SWITCHONCOUNT:
                    switchOnCount = property.AsInt();
                    break;
                case ModelCode.SWITCH_SWITCHONDATE:
                    switchOnDate = property.AsDateTime();
                    break;
                case ModelCode.SWITCH_NORMALOPEN:
                    normalOpen = property.AsBool();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
            
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Switch @switch &&
                   base.Equals(obj) &&
                   switchingOperation == @switch.switchingOperation &&
                   ratedCurrent == @switch.ratedCurrent &&
                   retained == @switch.retained &&
                   switchOnCount == @switch.switchOnCount &&
                   switchOnDate == @switch.switchOnDate &&
                   normalOpen == @switch.normalOpen;
        }
    }
}
