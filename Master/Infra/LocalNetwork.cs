using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Master
{
    public enum LocalNetworkTypes
    {
        Configuration = 1,
        Portal = 2,
    }

    public class NetworkStats
    {
        public string Date;
        public int requests = 0;
        public int requestsPerSecond = 0;
    }

    public class LocalNetwork
    {
        public const string Secret = "ciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NTc5Mjk4ODcsImV4cCI6MTU1fhdsjhfeuyrejhdfj73333";

        public List<string> ConfigurationHosts { get; set; }

        public List<string> PortalHosts { get; set; }

        public long requests = 0;

        public Hashtable hshStats = new Hashtable();
        public List<string> lstStats = new List<string>();

        int idx_config = 0, count_config = 0;
        int idx_portal = 0, count_portal = 0;

        public NetworkStats GetStats()
        {
            string tag = DateTime.Now.ToString("ddMMyyyyHHmm");
            var ns = hshStats[tag] as NetworkStats;

            if (ns == null)
                ns = new NetworkStats();

            return new NetworkStats
            {
                Date = DateTime.Now.ToShortTimeString(),
                requests = ns.requests,
                requestsPerSecond = ns.requests > 0 ? ns.requests / 60 : 0
            };
        }

        public List<NetworkStats> GetStats(int lastMinutes)
        {
            var dt = DateTime.Now;
            var ret = new List<NetworkStats>();

            for (int i = 0; i < lastMinutes; i++)
            {
                string tag = GetTimeTag(dt);
                var ns = hshStats[tag] as NetworkStats;

                if (ns == null)
                    ns = new NetworkStats();

                ret.Add ( new NetworkStats
                {
                    Date = dt.ToShortTimeString(),
                    requests = ns.requests,
                    requestsPerSecond = ns.requests > 0 ? ns.requests / 60 : 0
                });

                dt = dt.AddMinutes(-1);
            }

            return ret;
        }

        public string GetTimeTag(DateTime dt)
        {
            return dt.Minute.ToString() + dt.Second.ToString();
        }

        public void UpdateRequestStat()
        {
            string tag = GetTimeTag(DateTime.Now);

            if (!(hshStats[tag] is NetworkStats ns))
            {
                ns = new NetworkStats();
                hshStats[tag] = ns;
                lstStats.Add(tag);

                int maxMinutesStatistics = 5;

                if (lstStats.Count > maxMinutesStatistics)
                {
                    var indexEl = lstStats.Count - maxMinutesStatistics;

                    for (int i = 0; i < indexEl; i++)
                        hshStats[lstStats[i]] = null;

                    lstStats.RemoveRange(0, indexEl);
                }
            }

            ns.requests++;
        }

        public string GetHost(LocalNetworkTypes _type)
        {
            lock (this)
            {
                UpdateRequestStat();

                List<string> lst = null;
                int idx = 0, count = 0;

                switch (_type)
                {
                    case LocalNetworkTypes.Configuration:
                        lst = ConfigurationHosts;
                        idx = idx_config;
                        if (count_config == 0) count_config = lst.Count();
                        count = count_config;
                        break;

                    case LocalNetworkTypes.Portal:
                        lst = PortalHosts;
                        idx = idx_portal;
                        if (count_portal == 0) count_portal = lst.Count();
                        count = count_portal;
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
