using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Breeze.WebApi;
using DurandalBreeze.Hubs;
using DurandalBreeze.Models;
using Newtonsoft.Json.Linq;

namespace DurandalBreeze.Controllers
{
    [BreezeController]
    public class TodoController : ApiControllerWithHub<TodoHub>
    {
        readonly EFContextProvider<BreezeSampleContext> _contextProvider =
            new EFContextProvider<BreezeSampleContext>();

        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }

        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            var result = _contextProvider.SaveChanges(saveBundle);

            Hub.Clients.All.updateItems(saveBundle);

            return result;
        }

        [HttpGet]
        public IQueryable<BreezeSampleTodoItem> Todos()
        {
            return _contextProvider.Context.Todos;
        }        
    }
}