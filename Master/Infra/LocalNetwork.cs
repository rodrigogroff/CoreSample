using System.Collections.Generic;
using System.Linq;

namespace Master
{
    public enum LocalNetworkTypes
    {
        Config = 0,
    }

    public class LocalNetwork
    {
        public const string Secret = "ciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NTc5Mjk4ODcsImV4cCI6MTU1fhdsjhfeuyrejhdfj73333";
        public List<string> ConfigurationHosts { get; set; }

        int idx_config = 0;

        public string GetHost(LocalNetworkTypes _type)
        {
            lock (this)
            {
                List<string> lst = null;
                int idx = 0;

                switch (_type)
                {
                    case LocalNetworkTypes.Config: lst = ConfigurationHosts; idx = idx_config; break;
                }

                return ResolveHost(lst, ref idx);
            }
        }

        string ResolveHost(List<string> lst, ref int idx)
        {
            if (lst == null) return null;
            if (lst.Count() == 0) return null;
            if (lst.Count() == 1) return lst[0];
            else
            {
                int max = lst.Count();

                if (++idx >= max)
                    idx = 0;

                return ConfigurationHosts[idx];
            }
        }
    }
}
