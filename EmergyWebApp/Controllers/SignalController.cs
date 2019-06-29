using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using EmergyService.DataObjects;
using EmergyService.Hubs;
using EmergyService.Models;
using Microsoft.AspNet.SignalR;

namespace EmergyService.Controllers
{
    public class SignalController : TableController<Signal>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            EmergyContext context = new EmergyContext();
            DomainManager = new EntityDomainManager<Signal>(context, Request);
        }

        // GET tables/TodoItem
        public IQueryable<Signal> GetAllTodoItems()
        {
            return Query();
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Signal> GetTodoItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Signal> PatchTodoItem(string id, Delta<Signal> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostSignal(Signal item)
        {
            Signal current = await InsertAsync(item);
            var context = GlobalHost.ConnectionManager.GetHubContext<SignalHub>();

            context.Clients.All.RefreshSignals();
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}