using Sitecore.Forms.Mvc.Events;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.Models;
using System.Linq;
using System.Web;

namespace Sitecore.Support.Forms.Mvc.Data
{
    public class FormDataManager : Sitecore.Forms.Mvc.Data.FormDataManager
    {
        public FormDataManager(IProcessorFactory processorFactory) : base(processorFactory)
        {
        }

        protected override void FormLoadState(FormEventArgs args)
        {
            base.FormLoadState(args);

            if (args.RequestType == RequestType.POST)
            {
                return;
            }
            if (HttpContext.Current == null)
            {
                return;
            }
                
            if (HttpContext.Current.Request == null)
            {
                return;
            }
                
            var request = HttpContext.Current.Request;

            foreach (FieldModel fieldModel in args.Form.Sections.SelectMany(x => x.Fields))
            {
                var value = request.QueryString[fieldModel.Name];

                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }
                    
                fieldModel.Value = value;
            }
        }
    }
}