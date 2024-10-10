using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ext.Net;
using Ext.Net.MVC;
using Ext.Net.Utilities;
using System.Web.UI;
using System;
using SoluccionSalud.Entidades.Entidades;

namespace AppSaludMVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyAuthorizeAttribute());
            filters.Add(new HandleExceptionAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }

    //*****

    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method)]
    public sealed class MyAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values["controller"].ToString();

            //if
            if (controllerName.ToString().ToUpper() != "LOGIN")
            {
                // Validar la información que se encuentra en la sesión.
                if (HttpContext.Current.Session["User"] == null)
                {
                    // string strController = StrReverse
                    filterContext.Result = new ContentResult();

                    // Si la información es nula, redireccionar a
                    // página de error u otra página deseada.

                    filterContext.Result = new RedirectToRouteResult(
                       new RouteValueDictionary {
                       { "Controller", "Login" },
                       { "Action", "Index" }
                       });

                }

                /* base.OnAuthorization(filterContext);*/
            }
        }

    }

    // [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]

    public class HandleExceptionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var redirectTargetDictionary = new RouteValueDictionary();
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            //String errManualDescripcion = "";

            if (controllerName.ToString().ToUpper() != "LOGIN")
            {
                // Validar la información que se encuentra en la sesión.
                if (HttpContext.Current.Session["User"] == null)
                {
                    filterContext.Result = new ContentResult();

                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                         { "Controller", "Login" },
                         { "Action", "Index" }
                         });


                }

                // base.OnAuthorization(filterContext);
            }
            base.OnActionExecuting(filterContext);
        }



        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            String errManualNumero = "";
            String errManualDescripcion = "";
            String[] separador = new string[] { "@" };

            if (!filterContext.HttpContext.Request.IsAjaxRequest() & filterContext.Exception != null)
            {
                sisSesion.ErrorManualController = filterContext.Exception.TargetSite.DeclaringType.Name;
                sisSesion.ErrorManualAction = filterContext.Exception.TargetSite.Name;
                sisSesion.ErrorManualNumero = Convert.ToString(filterContext.Exception.GetHashCode());
                sisSesion.ErrorManualDescripcion = filterContext.Exception.Message;
            }
            else if (filterContext.HttpContext.Request.IsAjaxRequest() && filterContext.Exception != null)
            {
                if (filterContext.Exception.Message.Contains("@"))
                {
                    errManualNumero = Convert.ToString(filterContext.Exception.Message.Split(separador, 0, StringSplitOptions.None));
                    errManualDescripcion = Convert.ToString(filterContext.Exception.Message.Split(separador, 1, StringSplitOptions.None));
                }
                else
                {
                    errManualNumero = Convert.ToString(filterContext.Exception.GetHashCode());
                    errManualDescripcion = filterContext.Exception.Message;
                }

                sisSesion.ErrorManualController = filterContext.Exception.TargetSite.DeclaringType.Name;
                sisSesion.ErrorManualAction = filterContext.Exception.TargetSite.Name;
                sisSesion.ErrorManualNumero = errManualNumero;
                sisSesion.ErrorManualDescripcion = errManualDescripcion;
                filterContext.HttpContext.Response.StatusCode = Convert.ToInt32((int)(System.Net.HttpStatusCode.InternalServerError));
                // '  .Data = New With {Key errDescription, Key filterContext.Exception.StackTrace}};
                filterContext.Result = new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { Message = errManualDescripcion, StackTrace = filterContext.Exception.StackTrace, Error = errManualNumero }
                };
                filterContext.ExceptionHandled = true;
                //filterContext.Result = RedirectToAction("Index", "Error");
                filterContext.Result = new RedirectToRouteResult(
                         new RouteValueDictionary {
                         { "Controller", "Error" },
                         { "Action", "Index" }
                         });

            }
        }

    }





}