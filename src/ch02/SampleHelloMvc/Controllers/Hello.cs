// p.18 [Add] 新しいControllerクラスを追加する
using Microsoft.AspNetCore.Mvc;

namespace SampleHelloMvc.Controllers
{
    public class Hello : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
