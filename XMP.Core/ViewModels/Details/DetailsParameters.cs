using FlexiMvvm.ViewModels;

namespace XMP.Core.ViewModels.Details
{
    public class DetailsParameters : Parameters
    {
        public DetailsParameters()
        {
            CreateNew = true;
        }

        public DetailsParameters(string localId)
        {
            CreateNew = false;

            LocalId = localId;
        }

        public bool CreateNew
        {
            get => Bundle.GetBool();
            private set => Bundle.SetBool(value);
        }

        public string LocalId
        {
            get => Bundle.GetString();
            private set => Bundle.SetString(value);
        }
    }
}
