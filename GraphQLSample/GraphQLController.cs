using GraphQL;
using GraphQL.Types;
using GraphQLSample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;


namespace GraphQLSample.Controllers
{
    [Route("graphql")]
    [ApiController]
    public class GraphQLController : Controller
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;
        public GraphQLController(ISchema schema,
        IDocumentExecuter executer)
        {
            _schema = schema;
            _executer = executer;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]
        GraphQLQueryDTO query)
        {
            var result = await _executer.ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = query.Query;
                _.Inputs = query.Variables.ToInputs();// query.Variables.ToInputs();

            });
            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }
            return Ok(result.Data);
        }
    }
}
