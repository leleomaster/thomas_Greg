using Humanizer.Bytes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ThomasGreg.Domain.Models;

namespace ThomasGreg.Web.Utils
{
    public static class Helpers
    {
        public static string GetTokenSession(HttpContext httpContext)
        {
            var token = httpContext.Session.GetString("JWToken");
            return token;
        }

        public static IEnumerable<SelectListItem> ConvertLogradouroParaSelectListItem(IEnumerable<LogradouroViewModel> logradouroViewModels)
        {
            if (!logradouroViewModels.Any())
                return new List<SelectListItem>()
                {
                    new SelectListItem { Value = "", Text = "" }
                };

            List<SelectListItem> lista = new List<SelectListItem>();
            SelectListItem selectListItem = null;

            lista.Add(new SelectListItem() { Value = "", Text = "Selecione" });
            foreach (var logradouro in logradouroViewModels)
            {
                selectListItem = new SelectListItem { Value = logradouro.Id.ToString(), Text = logradouro.Nome + ", " + logradouro.Numero };

                lista.Add(selectListItem);
            }
            return lista;
        }

        public static byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }

        public static void ConvertImgDataURL(ClienteViewModel model)
        {
            if (model != null)
            {
                string imreBase64Data = Convert.ToBase64String(model.Logotipo);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);

                model.ImgDataURL = imgDataURL;
            }
        }
        public static void ConvertImgDataURL(IEnumerable<ClienteViewModel> model)
        {
            if (model != null)
            {
                foreach (var item in model)
                {
                    ConvertImgDataURL(item);
                }
            }
        }
    }
}
