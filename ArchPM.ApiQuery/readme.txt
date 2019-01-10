[HttpPost]
[ApiHelp]
public async Task<ApiResponse<Int32>> Create([FromBody] CreateRequest request)
{
    var engine = new ApiQueryEngine<CreateRequest, Int32>(base.CreateDatabaseAdaptor);
    var data = await engine.Execute(request);
    return data;
}

