using System.Collections.Generic;
using fiapweb2020.Models;

namespace fiapweb2020.Services
{
    public interface INoticiaService
    {
        List<Noticia> Load(int totalDeNoticias);
    }
}