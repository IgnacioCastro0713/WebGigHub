using NUnit.Framework;
using System;
using System.Activities.Statements;
using System.Transactions;
using NUnit.Framework.Interfaces;

namespace WebGigHub.IntegrationTest
{
    public class Isolated: Attribute, ITestAction
    {
        private TransactionScope _transactionScope;
        
        public void BeforeTest(ITest test)
        {
            _transactionScope = new TransactionScope();
        }

        public void AfterTest(ITest test)
        {
            _transactionScope = null;
        }

        public ActionTargets Targets => ActionTargets.Test;
    }
}
