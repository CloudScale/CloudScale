using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Mono.Cecil;
using System.Collections;
using System.Reflection;
using Shouldly;

namespace CloudScale.ConventionTests
{
    public class SolutionTypesDataSource
    {
        public static IEnumerable TestCases
        {
            get
            {
                Assembly[] assemblies = new Assembly[] { 
                        typeof(CloudScale.Web.Startup).Assembly, 
                        typeof(CloudScale.Api.Startup).Assembly,
                        typeof(CloudScale.SignalR.Startup).Assembly,
                        typeof(CloudScale.Movies.DataService.WorkerRole).Assembly,
                        typeof(CloudScale.Movies.LookupService.WorkerRole).Assembly ,
                    };

                foreach (Type type in assemblies.SelectMany(p => p.GetTypes()))
                    yield return new TestCaseData(type);
            }
        }
    }
}
