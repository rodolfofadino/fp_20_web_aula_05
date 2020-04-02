using System.Collections.Generic;
using fiapweb2020.core.Models;

namespace fiapweb2020.core.Services
{
    public interface INoticiaService
    {
        List<Noticia> Load(int totalDeNoticias);
    }
}