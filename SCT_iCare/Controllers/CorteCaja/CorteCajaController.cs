using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using conekta;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SCT_iCare;
using System.IO;
using System.Text;
using System.Globalization;
using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using System.IO.Compression;
using PagedList;

namespace SCT_iCare.Controllers.CorteCaja
{
    public class CorteCajaController : Controller
    {
        // GET: CorteCaja
        public ActionResult Index()
        {
            return View();
        }

    }
}
