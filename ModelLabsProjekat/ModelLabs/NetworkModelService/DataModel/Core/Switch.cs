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

        public long SwitchingOperation { get => switchingOperation; set => switchingOperation = value; }

        public override bool HasProperty(ModelCode property)
        {
            switch(property)
            {
                case ModelCode.SWITCH_SWITCHINGOP:
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
                default:
                    base.SetProperty(property);
                    break;
            }
            
        }

        public override bool Equals(object obj)
        {
            if(base.Equals(obj))
            {
                Switch x = (Switch)obj;
                return (x.SwitchingOperation == this.SwitchingOperation);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
