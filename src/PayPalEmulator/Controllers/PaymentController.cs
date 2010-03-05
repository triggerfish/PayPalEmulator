using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Triggerfish.NHibernate;

namespace PayPalEmulator.Controllers
{
	[HandleError]
	public class PaymentController : Controller
	{
		private Repository<PDT> m_pdtRepository;

		public PaymentController(Repository<PDT> pdtRepository)
		{
			m_pdtRepository = pdtRepository;
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[Transaction]
		public ViewResult MakePayment(PDT pdt)
		{
			if (!ModelState.IsValid)
			{
				return View("Error", new HandleErrorInfo(new ErrorDataException(ModelState), "Payment", "MakePayment"));
			}

			m_pdtRepository.Insert(pdt);

			return View(new PaymentViewData { PDT = pdt });
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[Transaction]
		public ActionResult PayNow(int pdtId)
		{
			PDT pdt = m_pdtRepository.Get(pdtId);

			if (null == pdt)
			{
				ModelState.AddModelError("pdtId", String.Format("Invalid ID: {0}", pdtId));
				throw new ErrorDataException(ModelState);
			}

			pdt.Tx = Regex.Replace(Guid.NewGuid().ToString(), "-", String.Empty);
			pdt.State = "Completed";

			return RedirectToAction("Paid", new { pdtId = pdtId });
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ViewResult Paid(int pdtId)
		{
			PDT pdt = m_pdtRepository.Get(pdtId);

			if (null == pdt)
			{
				return View("Error", new HandleErrorInfo(new ErrorDataException(ModelState), "Payment", "MakePayment"));
			}

			return View(new PaymentViewData { PDT = pdt });
		}
	}
}
