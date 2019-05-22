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

        int idx_config = 0, count_config = 0;

        public string GetHost(LocalNetworkTypes _type)
        {
            lock (this)
            {
                List<string> lst = null;
                int idx = 0, count = 0;

                switch (_type)
                {
                    case LocalNetworkTypes.Config:  lst = ConfigurationHosts;
                                                    idx = idx_config;
                                                    if (count_config == 0) count_config = lst.Count();
                                                    count = count_config;
                                                    break;
                }

                return ResolveHost(lst, ref idx, count);
            }
        }

        string ResolveHost(List<string> lst, ref int idx, int count)
        {
            if (count == 1) return lst[0];
            else
            {
                if (++idx >= count) idx = 0;
                return ConfigurationHosts[idx];
            }
        }
    }
}
