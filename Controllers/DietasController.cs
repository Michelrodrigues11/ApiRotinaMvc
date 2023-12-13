using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using ApiRotinaMvc.Models;

namespace ApiRotinaMvc.Controllers
{
    public class DietasController : Controller
    {
        public string uriBase = "http://MichelDSHR.somee.com/ApiRotina/Dietas/";

        [HttpGet]
        public async Task<ActionResult> IndexDietas()
        {
            try
            {
                string uriComplementar = "GetAll";
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<DietaViewModel> listaDietas = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<DietaViewModel>>(serialized));

                    return View(listaDietas);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {

                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("IndexDietas");
            }
        }
        [HttpPost]
        public async Task<ActionResult> CadastrarAsync(DietaViewModel p)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(JsonConvert.SerializeObject(p));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Dieta {0}, Id {1} salvo com sucesso!", p.Dia, serialized);
                    return RedirectToAction("IndexDieta");
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Create");
            }
        }
        [HttpGet]
        public async Task<ActionResult> DetalhesAsync(int? idDieta)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.GetAsync(uriBase + idDieta.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    DietaViewModel p = await Task.Run(() =>
                    JsonConvert.DeserializeObject<DietaViewModel>(serialized));
                    return View(p);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("indexDietas");
            }
        }
        [HttpGet]
        public async Task<ActionResult> EditarAsync(int? idDieta)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.GetAsync(uriBase + idDieta.ToString());

                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    DietaViewModel p = await Task.Run(() =>
                    JsonConvert.DeserializeObject<DietaViewModel>(serialized));
                    return View(p);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("IndexDietas");
            }
        }
        [HttpPost]
        public async Task<ActionResult> EditarAsync(DietaViewModel p)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var content = new StringContent(JsonConvert.SerializeObject(p));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await httpClient.PutAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] =
                        string.Format("Dieta {0}, class{1} atualizado com sucesso!", p.Dia, p.DietaClass);

                    return RedirectToAction("IndexDietas");
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {

                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("IndexDietas");
            }
        }
         [HttpGet]
        public async Task<ActionResult> DeleteAsync(int idDieta)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Barer", token);

                HttpResponseMessage response = await httpClient.DeleteAsync(uriBase + idDieta.ToString());
                String serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Dieta Id {0} removida com sucesso!", idDieta);
                    return RedirectToAction("IndexDietas");
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {

                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("IndexDietas");

            }

        }

    }
}
