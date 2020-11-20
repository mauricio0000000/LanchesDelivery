using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LanchesDel.Utils
{
    public class UtilsSession
    {
        private readonly IHttpContextAccessor _http;
        private readonly string SACOLA_ID = "sacolaId";
        public UtilsSession(IHttpContextAccessor http)
        {
            _http = http;
        }
        public string RetornarCarrinhoId()
        {
            if (_http.HttpContext.Session.GetString(SACOLA_ID) == null)
            {
                _http.HttpContext.Session.SetString(SACOLA_ID, Guid.NewGuid().ToString());
            }
            return _http.HttpContext.Session.GetString(SACOLA_ID);
        }
    }
}
