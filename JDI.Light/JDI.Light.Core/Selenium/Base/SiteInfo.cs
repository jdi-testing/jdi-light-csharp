using JDI.Core.Selenium.Elements.Composite;

namespace JDI.Core.Selenium.Base
{
    public class SiteInfo<TSite> where TSite : WebSite
    {
        public bool IsUsed;
        public TSite Site;

        public SiteInfo(TSite site)
        {
            Site = site;
            IsUsed = true;
        }
    }
}