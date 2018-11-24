using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CardValidation.Repository;

namespace CardValidation.Api.Controllers
{
    /// <summary>
    ///     test controller
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TestController : ApiController
    {
        private readonly IRepositoryCardValidation repository;

        /// <summary>
        ///     test controller constructor
        /// </summary>
        /// <param name="repositoryProductCatalog"></param>
        public TestController(IRepositoryCardValidation repository)
        {
            this.repository = repository;
        }

        /// <summary>
        ///     test any get method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Test/Get")]
        public async Task<string> Get()
        {
            await Task.Delay(10);
            return "hello test";
        }
    }
}