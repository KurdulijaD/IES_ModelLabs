using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
	public class Equipment : PowerSystemResource
	{
        private bool normallyInService;
        private bool agregate;
		public Equipment(long globalId) : base(globalId) 
		{
		}

        public bool NormallyInService { get => normallyInService; set => normallyInService = value; }
        public bool Agregate { get => agregate; set => agregate = value; }

        public override bool Equals(object obj)
        {
            return obj is Equipment equipment &&
                   base.Equals(obj) &&
                   normallyInService == equipment.normallyInService &&
                   agregate == equipment.agregate;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.EQUIPMENT_NORMALLYINSERVICE:
                    property.SetValue(normallyInService);
                    break;
                case ModelCode.EQUIPMENT_AGREGATE:
                    property.SetValue(agregate);
                    break;
                    base.GetProperty(property);
                    break;
            }
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.EQUIPMENT_NORMALLYINSERVICE:
                    return true;
                case ModelCode.EQUIPMENT_AGREGATE:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.EQUIPMENT_NORMALLYINSERVICE:
                    normallyInService = property.AsBool();
                    break;
                case ModelCode.EQUIPMENT_AGREGATE:
                    agregate = property.AsBool();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

    }
}
