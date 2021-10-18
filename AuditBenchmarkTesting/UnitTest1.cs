using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Audit_Benchmark_Service.Service_Layer;
using Audit_Benchmark_Service;
using Audit_Benchmark_Service.Models;
using System;
using Audit_Benchmark_Service.Controllers;
using Microsoft.Extensions.Logging;

namespace AuditBenchmarkTesting
{
    public class Tests
    {
        BenchmarkService getList;
        Mock<BenchmarkService> moqobj;
        AuditBenchmarkController controller;
        Mock<ILogger<AuditBenchmarkController>> mockController;
        [SetUp]
        public void Setup()
        {
            getList = new BenchmarkService();
            moqobj = new Mock<BenchmarkService>();
            moqobj.Setup(x => x.GetListOfBenchmarks()).Returns(new List<AuditBenchmarkClass>()
            {
                new AuditBenchmarkClass(){ AuditType="Internal",BenchmarkNoAnswers=3 },
                new AuditBenchmarkClass(){ AuditType="SOX",BenchmarkNoAnswers=1 }
            });
            controller = new AuditBenchmarkController();
            mockController = new Mock<ILogger<AuditBenchmarkController>>();
        }

        [Test]
        public void BenchmarkServiceTest()
        {
            List<AuditBenchmarkClass> expected = moqobj.Object.GetListOfBenchmarks();
            List<AuditBenchmarkClass> actual = getList.GetListOfBenchmarks();
            Console.WriteLine(actual[0].AuditType);
            Console.WriteLine(actual[0].BenchmarkNoAnswers);
            //Assert.That(expected[0].AuditType==actual[0].AuditType && expected[0].BenchmarkNoAnswers == actual[0].BenchmarkNoAnswers
            //&& expected[1].AuditType == actual[1].AuditType && expected[1].BenchmarkNoAnswers == actual[1].BenchmarkNoAnswers);
            Assert.IsInstanceOf<List<AuditBenchmarkClass>>(actual);
        }
        [Test]
        public void AuditBenchmarkControllerTest()
        {
            var result = controller.Get();
            Assert.That(result, Is.InstanceOf<List<AuditBenchmarkClass>>());
        }
    }
}