using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FTADOTAutomation.Helpers
{

    /*/
     * in order for this class to run, a PAT (personal access token) is needed. 
     * 
     * it will look for a SessionToken.temp file in the source folder directory. 
     * this file should contain a Json formatted string having an entry key=AZUREDEVOPS_PAT with the value of the PAT
     * 
     * otherwise, it will look in the Environment variables for AZUREDEVOPS_PAT variable
     * 
     * if none of the above work, null is returned and the entire class will be disabled 
     */
    class Devops
    {
        public const int OUTCOME_RESET = 0;
        public const int OUTCOME_PASS = 2;
        public const int OUTCOME_FAIL = 3;
        private static int TestPlanId => int.Parse(Environment.GetEnvironmentVariable("AZUREDEVOPS_TESTPLAN_ID"));
        private static int TestSuiteId => int.Parse(Environment.GetEnvironmentVariable("AZUREDEVOPS_TESTSUITE_ID"));
        private static string ORGANIZATION => Environment.GetEnvironmentVariable("AZUREDEVOPS_ORGANIZATION");
        private static string PROJECT => Environment.GetEnvironmentVariable("AZUREDEVOPS_PROJECT");

        private static string ENV => Environment.GetEnvironmentVariable("ENVIRONMENT_NAME");

        private static AuthenticationHeaderValue authenticationHeader = loadPAT();

        private static AuthenticationHeaderValue loadPAT()
        {
            throw new NotImplementedException();
        }

        public static void markTestCases(Dictionary<int, int> TCOutcome)
        {
            if(authenticationHeader == null)
            {
                return;
            }
            TriggerTestSuiteExecution(TestPlanId, TestSuiteId, TCOutcome);
        }

        private static void TriggerTestSuiteExecution(int testPlanId, int testSuiteId, Dictionary<int, int> TCOutcome)
        {
            var body = new JArray();

            var testPointIds = getTestPointIds(testPlanId, testSuiteId, TCOutcome.Keys);

            foreach (var testCase in testPointIds)
            {
                var testCaseId = testCase.Key;

                foreach (var testPointId in testCase.Value)
                {
                    var testResult = new JObject() { { "id", testPointId } };

                    if (TCOutcome[testCaseId] == OUTCOME_RESET)
                    {
                        testResult["isActive"] = true;
                    }
                    else
                    {
                        testResult["results"] = new JObject() { { "outcome", TCOutcome[testCaseId] } };
                    }
                    body.Add(testResult);
                }
            }

            Log.Debug("Test Suite " + testSuiteId);
            var url = $"https://dev.azure.com/{ORGANIZATION}/{PROJECT}/_apis/testplan/Plans/{testPlanId}/suites/{testSuiteId}/TestPoint?api-version=6.0-preview.2";

            RestAPI.PATCH(url, body, authenticationHeader);
        }
       
        private static Dictionary<int, List<int>> getTestPointIds(int planId, int suiteId, IEnumerable<int> testCaseIds)
        {
            Log.Debug("getting test point @testcases", ("@testcases", testCaseIds));

            var url = $"https://dev.azure.com/{ORGANIZATION}/{PROJECT}/_apis/testplan/Plans/{planId}/suites/{suiteId}/TestPoint?api-version=6.0-preview.2";

            var testPoints = RestAPI.GET(url, authenticationHeader);
            var result = new Dictionary<int, List<int>>();

            var unknownMatches = new Dictionary<int, List<int>>();

            foreach (dynamic testPoint in (JArray)testPoints.value)
            {
                if (testPoint?.testCaseReference?.id != null && testCaseIds.Contains((int)testPoint.testCaseReference.id))
                {
                    if (testPoint.configuration.name == $"Automation {ENV}")
                    {
                        if (!result.ContainsKey((int)testPoint.testCaseReference.id))
                        {
                            result.Add((int)testPoint.testCaseReference.id, new List<int>());
                        }
                        result[(int)testPoint.testCaseReference.id].Add((int)testPoint.id);
                    }
                    else if(testPoint.configuration.name == $"Automation Unknown")
                    {
                        if (!unknownMatches.ContainsKey((int)testPoint.testCaseReference.id))
                        {
                            unknownMatches.Add((int)testPoint.testCaseReference.id, new List<int>());
                        }
                        unknownMatches[(int)testPoint.testCaseReference.id].Add((int)testPoint.id);
                    }  
                }
            }

           if(result.Count() != testCaseIds.Count())
           {
                foreach(var unknownMatch in unknownMatches)
                {
                    if (!result.ContainsKey((int)unknownMatch.Key))
                    {
                        result.Add(unknownMatch.Key, unknownMatch.Value);
                    }
                }
           }
            return result;
        }


        private static async Task<IEnumerable<dynamic>> getTestSuites(int testCaseId)
        {
            var url = $"https://dev.azure.com/{ORGANIZATION}/_apis/testplan/suites?testCaseId={testCaseId}&api-version=5.0";

            var response = RestAPI.GET(url, authenticationHeader);
            return ((JArray)response.value).ToObject<IEnumerable<dynamic>>();
        }


       
    }
}
