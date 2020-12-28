using Alura.ListaLeitura.HttpClients;
using Alura.ListaLeitura.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Alura.ListaLeitura.WebApp.Controllers
{
    [Authorize]
    public class LivroController : Controller
    {
        private readonly LivroApiClient _api;


        public LivroController( LivroApiClient api)
        {
            _api = api;
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View(new LivroUpload());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Novo(LivroUpload model)
        {
            if (ModelState.IsValid)
            {
                //_repo.Incluir(model.ToLivro());
                await _api.PostLivroAsync(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ImagemCapa(int id)
        {

            byte[] img = await _api.GetCapaLivroAsync(id);
                //_repo.All
                //.Where(l => l.Id == id)
                //.Select(l => l.ImagemCapa)
                //.FirstOrDefault();
            if (img != null)
            {
                return File(img, "image/png");
            }
            return File("~/images/capas/capa-vazia.png", "image/png");
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {


            var model = await _api.GetLivroAsync(id);//_repo.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model.ToUpload());
        }
       


        //public Livro RecuperaLivro(int id)
        //{
        //    return _repo.Find(id);
        //}

        //public ActionResult <LivroUpload> DetalhesJson(int id)
        //{
        //    var model = RecuperaLivro(id);
        //    if (model == null)
        //    {
        //        return NotFound();
        //    }
        //    return model.ToModel();

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Detalhes(LivroUpload model)
        {
            if (ModelState.IsValid)
            {
                await _api.PutLivroAsync(model);
                //var livro = model.ToLivro();
                //if (model.Capa == null)
                //{
                //    livro.ImagemCapa = _repo.All
                //        .Where(l => l.Id == livro.Id)
                //        .Select(l => l.ImagemCapa)
                //        .FirstOrDefault();
                //}
                //_repo.Alterar(livro);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remover(int id)
        {
            var model = await _api.GetLivroAsync(id); //_repo.Find(id);
            if (model == null)
            {
                return NotFound();
            }

            await _api.DeleteLivroAsync(id);
            //_repo.Excluir(model);
            return RedirectToAction("Index", "Home");
        }
    }
}