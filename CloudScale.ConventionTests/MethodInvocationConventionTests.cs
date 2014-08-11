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
    [TestFixture]
    public class MethodInvocationConventionTests
    {
        private static int GetReferenceCount(Type type, string undesiredTypeName)
        {
            UriBuilder uri = new UriBuilder(type.Assembly.CodeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(path);
            TypeDefinition vm = assemblyDefinition.Modules.SelectMany(m => m.GetTypes().Where(p => p.FullName == type.FullName)).Single();
            int referenceCount = vm.Methods.Where(p => p.HasBody)
                                           .SelectMany(p => p.Body.Instructions.Where(i => i.OpCode.Code == Mono.Cecil.Cil.Code.Call &&
                                                       ((Mono.Cecil.MethodReference)i.Operand).DeclaringType.FullName.Equals(undesiredTypeName))).Count();
            return referenceCount;
        }

        [TestCaseSource(typeof(SolutionTypesDataSource), "TestCases")]
        public void ClassesShouldNotReferenceCloudConfigurationManager(Type type)
        {
            int refCount = GetReferenceCount(type, "Microsoft.WindowsAzure.CloudConfigurationManager");

            refCount.ShouldBe(0);
        }
    }
}
