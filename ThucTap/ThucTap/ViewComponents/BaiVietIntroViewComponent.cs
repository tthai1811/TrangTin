using Microsoft.AspNetCore.Mvc;

namespace DongHoShop.ViewComponents
{
    public class BaiVietIntroViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
