using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ArchPM.Core.Extensions;
using ArchPM.Core.Tests.Domain;

namespace ArchPM.Core.Tests
{
    [TestClass]
    public class ApiTests
    {
        [TestMethod]
        public void ApiHelpAttributeDefinedOnlyOnClass_NoInputNoOutput_Valid()
        {
            var response = new Api.ApiHelpResponse(typeof(ApiHelpAttributeDefinedOnlyOnClass));

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Actions, "Actions is null");
            Assert.AreEqual(2, response.Actions.Count);
        }

        [TestMethod]
        public void ApiHelpAttributeDefinedOnlyOnClass_2MethodsContaining()
        {
            var response = new Api.ApiHelpResponse(typeof(ApiHelpAttributeDefinedOnlyOnClass));

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Actions, "Actions is null");
            Assert.AreEqual(2, response.Actions.Count);
        }



        [TestMethod]
        public void InputParameterAsNullablePropertiesContainingClass_InputClassPropertyTypeHavingQuestionMark()
        {
            var response = new Api.ApiHelpResponse(typeof(InputParameterAsNullablePropertiesContainingClass));

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Actions, "Actions is null");
            Assert.AreEqual(1, response.Actions.Count);
            Assert.AreEqual(0, response.Actions[0].OutputParameters.Count);
            Assert.AreEqual(1, response.Actions[0].InputParameters.Count);
            Assert.AreEqual("input", response.Actions[0].InputParameters[0].Name);
            Assert.AreEqual("HavingNullablePropertyClass", response.Actions[0].InputParameters[0].Type);
            Assert.AreEqual(1, response.Actions[0].InputParameters[0].Parameters.Count);
            Assert.AreEqual("NullableInt32", response.Actions[0].InputParameters[0].Parameters[0].Name);
            Assert.AreEqual("Int32?", response.Actions[0].InputParameters[0].Parameters[0].Type);

        }


        [TestMethod]
        public void NullableMethodContainerClass_VoidOutputIsValid()
        {
            var response = new Api.ApiHelpResponse(typeof(NullableValueTypesContainingClass));

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Actions, "Actions is null");
            Assert.AreEqual(1, response.Actions.Count);
            Assert.AreEqual(0, response.Actions[0].OutputParameters.Count);

        }

        [TestMethod]
        public void NullableMethodContainerClass_InputTypesHavingQuestionMark()
        {
            var response = new Api.ApiHelpResponse(typeof(NullableValueTypesContainingClass));

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Actions, "Actions is null");
            Assert.AreEqual(1, response.Actions.Count);
            Assert.AreEqual(2, response.Actions[0].InputParameters.Count);
            Assert.AreEqual("input1", response.Actions[0].InputParameters[0].Name);
            Assert.AreEqual("Int32?", response.Actions[0].InputParameters[0].Type);
            Assert.AreEqual("input2", response.Actions[0].InputParameters[1].Name);
            Assert.AreEqual("DateTime?", response.Actions[0].InputParameters[1].Type);

        }

        [TestMethod]
        public void HelpWhenCalledThenReturns6Methods()
        {
            var response = new Api.ApiHelpResponse(typeof(DummyController));

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Actions, "Actions is null");
            Assert.AreEqual(6, response.Actions.Count);
        }

        [TestMethod]
        public void SingleComplexMethodWhen()
        {
            var response = new Api.ApiHelpResponse(typeof(ComplexMethodController));

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Actions, "Actions is null");
            Assert.AreEqual(1, response.Actions.Count);

            var item = response.Actions[0];
            {
                Assert.IsTrue(item.Name == "Input_ComplexRequest_Output_ApiResponse_ComplexResponse");
                Assert.IsTrue(item.InputParameters.Count == 1);
            }

            var inputPrm = item.InputParameters[0];
            {
                Assert.AreEqual("request", inputPrm.Name);
                Assert.AreEqual("ComplexRequest", inputPrm.Type);

                Assert.AreEqual("Id", inputPrm.Parameters[0].Name);
                Assert.AreEqual("String", inputPrm.Parameters[0].Type);
                Assert.AreEqual("Simple", inputPrm.Parameters[1].Name);
                Assert.AreEqual("SimpleRequest", inputPrm.Parameters[1].Type);
                Assert.AreEqual("ComplexRequest1", inputPrm.Parameters[2].Name);
                Assert.AreEqual("ComplexRequest", inputPrm.Parameters[2].Type);
            }

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Help_Input_Null()
        {
            var response = new Api.ApiHelpResponse(null);
        }

        [TestMethod]
        public void Help_TaskBasedController_Input_ComplexRequest_Output_Task_ApiResponse_Int32()
        {
            var response = new Api.ApiHelpResponse(typeof(TaskBasedController));

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Actions, "Actions is null");
            Assert.AreEqual(1, response.Actions.Count);

            foreach (var item in response.Actions)
            {
                Assert.AreEqual("Input_ComplexRequest_Output_Task_ApiResponse_Int32", item.Name);
                Assert.AreEqual(1, item.InputParameters.Count);

                var inputPrm = item.InputParameters[0];
                Assert.AreEqual("request", inputPrm.Name);
                Assert.AreEqual("ComplexRequest", inputPrm.Type);

                Assert.AreEqual("Id", inputPrm.Parameters[0].Name);
                Assert.AreEqual("String", inputPrm.Parameters[0].Type);
                Assert.AreEqual("Simple", inputPrm.Parameters[1].Name);
                Assert.AreEqual("SimpleRequest", inputPrm.Parameters[1].Type);
                Assert.AreEqual("ComplexRequest1", inputPrm.Parameters[2].Name);
                Assert.AreEqual("ComplexRequest", inputPrm.Parameters[2].Type);


                Assert.AreEqual(1, item.OutputParameters.Count);
                var outputPrm = item.OutputParameters[0];
                Assert.AreEqual("Result", outputPrm.Parameters[0].Name);
                Assert.AreEqual("Boolean", outputPrm.Parameters[0].Type);
                Assert.AreEqual("Code", outputPrm.Parameters[1].Name);
                Assert.AreEqual("String", outputPrm.Parameters[1].Type);
                Assert.AreEqual("Message", outputPrm.Parameters[2].Name);
                Assert.AreEqual("String", outputPrm.Parameters[2].Type);
                Assert.AreEqual("Source", outputPrm.Parameters[3].Name);
                Assert.AreEqual("String", outputPrm.Parameters[3].Type);
                Assert.AreEqual("Errors", outputPrm.Parameters[4].Name);
                Assert.AreEqual("List<ApiError>", outputPrm.Parameters[4].Type);
                Assert.AreEqual("Data", outputPrm.Parameters[5].Name);
                Assert.AreEqual("ComplexResponse", outputPrm.Parameters[5].Type);
                Assert.AreEqual(3, outputPrm.Parameters[5].Parameters.Count);
                var outComplexResponse = outputPrm.Parameters[5];
                Assert.AreEqual("Name", outComplexResponse.Parameters[0].Name);
                Assert.AreEqual("String", outComplexResponse.Parameters[0].Type);
                Assert.AreEqual("Age", outComplexResponse.Parameters[1].Name);
                Assert.AreEqual("Int32", outComplexResponse.Parameters[1].Type);
                Assert.AreEqual("Responses", outComplexResponse.Parameters[2].Name);
                Assert.AreEqual("List<ComplexResponse>", outComplexResponse.Parameters[2].Type);

                Assert.AreEqual(3, outComplexResponse.Parameters[2].Parameters.Count);
                var outputPrm_2 = outComplexResponse.Parameters[2];

                //Assert.AreEqual("", outputPrm_2.Parameters[0].Name);
                //Assert.AreEqual("String", outputPrm_2.Parameters[0].Type);
                //Assert.AreEqual("Simple", outputPrm_2.Parameters[1].Name);
                //Assert.AreEqual("SimpleRequest", outputPrm_2.Parameters[1].Type);
                //Assert.AreEqual(1, outputPrm_2.Parameters[1].Parameters.Count);
                //Assert.AreEqual("Id", outputPrm_2.Parameters[1].Parameters[0].Name);
                //Assert.AreEqual("String", outputPrm_2.Parameters[1].Parameters[0].Type);
                //Assert.AreEqual("ComplexRequest1", outputPrm_2.Parameters[2].Name);
                //Assert.AreEqual("ComplexRequest", outputPrm_2.Parameters[2].Type);
            }

        }

        [TestMethod]
        public void Help_TaskBasedController_Check_Comments()
        {
            var response = new Api.ApiHelpResponse(typeof(TaskBasedController));

            Assert.AreEqual("Input_ComplexRequest_Output_Task_ApiResponse_Int32 bla bla!", response.Actions[0].Comment);
            Assert.AreEqual("ComplexRequest here!", response.Actions[0].InputParameters[0].Comment);
            Assert.AreEqual("Name here!", response.Actions[0].OutputParameters[0].Parameters[5].Parameters[0].Comment);
        }



    }
}
