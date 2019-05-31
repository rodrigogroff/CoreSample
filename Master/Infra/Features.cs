
namespace Master
{
    public class FeatureState
    {
        public bool Execute { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Features
    {
        public bool Cache { get; set; }
        public string CacheLocation { get; set; }
        public FeatureState CreateAccount { get; set; }
        public FeatureState Authenticate { get; set; }
        public FeatureState CreateCategory { get; set; }
        public FeatureState CreateSubCategory { get; set; }
        public FeatureState CreateProduct { get; set; }
    }
}
