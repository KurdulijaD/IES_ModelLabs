using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class GroundDisconnector : Switch
    {
        public GroundDisconnector(long globalId) : base(globalId)
        {
        }
        public override bool Equals(object x)
        {
            return base.Equals(x);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void GetProperty(Property property)
        {
            base.GetProperty(property);
        }

        public override bool HasProperty(ModelCode property)
        {
            return base.HasProperty(property);
        }

        public override void SetProperty(Property property)
        {
            base.SetProperty(property);
        }
    }
}
