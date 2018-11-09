using System;
using JDI.Web.Selenium.Elements.Composite;
using NUnit.Framework;

namespace JDI.Web.Selenium.Base
{
    public class BaseParallelTest<TSite> : BaseParallelTest<string, TSite>
        where TSite : WebSite
    {
    }

    public class BaseParallelTest<TScope, TSite>
        where TSite : WebSite
        where TScope : class
    {
        public TScope Scope = null;
        public SiteInfo<TSite> SiteInfo;
        public TSite Site => SiteInfo.Site;

        [OneTimeSetUp]
        public void SetUp()
        {
            SiteInfo = SiteFactory<TScope, TSite>.Site(Scope ?? Activator.CreateInstance<TScope>());
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            SiteInfo.IsUsed = false;
        }
    }
}