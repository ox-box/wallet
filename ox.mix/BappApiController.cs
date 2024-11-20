using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using OX.Bapps;
using System.Reflection;


namespace OX.Wallets
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ApiTagAttribute : RouteAttribute
    {
        public string ApiBoxName { get; set; }
        public string ApiModuleName { get; set; }
        public string ApiActionName { get; set; }

        public ApiTagAttribute(string boxName, string moduleName, string actionName)
            : base("/api/" + boxName + "/" + moduleName + "/" + actionName)
        {
            ApiBoxName = boxName;
            ApiModuleName = moduleName;
            ApiActionName = actionName;
        }
    }
    [ApiController]
    public class BappApiController : ControllerBase
    {
        static Dictionary<string, ApiBoxBuilder> ApiBox = new Dictionary<string, ApiBoxBuilder>();
        static BappApiController()
        {
            foreach (var assembly in Bapp.Assemblies)
            {
                foreach (Type type in assembly.ExportedTypes)
                {
                    if (!type.IsSubclassOf(typeof(ApiBoxBuilder))) continue;
                    if (type.IsAbstract) continue;

                    ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                    try
                    {
                        var apiBuilder = constructor?.Invoke(null) as ApiBoxBuilder;
                        ApiBox[apiBuilder.ApiBoxName] = apiBuilder;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        [Route("api/{ApiBoxName}/{ApiModuleName}/{ApiActionName}/{arg?}")]
        [HttpGet]
        [ResponseCache(Duration = 60)]
        public IActionResult Get(string ApiBoxName, string ApiModuleName, string ApiActionName, string arg)
        {
            if (ApiBox.TryGetValue(ApiBoxName, out var boxBuilder))
            {
                var box = boxBuilder.Build();
                if(box.IsNull()) return StatusCode(404);
                return box.ProcessGet(this, ApiModuleName, ApiActionName, arg);
            }
            return StatusCode(404);
        }
        [Route("api/{ApiBoxName}/{ApiModuleName}/{ApiActionName}/{arg?}")]
        [HttpPost]
        public IActionResult Post(string ApiBoxName, string ApiModuleName, string ApiActionName, string arg)
        {
            if (ApiBox.TryGetValue(ApiBoxName, out var boxBuilder))
            {
                var box = boxBuilder.Build();
                if (box.IsNull()) return StatusCode(404);
                return box.ProcessPost(this, ApiModuleName, ApiActionName, arg);
            }
            return StatusCode(404);
        }

    }
}
