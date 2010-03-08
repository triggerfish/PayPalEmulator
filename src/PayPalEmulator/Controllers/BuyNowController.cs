using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Triggerfish.NHibernate;
using Triggerfish.Web.Mvc;

namespace PayPalEmulator.Controllers
{
	[HandleError]
	public class BuyNowController : Controller
	{
		private Repository<PDT> m_pdtRepository;

		public BuyNowController(Repository<PDT> pdtRepository)
		{
			m_pdtRepository = pdtRepository;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ViewResult BuyNow(int pdtId)
		{
			PDT pdt = GetPdt(pdtId);

			m_pdtRepository.Insert(pdt);

			return View(new PaymentViewData { PDT = pdt });
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[Transaction]
		public ActionResult PayNow(int pdtId)
		{
			PDT pdt = GetPdt(pdtId);

			pdt.Tx = Regex.Replace(Guid.NewGuid().ToString(), "-", String.Empty);
			pdt.State = "Completed";

			return RedirectToAction("Paid", new { pdtId = pdtId });
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ViewResult Paid(int pdtId)
		{
			PDT pdt = GetPdt(pdtId);

			return View(new PaymentViewData { PDT = pdt });
		}

		private PDT GetPdt(int id)
		{
			PDT pdt = m_pdtRepository.Get(id);

			if (null == pdt)
			{
				ModelState.AddModelError("pdtId", String.Format("Invalid ID: {0}", id));
				throw new ErrorDataException(ModelState);
			}

			return pdt;
		}
	}
}
