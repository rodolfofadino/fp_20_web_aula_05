using CodeHollow.FeedReader;
using fiapweb2020.core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace fiapweb2020.core.Services
{
    public class NoticiaService : INoticiaService
    {
        private IMemoryCache _memoryCache;

        public NoticiaService(IMemoryCache memoryCache)
        //public NoticiaService(IDistributedCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public List<Noticia> Load(int totalDeNoticias)
        {
            var key = $"noticias_";

            List<Noticia> noticias;
            
            if (!_memoryCache.TryGetValue(key, out noticias))
            {
                noticias = new List<Noticia>();

                var feed = FeedReader.ReadAsync("https://g1.globo.com/rss/g1/turismo-e-viagem/").Result;

                foreach (var item in feed.Items)
                {
                    var feedItem = item.SpecificItem as CodeHollow.FeedReader.Feeds.MediaRssFeedItem;
                    var media = feedItem.Media;
                    var url = "";
                    if (media.Any())
                        url = media.FirstOrDefault().Url;
                    noticias.Add(new Noticia() { Id = 1, Titulo = item.Title, Link = item.Link, Imagem = url });
                }

                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(2));
                //var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration( TimeSpan.FromMinutes(2));

                _memoryCache.Set(key, noticias, cacheOptions);
            }

            return noticias;
        }
    }
}