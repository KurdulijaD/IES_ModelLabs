using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class IrregularTimePoint : IdentifiedObject
    {
        private float time;
        private float value1;
        private float value2;
        private long irrIntSch = 0;
        public IrregularTimePoint(long globalId) : base(globalId) { }

        public float Time { get => time; set => time = value; }
        public float Value1 { get => value1; set => value1 = value; }
        public float Value2 { get => value2; set => value2 = value; }
        public long IrrIntSch { get => irrIntSch; set => irrIntSch = value; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                IrregularTimePoint i = (IrregularTimePoint)x;
                return (i.time == this.time && i.value1 == this.value1 && i.value2 == this.value2 && i.irrIntSch == this.irrIntSch);
            }
            else
                return false;
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.IRRTIMEPOINT_INTERVALSCHEDULE:
                case ModelCode.IRRTIMEPOINT_TIME:
                case ModelCode.IRRTIMEPOINT_VALUE1:
                case ModelCode.IRRTIMEPOINT_VALUE2:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.IRRTIMEPOINT_INTERVALSCHEDULE:
                    property.SetValue(irrIntSch);
                    break;
                case ModelCode.IRRTIMEPOINT_TIME:
                    property.SetValue(time);
                    break;
                case ModelCode.IRRTIMEPOINT_VALUE1:
                    property.SetValue(value1);
                    break;
                case ModelCode.IRRTIMEPOINT_VALUE2:
                    property.SetValue(value2);
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
                case ModelCode.IRRTIMEPOINT_INTERVALSCHEDULE:
                    irrIntSch = property.AsReference();
                    break;
                case ModelCode.IRRTIMEPOINT_TIME:
                    time = property.AsFloat();
                    break;
                case ModelCode.IRRTIMEPOINT_VALUE1:
                    value1 = property.AsFloat();
                    break;
                case ModelCode.IRRTIMEPOINT_VALUE2:
                    value2 = property.AsFloat();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if(irrIntSch != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.IRRTIMEPOINT_INTERVALSCHEDULE] = new List<long>();
                references[ModelCode.IRRTIMEPOINT_INTERVALSCHEDULE].Add(irrIntSch);
            }
            base.GetReferences(references, refType);
        }

    }
}
