using ArchPM.Core.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.Tests.Domain
{
    [ApiHelp]
    public class ApiHelpAttributeDefinedOnlyOnClass
    {
        public void MethodNotHavingApiHelpAttribute1()
        {

        }

        public void MethodNotHavingApiHelpAttribute2()
        {

        }

    }


    public class InputParameterAsNullablePropertiesContainingClass
    {
        [ApiHelp]
        public void InputNullableValueTypesMethod(HavingNullablePropertyClass input)
        {

        }

        public class HavingNullablePropertyClass
        {
            public Int32? NullableInt32 { get; set; }
        }
    }

    public class NullableValueTypesContainingClass
    {
        [ApiHelp]
        public void InputNullableValueTypesMethod(Int32? input1, DateTime? input2)
        {

        }
    }

    [ApiHelp(Comment = "ComplexRequest here!")]
    public class ComplexRequest
    {
        public String Id { get; set; }
        public SimpleRequest Simple { get; set; }
        public ComplexRequest ComplexRequest1 { get; set; }
    }

    [ApiHelp]
    public class ComplexResponse
    {
        [ApiHelp(Comment = "Name here!")]
        public String Name { get; set; }
        public Int32 Age { get; set; }
        public List<ComplexResponse> Responses { get; set; }
    }

    [ApiHelp]
    public class SimpleRequest
    {
        public String Id { get; set; }
    }

    [ApiHelp]
    public class SimpleResponse
    {
        public String Name { get; set; }
        public Int32 Age { get; set; }
    }

    public class DummyController
    {
        [ApiHelpAttribute(Comment = "PrimitiveInputsPrimitiveOutput bla bla")]
        public Int32 PrimitiveInputsPrimitiveOutput(Int32 input1, String input2, DateTime? input3)
        {
            return 0;
        }

        [ApiHelp(Comment = "PrimitiveInputsApiResponseWithPrimitiveOutput bla bla")]
        public ApiResponse<Int32> PrimitiveInputsApiResponseWithPrimitiveOutput(Int32 input1, String input2, DateTime? input3)
        {
            return null;
        }

        [ApiHelpAttribute(Comment = "SimpleInputSimpleOutput bla bla!")]
        public SimpleResponse SimpleInputSimpleOutput(SimpleRequest request)
        {
            return null;
        }

        [ApiHelpAttribute(Comment = "PrimitiveAndSimpleInputSimpleOutput bla bla!")]
        public SimpleResponse PrimitiveAndSimpleInputSimpleOutput(Int32 input1, SimpleRequest request)
        {
            return null;
        }

        [ApiHelpAttribute(Comment = "ComplexInputComplexOutput bla bla!")]
        public ComplexResponse ComplexInputComplexOutput(ComplexRequest request)
        {
            return null;
        }

        [ApiHelpAttribute(Comment = "ComplexInputApiResponseWithComplexOutput bla bla!")]
        public ApiResponse<ComplexResponse> ComplexInputApiResponseWithComplexOutput(ComplexRequest request)
        {
            return null;
        }

        public ApiResponse<ComplexResponse> NoHelpMethod1(ComplexRequest request)
        {
            return null;
        }

        public ApiResponse<ComplexResponse> NoHelpMethod2(ComplexRequest request)
        {
            return null;
        }

    }

    public class ComplexMethodController
    {
        [ApiHelpAttribute(Comment = "Input_ComplexRequest_Output_ApiResponse_ComplexResponse bla bla!")]
        public ApiResponse<ComplexResponse> Input_ComplexRequest_Output_ApiResponse_ComplexResponse(ComplexRequest request)
        {
            return null;
        }
    }

    public class TaskBasedController
    {
        [ApiHelpAttribute(Comment = "Input_ComplexRequest_Output_Task_ApiResponse_Int32 bla bla!")]
        public Task<ApiResponse<ComplexResponse>> Input_ComplexRequest_Output_Task_ApiResponse_Int32(ComplexRequest request)
        {
            return null;
        }
    }
}
