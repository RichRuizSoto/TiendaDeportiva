using System.Web.Mvc;

namespace TiendaDeportiva.MVC.Controllers
{
    public class ProductosController : Controller
    {
        // GET: Productos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        // GET: Productos/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}