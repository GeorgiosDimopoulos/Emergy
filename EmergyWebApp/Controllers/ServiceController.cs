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
    public class ServiceController : TableController<Service>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            EmergyContext context = new EmergyContext();
            DomainManager = new EntityDomainManager<Service>(context, Request);
        }

        // GET tables/TodoItem
        public IQueryable<Service> GetAllTodoItems()
        {
            return Query();
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Service> GetTodoItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Service> PatchTodoItem(string id, Delta<Service> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostService(Service item)
        {
            Service current = await InsertAsync(item);
            var context = GlobalHost.ConnectionManager.GetHubContext<SignalHub>();

            context.Clients.All.RefreshServices();

            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}