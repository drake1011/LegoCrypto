using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LegoCrypto.Data.Model;
using System.Runtime.Serialization;

namespace LegoCrypto.API.Controllers
{
    [Route("api/[controller]/encrypt")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        [HttpPost]
        public ActionResult<ITag> Encrypt(TagRequestId request)
        {
            try
            {
                ITag tag;
                tag = TagFactory.CreateTag(request.id, request.uid);
                tag.Encrypt();
                return new ActionResult<ITag>(tag);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}