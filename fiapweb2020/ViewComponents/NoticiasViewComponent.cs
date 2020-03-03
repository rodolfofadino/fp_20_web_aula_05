using fiapweb2020.Models;
using fiapweb2020.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiapweb2020.ViewComponents
{
    public class NoticiasViewComponent : ViewComponent
    {
        private INoticiaService _service;

        public NoticiasViewComponent(INoticiaService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(int totalDeNoticias, bool noticiasUrgents)
        {
            var view = noticiasUrgents ? "noticiasUrgentes" : "noticias";

            //await Task.Delay(200);
            return View(view, _service.Load(totalDeNoticias));
        }
    }
}
