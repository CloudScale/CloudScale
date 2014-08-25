using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using CloudScale.Movies.DataService;
using CloudScale.Web;
using NUnit.Framework;

namespace CloudScale.ConventionTests
{
    public class SolutionTypesDataSource
    {
        public static IEnumerable TestCases
        {
            get
            {
                Assembly[] assemblies =
                {
                    typeof (Startup).Assembly,
                    typeof (Api.Startup).Assembly,
                    typeof (SignalR.Startup).Assembly,
                    typeof (WorkerRole).Assembly,
                    typeof (Movies.LookupService.WorkerRole).Assembly
                };

                foreach (Type type in assemblies.SelectMany(p => p.GetTypes()))
                    yield return new TestCaseData(type);
            }
        }
    }
}