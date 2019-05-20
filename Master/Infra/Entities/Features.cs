
namespace Gateway
{
    public class FeatureState
    {
        public bool Execute { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Features
    {
        public FeatureState CreateAccount { get; set; }
        public FeatureState Authenticate { get; set; }
    }
}
