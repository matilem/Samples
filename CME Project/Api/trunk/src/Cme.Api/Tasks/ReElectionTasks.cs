using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Helpers;
using Aafp.Cme.Api.Tasks.Interfaces;
using ApiClientHelper.Components;

namespace Aafp.Cme.Api.Tasks
{
    public class ReElectionTasks : IReElectionTasks
    {
        public ICreditTasks CreditTasks { get; set; }

        public ICreditTypeTasks CreditTypeTasks { get; set; }

        public IIndividualTasks IndividualTasks { get; set; }

        public async Task<ReElectionDto> GetReElectionByWebLogin(string webLogin)
        {
            var dto = new ReElectionDto();
            var individual = await IndividualTasks.GetIndividualByWebLogin(webLogin);

            if (individual.IsMember && individual.HasReElectionCycle)
            {
                var credits = CreditTasks.GetByCustomerForReElectionCalculation(individual.Key, individual.ReElectionStartYear, individual.ReElectionEndYear);
                var creditTypes = CreditTypeTasks.GetByLimitType("R");
                var totals = new ReElectionTotalsHelper(creditTypes, individual.ChapterStateCode);
                totals = CalculateReElectionTotals(credits, totals, individual.ChapterStateCode);
                dto.Totals = AutoMapper.Mapper.Map(totals, new ReElectionTotalsDto());
                dto.Totals.ReElectionEndYear = individual.ReElectionEndYear;
                dto.Totals.ReElectionStartYear = individual.ReElectionStartYear;

                var requirementsFulfilled = dto.Totals.RequirementsFulfilledAll;

                if (DateTime.Now.Year > individual.ReElectionEndYear && !requirementsFulfilled)
                {
                    dto.Message = "Insufficient CME, Check Transcript";
                    dto.Status = ReElectionStatusHelper.Danger;
                }
                else if (DateTime.Now.Year == individual.ReElectionEndYear && !requirementsFulfilled)
                {
                    dto.Message = "Insufficient CME, Check Transcript";
                    dto.Status = ReElectionStatusHelper.Warning;
                }
                else
                {
                    dto.Message = "In Good Standing";
                    dto.Status = ReElectionStatusHelper.Good;
                }
            }
            else
            {
                dto.Message = "Not Applicable";
                dto.Status = ReElectionStatusHelper.Invalid;
            }

            dto.IsMember = individual.IsMember;

            return dto;
        }

        public ReElectionTotalsHelper CalculateReElectionTotals(List<CreditReElectionDto> credits, ReElectionTotalsHelper totals, string chapterStateCode)
        {
            foreach (var credit in credits)
            {
                // Non CME Credit should not be counted into ReElection Totals - go to next Credit
                if (credit.IsWriteIn() && credit.CreditTypeTitle == CreditTypeHelper.NonCmeCredit)
                {
                    continue;
                }

                // First sum up all overall totals
                totals.PrescribedCredits += credit.PrescribedCredits;
                totals.ElectiveCredits += credit.ElectiveCredits;

                if (credit.IsGroup())
                {
                    totals.GroupCredits += credit.PrescribedCredits + credit.ElectiveCredits;
                }

                if (chapterStateCode == "FL")
                {
                    if (credit.SessionKey.HasValue && credit.ActivityKey.HasValue && credit.FloridaChapterApproved)
                        totals.ChapterCredits += credit.PrescribedCredits + credit.ElectiveCredits;
                }
                else if (chapterStateCode == "MD")
                {
                    if (credit.SessionKey.HasValue && credit.ActivityKey.HasValue && credit.MarylandChapterApproved)
                        totals.ChapterCredits += credit.PrescribedCredits + credit.ElectiveCredits;
                }

                // Load credit type limit details.  Adjust totals based upon them.
                if (credit.IsWriteIn() || credit.IsAlso())
                {
                    if (credit.CreditTypeKey.HasValue)
                    {
                        if (credit.CreditTypeLimitType == "R")
                        {
                            decimal creditTypeCredits = 0;
                            if (credit.CreditTypeDesignation == "P")
                                creditTypeCredits = credit.PrescribedCredits;

                            if (credit.CreditTypeDesignation == "E")
                                creditTypeCredits = credit.ElectiveCredits;

                            var currCreditTypeTotals = totals.CreditTypeReElectionTotalsList.Find(t => t.CreditTypeKey == credit.CreditTypeKey.Value);

                            if (currCreditTypeTotals != null)
                                currCreditTypeTotals.CreditsReported += creditTypeCredits;
                        }
                    }
                }
            }

            AdjustEnrichmentCredits(totals.CreditTypeReElectionTotalsList);

            return totals;
        }

        private void AdjustEnrichmentCredits(List<CreditTypeReElectionTotalsHelper> totals)
        {
            var enrichmentTotals = totals.FindAll(creditTypeTotals => creditTypeTotals.CreditTypeTitle.Contains("Enrichment"));

            if (enrichmentTotals.Count != 2)
                throw new ServiceException("Credit Types not loaded correctly.  Should be two total.  " + enrichmentTotals.Count + " found.");

            enrichmentTotals[0].CreditsReported += enrichmentTotals[1].CreditsReported;
            totals.Remove(enrichmentTotals[1]);
            enrichmentTotals[0].CreditTypeTitle = "Professional Enrichment";
        }
    }
}