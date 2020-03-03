using fiapweb2020.Models;
using System;
using System.Collections.Generic;

namespace fiapweb2020.Services
{
    public class NoticiaService : INoticiaService
    {

        public NoticiaService()
        {

        }

        public List<Noticia> Load(int totalDeNoticias)
        {
            var noticias = new List<Noticia>();
            for (int i = 0; i < totalDeNoticias; i++)
            {
                noticias.Add(new Noticia()
                {
                    Id = i + 1,
                    Titulo = $"Noticia sobre {i + 1}",
                    Link = "http://www.globo.com"
                });
            }

            return noticias;
        }
    }
}