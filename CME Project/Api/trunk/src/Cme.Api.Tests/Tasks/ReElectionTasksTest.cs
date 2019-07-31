using System;
using System.Collections.Generic;
using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Helpers;
using Aafp.Cme.Api.Tasks;
using Aafp.Cme.Api.Tasks.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Aafp.Cme.Api.Tests.Tasks
{
    [TestFixture]
    [Category("Unit")]
    public class ReElectionTasksTest : AbstractTestFixture
    {
        private readonly Guid enrichGroupCreditTypeKey = Guid.NewGuid();

        private readonly Guid enrichNonGroupCreditTypeKey = Guid.NewGuid();

        private readonly Guid randomCreditTypeKey = Guid.NewGuid();
    
        private IReElectionTasks reElectionTasks;

        private ICreditTasks creditTasks;

        private ICreditTypeTasks creditTypeTasks;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            reElectionTasks = new ReElectionTasks();
            creditTasks = Substitute.For<ICreditTasks>();
            creditTypeTasks = Substitute.For<ICreditTypeTasks>();
        }

        [TearDown]
        public override void TearDown()
        {
            reElectionTasks = null;
            creditTasks = null;
            creditTypeTasks = null;
            base.TearDown();
        }

        [Test]
        public void TestCalculateReElectionTotals()
        {
            var creditTypes = GetCreditTypeList();
            var credits = SetUpTestCredits();
            var membersChapterState = "MD";

            creditTypeTasks.GetAllCreditTypes().Returns(creditTypes);
            creditTasks.GetByCustomerForReElectionCalculation(new Guid(), 1, 2).Returns(credits);

            var totalsHelper = new ReElectionTotalsHelper(creditTypes, membersChapterState);
            var totals = reElectionTasks.CalculateReElectionTotals(credits, totalsHelper, membersChapterState);

            // result expectations:
            // 40 credits of each, 30 group, 8 enrich (limit), 33 Elective Applied, 36 Prescribed Applied, 10 Maryland (if member in MD), 10 Florida (if member in FL)
            Assert.AreEqual(40, totals.PrescribedCredits);
            Assert.AreEqual(40, totals.ElectiveCredits);
            Assert.AreEqual(40, totals.GroupCredits);
            Assert.AreEqual(36, totals.PrescribedCreditsApplied);
            Assert.AreEqual(33, totals.ElectiveCreditsApplied);
            Assert.AreEqual("MD", totals.ChapterStateCode);
            Assert.AreEqual(10, totals.ChapterCredits); // member in maryland

            // enrichment testing
            var enrichCreditTypeTotalsList = totals.CreditTypeReElectionTotalsList.FindAll(delegate (CreditTypeReElectionTotalsHelper creditTypeTotals)
                    {
                        return creditTypeTotals.CreditTypeTitle.Contains("Enrichment");
                    });
            Assert.AreEqual(1, enrichCreditTypeTotalsList.Count);
            var enrichCreditTypeTotals = enrichCreditTypeTotalsList[0];
            Assert.AreEqual(15, enrichCreditTypeTotals.CreditsReported);
            Assert.AreEqual(8, enrichCreditTypeTotals.CreditsApplied);
        }
        
        #region Private Credit Setup Methods

        private List<CreditReElectionDto> SetUpTestCredits()
        {
            var credits = new List<CreditReElectionDto>();
            var enrichmentGroupCreditType = GetEnrichmentGroupCreditType();
            var enrichmentNonGroupCreditType = GetEnrichmentNonGroupCreditType();

            // 5 credits of each
            credits.Add(GetNewTestCredit(5, 5, false, false));

            // 10 credits of each
            credits.Add(GetNewTestCredit(5, 5, false, false)); 

             // 15 credits of each, 10 group, 5 enrich
            var enrichGroupCredit1 = GetNewTestCredit(5, 5, false, false);
            enrichGroupCredit1.CreditTypeLimitType = enrichmentGroupCreditType.LimitType;
            enrichGroupCredit1.CreditTypeDesignation = enrichmentGroupCreditType.Designation;
            enrichGroupCredit1.CreditTypeGroupType = enrichmentGroupCreditType.GroupType;
            enrichGroupCredit1.CreditTypeTitle = enrichmentGroupCreditType.Title;
            enrichGroupCredit1.CreditTypeKey = enrichmentGroupCreditType.Key;
            credits.Add(enrichGroupCredit1);

            // 20 credits of each, 20 group, 8 enrich (limit), 18 Elective Applied
            var enrichGroupCredit2 = GetNewTestCredit(5, 5, false, false);
            enrichGroupCredit2.CreditTypeLimitType = enrichmentGroupCreditType.LimitType;
            enrichGroupCredit2.CreditTypeDesignation = enrichmentGroupCreditType.Designation;
            enrichGroupCredit2.CreditTypeGroupType = enrichmentGroupCreditType.GroupType;
            enrichGroupCredit2.CreditTypeTitle = enrichmentGroupCreditType.Title;
            enrichGroupCredit2.CreditTypeKey = enrichmentGroupCreditType.Key;
            credits.Add(enrichGroupCredit2);

            // 25 credits of each, 20 group, 8 enrich (limit), 18 Elective Applied
            var enrichNonGroupCredit = GetNewTestCredit(5, 5, false, false);
            enrichNonGroupCredit.CreditTypeLimitType = enrichmentNonGroupCreditType.LimitType;
            enrichNonGroupCredit.CreditTypeDesignation = enrichmentNonGroupCreditType.Designation;
            enrichNonGroupCredit.CreditTypeGroupType = enrichmentNonGroupCreditType.GroupType;
            enrichNonGroupCredit.CreditTypeKey = enrichmentNonGroupCreditType.Key;
            enrichNonGroupCredit.CreditTypeTitle = enrichmentNonGroupCreditType.Title;
            credits.Add(enrichNonGroupCredit);

            // 30 credits of each, 30 group, 8 enrich (limit), 23 Elective Applied, 10 Maryland (if member in MD)
            var chapterCredit1 = GetNewTestCredit(5, 5, true, false);
            chapterCredit1.ActivityType = "Live Activity";
            chapterCredit1.SessionKey = Guid.NewGuid();
            chapterCredit1.ActivityKey = Guid.NewGuid();
            credits.Add(chapterCredit1);

            // 35 credits of each, 30 group, 8 enrich (limit), 28 Elective Applied, 10 Florida (if member in FL), 10 Maryland (if member in MD)
            var chapterCredit2 = GetNewTestCredit(5, 5, false, true);
            chapterCredit2.ActivityType = "Medical Journal";
            chapterCredit2.SessionKey = Guid.NewGuid();
            chapterCredit2.ActivityKey = Guid.NewGuid();
            credits.Add(chapterCredit2);

            //// 40 credits of each, 40 group, 8 enrich (limit), 33 Elective Applied, 36 Prescribed Applied, 10 Maryland (if member in MD), 10 Florida (if member in FL)
            var randonGroupCreditType = GetRandomGroupCreditType();
            var anotherCreditWithCreditType = GetNewTestCredit(5, 5, false, false);
            anotherCreditWithCreditType.CreditTypeDesignation = randonGroupCreditType.Designation;
            anotherCreditWithCreditType.CreditTypeGroupType = randonGroupCreditType.GroupType;
            anotherCreditWithCreditType.CreditTypeKey = randonGroupCreditType.Key;
            anotherCreditWithCreditType.CreditTypeLimitType = randonGroupCreditType.LimitType;
            anotherCreditWithCreditType.CreditTypeTitle = randonGroupCreditType.Title;
            credits.Add(anotherCreditWithCreditType);

            return credits;
        }
        
        private static CreditReElectionDto GetNewTestCredit(int prescribedCredit, int electiveCredit, bool mdApproved, bool flApproved)
        {
            var credit = new CreditReElectionDto();
            credit.PrescribedCredits = prescribedCredit;
            credit.ElectiveCredits = electiveCredit;
            credit.MarylandChapterApproved = mdApproved;
            credit.FloridaChapterApproved = flApproved;

            return credit;
        }

        private List<CreditTypeDto> GetCreditTypeList()
        {
            var creditTypes = new List<CreditTypeDto>();
            creditTypes.Add(GetEnrichmentGroupCreditType());
            creditTypes.Add(GetEnrichmentNonGroupCreditType());
            creditTypes.Add(GetRandomGroupCreditType());

            var randomType1 = GetCreditType(Guid.NewGuid(), "Random Type 1", "P", "G", "R", 10);
            var randomType2 = GetCreditType(Guid.NewGuid(), "Random Type 2", "P", "N", "R", 10);

            creditTypes.Add(randomType1);
            creditTypes.Add(randomType2);

            return creditTypes;
        }

        private CreditTypeDto GetEnrichmentGroupCreditType()
        {
            return GetCreditType(enrichGroupCreditTypeKey, "Enrichment Group", "E", "G", "R", 8);
        }

        private CreditTypeDto GetEnrichmentNonGroupCreditType()
        {
            return GetCreditType(enrichNonGroupCreditTypeKey, "Enrichment Non-Group", "E", "N", "R", 8);
        }

        private CreditTypeDto GetRandomGroupCreditType()
        {
            return GetCreditType(randomCreditTypeKey, "Random Credit Type", "P", "G", "R", 1);
        }

        private CreditTypeDto GetCreditType(Guid key, string title, string desig, string groupType, string limitType, decimal? maxCredits)
        {
            var creditType = new CreditTypeDto();
            creditType.Key = key;
            creditType.Title = title;
            creditType.Designation = desig;
            creditType.GroupType = groupType;
            creditType.LimitType = limitType;
            creditType.MaximumCreditsPerCycle = maxCredits;

            return creditType;
        }

        #endregion
    }
}