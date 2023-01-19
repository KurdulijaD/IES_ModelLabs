using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class CurveData : IdentifiedObject
    {
        private float xvalue;
        private float y1value;
        private float y2value;
        private float y3value;
        private long curve = 0;
        public CurveData(long globalId) : base(globalId)
        {

        }

        public float Xvalue { get => xvalue; set => xvalue = value; }
        public float Y1value { get => y1value; set => y1value = value; }
        public float Y2value { get => y2value; set => y2value = value; }
        public float Y3value { get => y3value; set => y3value = value; }
        public long Curve { get => curve; set => curve = value; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                CurveData c = (CurveData)x;
                return (c.xvalue == this.xvalue && c.y1value == this.y1value && c.y2value == this.y2value && c.y3value == this.y3value && c.curve == this.curve);
            }
            else
                return false;
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.CURVEDATA_CURVE:
                case ModelCode.CURVEDATA_XVALUE:
                case ModelCode.CURVEDATA_Y1VALUE:
                case ModelCode.CURVEDATA_Y2VALUE:
                case ModelCode.CURVEDATA_Y3VALUE:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.CURVEDATA_CURVE:
                    property.SetValue(curve);
                    break;
                case ModelCode.CURVEDATA_XVALUE:
                    property.SetValue(xvalue);
                    break;
                case ModelCode.CURVEDATA_Y1VALUE:
                    property.SetValue(y1value);
                    break;
                case ModelCode.CURVEDATA_Y2VALUE:
                    property.SetValue(y2value);
                    break;
                case ModelCode.CURVEDATA_Y3VALUE:
                    property.SetValue(y3value);
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.CURVEDATA_CURVE:
                    curve = property.AsReference();
                    break;
                case ModelCode.CURVEDATA_XVALUE:
                    xvalue = property.AsFloat();
                    break;
                case ModelCode.CURVEDATA_Y1VALUE:
                    y1value = property.AsFloat();
                    break;
                case ModelCode.CURVEDATA_Y2VALUE:
                    y2value = property.AsFloat();
                    break;
                case ModelCode.CURVEDATA_Y3VALUE:
                    y3value = property.AsFloat();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if(curve != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.CURVEDATA_CURVE] = new List<long>();
                references[ModelCode.CURVEDATA_CURVE].Add(curve);
            }
            base.GetReferences(references, refType);
        }
    }
}
