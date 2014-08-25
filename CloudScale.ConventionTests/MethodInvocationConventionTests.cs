using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using NUnit.Framework;
using Shouldly;

namespace CloudScale.ConventionTests
{
    [TestFixture]
    public class MethodInvocationConventionTests
    {
        private static int GetReferenceCount(Type type, string undesiredTypeName)
        {
            var uri = new UriBuilder(type.Assembly.CodeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(path);
            IEnumerable<TypeDefinition> vms =
                assemblyDefinition.Modules.SelectMany(m => m.GetTypes().Where(p => p.GetType() == type));
            TypeDefinition vm = vms.FirstOrDefault();
            int referenceCount = vm.Methods.Where(p => p.HasBody)
                .SelectMany(p => p.Body.Instructions.Where(i => i.OpCode.Code == Code.Call &&
                                                                ((MethodReference) i.Operand).DeclaringType.FullName
                                                                    .Equals(undesiredTypeName))).Count();
            return referenceCount;
        }

        [TestCaseSource(typeof (SolutionTypesDataSource), "TestCases")]
        public void ClassesShouldNotReferenceCloudConfigurationManager(Type type)
        {
            int refCount = GetReferenceCount(type, "Microsoft.WindowsAzure.CloudConfigurationManager");

            refCount.ShouldBe(0);
        }
    }
}